using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using RPG.Battle.Core;
using RPG.Battle.Behaviour;
using RPG.Battle.AI;
using RPG.Battle.UI;
using RPG.Character.Status;
using RPG.Main.Audio;

/*
 *  ���� �� ĳ������ ������ �����ϴ� ��Ʈ�ѷ�
 */

namespace RPG.Battle.Control
{
    public abstract class Controller : MonoBehaviour
    {
        // Component
        public CharacterUI ui;              // ĳ���� UI
        public Animator animator;           // ĳ���� �ִϸ�����
        public NavMeshAgent nav;            // ĳ���� �׺�޽�������Ʈ
        public BattleStatus battleStatus;   // ĳ���� ����

        // AI State
        public StateContext stateContext;   // ���� ������ ����ϴ� ���ý�Ʈ
        public IdelState idleState;         // ���� ���� ����
        public ChaseState chaseState;       // ���� ���� ����
        public AttackState attackState;     // ���� ���� ����
        public DeadState deadState;         // ���� ���� ����
        public DebuffState debuffState;     // ����� ���� ����

        // Behaviour
        public Movement movement;   // �̵� �ൿ
        public Attack attack;       // ���� �ൿ

        // Battle
        public Controller target;       // ���� Ÿ��
        public AIState currentState;  // ���� ����

        private void Awake()
        {
            // ��Ʈ�ѷ� ������ �� ���۰� ���¸� �����մϴ�.
            SetUp();
        }

        private void OnEnable()
        {
            // ��Ʈ�ѷ��� Ȱ��ȭ�Ǹ� ����, UI, �̺�Ʈ�� �����մϴ�.
            battleStatus.Init();
            ui.Init();
            Init();
            if (BattleManager.Instance == null)
            {
                return;
            }
            // �̺�Ʈ ������ ���� ���¿� ���� �޼��带 �����մϴ�.
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Win, Win);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Defeat, Defeat);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Ready, Ready);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Battle, Battle);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Pause, Pause);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Ending, Ending);
        }


        private void OnDisable()
        {
            // ��Ʈ�ѷ��� ��Ȱ��ȭ�Ǹ� ����, UI �̺�Ʈ�� ���ݴϴ�.
            battleStatus.Release();
            ui.ReleaseUI();
            Release();
            if (BattleManager.Instance == null)
            {
                return;
            }
            // �̺�Ʈ ������ ����� �޼��带 ���������մϴ�.
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Win, Win);
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Defeat, Defeat);
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Ready, Ready);
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Battle, Battle);
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Pause, Pause);
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Ending, Ending);
        }


        private void Update()
        {
            // �������ΰ�?
            if (BattleManager.Instance == null) return;
            if (BattleManager.Instance.currentBattleState != BattleSceneState.Battle) return;

            // ���� ���¸� �Ǵ��մϴ�.
            stateContext.SetState(CheckState());
            // ���� ������ ������ �����մϴ�.
            stateContext.Update();
        }


        #region Initialize

        // ���� �� �ʱ�ȭ �ܰ�
        public virtual void SetUp()
        {
            movement = new Movement(battleStatus, nav);
            attack = new Attack(battleStatus);

            stateContext = new StateContext(this);
            idleState = new IdelState(this);
            chaseState = new ChaseState(this);
            attackState = new AttackState(this);
            deadState = new DeadState(this);
            debuffState = new DebuffState(this);
        }

        // Ȱ��ȭ �� �� �ʱ�ȭ�մϴ�.
        public virtual void Init()
        {
            attack.canAttack = true;
            nav.enabled = true;
            animator.Rebind();

            RuntimeAnimatorController rc = animator.runtimeAnimatorController;
            // ���� ���� �ִϸ��̼��� ���̸� üũ�մϴ�.
            foreach (var item in rc.animationClips)
            {
                if (item.name == "Attack")
                {
                    attack.defaultAttackAnimLength = item.length;
                    break;
                }
            }

            // �ൿ�� ��ġ�� ������Ʈ�մϴ�.
            battleStatus.UpdateBehaviour();
            battleStatus.currentState = CombatState.Default;
        }

        // ��Ȱ��ȭ �� �� �ൿ�Դϴ�.
        public virtual void Release()
        {
        }
        #endregion

        #region BattleSceneState EventMethod
        // �������� �¸��մϴ�.
        public void Win()
        {
            // ��������� �̵��ൿ�� ����մϴ�.
            target = null;
            movement.ResetNav();
            // ��� ������� �����ϰ� �̺�Ʈ�� �ߴ��մϴ�.
            battleStatus.StopAllDebuff();
            battleStatus.RemoveAllDebuff();
            StopCoroutine(battleStatus.perSecCoroutine);
        }

        // �������� �й��մϴ�.
        public void Defeat()
        {
            // ��������� �̵��ൿ�� ����մϴ�.
            target = null;
            movement.ResetNav();
            // ��� ������� �����ϰ� �̺�Ʈ�� �ߴ��մϴ�.
            battleStatus.StopAllDebuff();
            battleStatus.RemoveAllDebuff();
            StopCoroutine(battleStatus.perSecCoroutine);
        }

        // ������ �غ��մϴ�.
        public void Ready()
        {
            // ��������� �̵��ൿ�� ����մϴ�.
            target = null;
            movement.ResetNav();
        }

        // ������ �����մϴ�.
        public void Battle()
        {
            // ��ų �̺�Ʈ�� Ȱ��ȭ�ϰ�, �ߴ��ߴ� ������� �ٽ� Ȱ��ȭ�մϴ�.
            animator.speed = 1;
            StartCoroutine(battleStatus.perSecCoroutine);
            StartCoroutine(movement.moveEventCorotine);
            battleStatus.ReStartAllDebuff();
        }

        // ������ ��� �ߴ��մϴ�.
        public void Pause()
        {
            // �������̾��� �ִϸ����͸� ���߰��մϴ�.
            animator.speed = 0;
            // ��ų �̺�Ʈ�� Ȱ��ȭ�ϰ� ������� �ߴ��մϴ�.
            StopCoroutine(battleStatus.perSecCoroutine);
            StopCoroutine(movement.moveEventCorotine);
            battleStatus.StopAllDebuff();
            // �̵����̾��ٸ� �̵��� ����մϴ�.
            movement.ResetNav();
        }

        // ���� �� �ൿ
        protected virtual void Ending()
        {
        }


        #endregion

        #region CheckState
        // ORDER : #3) ���� ���¿� ���� ��Ʈ�ѷ��� ������ �����ϴ� �������� ����
        private IState CheckState()
        {
            if (battleStatus.isDead)
            // ���� �׾��ִ°�?
            {
                return deadState;
            }

            if (battleStatus.currentState == CombatState.Actunable)
            // �ൿ �Ұ� �����ΰ�?
            {
                return debuffState;
            }

            if (!SetTarget(out target))
            // �ٸ� ���� �ִ°�?
            {
                return idleState;
            }

            if (attack.isAttack)
                // �������ΰ�?
            {
                return attackState;
            }

            if (movement.MoveDistanceResult(target.transform))
            // ������ �Ÿ��� ���� ���� ��Ÿ����� �հ�?
            {
                return chaseState;
            }
            else
            {
                //Ÿ���� ����ִ°�?
                if (!target.battleStatus.isDead)
                {
                    if (attack.canAttack)
                        // ������ �� �ִ°�?
                        return attackState;
                }
            }

            return idleState;
        }

        #endregion

        // ������ �ߴ��մϴ�.
        public void StopAttack()
        {
            if (attack.isAttack)
            {
                attack.isAttack = false;
            }
        }

        // ��Ʈ�ѷ��� ������ ���� �Ŵ����� �˷��ݴϴ�.
        public virtual void DeadController()
        {
            BattleManager.Instance.CharacterDead(this);
        }

        /// <summary>
        /// ã���� true ��ã���� false
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        // ����� �����մϴ�.
        public abstract bool SetTarget(out Controller controller);

        // ���� �ִϸ��̼� �̺�Ʈ �޼����Դϴ�.
        public void AttackEvent()
        {
            if (target == null) return;
            if (target.battleStatus.isDead) return;

            AudioManager.Instance.PlaySoundOneShot("AttackSound");
            attack.TargetTakeDamage();
        }
    }
}