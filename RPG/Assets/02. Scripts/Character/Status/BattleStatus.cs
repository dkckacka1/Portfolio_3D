using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using RPG.Character.Equipment;
using RPG.Battle.UI;
using RPG.Battle.Event;
using RPG.Battle.Control;

/*
 * ������ ĳ���͵��� ���� Ŭ����
 */

namespace RPG.Character.Status
{
    public class BattleStatus : MonoBehaviour
    {
        // Component
        [Header("UI")]
        public CharacterUI characterUI;     // ���� ĳ���� UI

        [Header("Battle")]
        public int currentHp = 0;       // ���� ü��
        public bool isDead = false;     // ���� ����

        [Header("Status")]
        public Status status;       // ĳ���� ����

        // Coroutine
        public IEnumerator perSecCoroutine; // ���ʴ� �̺�Ʈ�� ȣ���� �ڷ�ƾ

        public CombatState currentState;    // ���� ����
        public DebuffType currentDebuff;    // ���� ����� ��Ȳ
        public bool isActunableDebuff; // ���� �ൿ �Ұ� ����� �����ΰ�?
        private bool isCursed;  // ���� ���ֹ��� �������� ����
        public List<IEnumerator> debuffList = new List<IEnumerator>(); // ���� ����� ����Ʈ

        // Event
        public PerSecondEvent perSecEvent;      // ���ʴ� ���� �̺�Ʈ
        public TakeDamageEvent takeDamageEvent; // ���ع޾����� ���� �̺�Ʈ

        // ���� ü���� �����մϴ�.
        public int CurrentHp
        {
            get => currentHp;
            set
            {
                currentHp = Mathf.Clamp(value, 0, status.MaxHp);
                if (characterUI != null)
                    // ���� ü�¹ٸ� �����մϴ�.
                {
                    characterUI.UpdateHPUI(currentHp);
                }

                if (currentHp <= 0)
                    // ü���� 0 ���ϰ� �Ǹ� ����մϴ�.
                {
                    Dead();
                }
            }
        }

        private void Awake()
        {
            perSecEvent = new PerSecondEvent();     
            takeDamageEvent = new TakeDamageEvent();
            perSecCoroutine = PerSecEvent();
        }

        #region Initialize
        // Ȱ��ȭ �Ǿ����� �ʱ�ȭ�մϴ�.
        public virtual void Init()
        {
            currentHp = status.MaxHp;
            isDead = false;
        }

        // ���� ������Ʈ�� Ȱ��ȭ �ɶ� �ൿ
        public virtual void Release()
        {
        }
        #endregion

        #region BattleEvent

        // ���������� �̺�Ʈ�� �߰��մϴ�.
        public void AddTakeDamageAction(UnityAction<BattleStatus, BattleStatus> action)
        {
            takeDamageEvent.AddListener(action);
        }

        // �ʴ� �̺�Ʈ�� �߰��մϴ�.
        public void AddPerSecAction(UnityAction<BattleStatus> action)
        {
            perSecEvent.AddListener(action);
        }

        // ���ʴ� �̺�Ʈ�� ȣ���� �ڷ�ƾ
        public IEnumerator PerSecEvent()
        {
            while (!isDead)
            {
                yield return new WaitForSeconds(1f);
                perSecEvent.Invoke(this);
            }
        }

        #endregion

        // ���� �Խ��ϴ�.
        public void TakeDamage(int damage, DamagedType type = DamagedType.Normal)
        {
            if (isDead) return;

            int totalDamage = 0;
            totalDamage += damage;
            if (isCursed)
            {
                totalDamage += (int)(damage * 0.5f);
            }

            // ���� ���� Ÿ�Կ� ���� ���� ���� �ؽ�Ʈ�� �����մϴ�.
            switch (type)
            {
                case DamagedType.Normal:
                    CurrentHp -= totalDamage;
                    characterUI.TakeDamageText(totalDamage.ToString(), type);
                    break;
                case DamagedType.Ciritical:
                    CurrentHp -= totalDamage;
                    characterUI.TakeDamageText(totalDamage.ToString() + "!!", type);
                    break;
                case DamagedType.MISS:
                    characterUI.TakeDamageText("MISS~", type);
                    break;
            }
        }

        // ĳ���Ͱ� �׽��ϴ�.
        public void Dead()
        {
            StopAllDebuff();
            RemoveAllDebuff();
            currentState = CombatState.Dead;
            isDead = true;
        }

        // ü���� ȸ���մϴ�.
        public void Heal(int healPoint)
        {
            CurrentHp += healPoint;
        }

        #region Debuff
        // ������� �޽��ϴ�.
        public void TakeDebuff(DebuffType type, float duration)
        {
            // ���� ����� ������ ���缭 Ȱ��ȭ �մϴ�.
            switch (type)
            {
                case DebuffType.Stern:
                    if (isActunableDebuff) return;
                    {
                        // ���� ������� Ȱ��ȭ�մϴ�.
                        IEnumerator debuff = TakeStern(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Fear:
                    if (isActunableDebuff) return;
                    {
                        // ���� ������� Ȱ��ȭ�մϴ�.
                        IEnumerator debuff = TakeFear(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Temptation:
                    if (isActunableDebuff) return;
                    {
                        // ��Ȥ ������� Ȱ��ȭ�մϴ�.
                        IEnumerator debuff = TakeTemptation(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Paralysis:
                    {
                        // ���� ������� Ȱ��ȭ�մϴ�.
                        IEnumerator debuff = TakeParalysis(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Bloody:
                    {
                        // ���� ������� Ȱ��ȭ�մϴ�.
                        IEnumerator debuff = TakeBloody(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Curse:
                    {
                        // ���� ������� Ȱ��ȭ�մϴ�.
                        IEnumerator debuff = TakeCurse(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
            }
        }

        // ��� ������� Ȱ��ȭ�մϴ�.
        public void ReStartAllDebuff()
        {
            foreach (var debuff in debuffList)
            {
                StartCoroutine(debuff);
            }
        }

        // ��� ������� �ߴܽ�ŵ�ϴ�.
        public void StopAllDebuff()
        {
            foreach (var debuff in debuffList)
            {
                StopCoroutine(debuff);
            }
        }

        // ��� ������� �����մϴ�.
        public void RemoveAllDebuff()
        {
            debuffList.Clear();
            characterUI.debuffUI.ResetAllDebuff();
        }

        // �����Ҷ��� �ൿ�Դϴ�.
        private IEnumerator TakeStern(float duration)
        {
            // ���� ���¸� �ൿ�Ұ� ���·� �����մϴ�.
            currentState = CombatState.Actunable;
            isActunableDebuff = true;
            currentDebuff = DebuffType.Stern;
            // ���� UI�� ǥ���մϴ�.
            characterUI.debuffUI.InitDebuff(DebuffType.Stern);
            // �������� ���ȿ��� �ƹ��� �ൿ�� ���� �ʽ��ϴ�.

            float time = 0;
            while (true)
            {
                // ���� UI�� �ð��� ǥ���մϴ�.
                characterUI.debuffUI.ShowDebuff(DebuffType.Stern, duration - time);
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
                if (time >= duration)
                {
                    break;
                }
            }

            // ������ ������ UI�� ������Ʈ�ϰ� �ൿ�� �����մϴ�.
            currentState = CombatState.Actable;
            isActunableDebuff = false;
            currentDebuff = DebuffType.Defualt;
            characterUI.debuffUI.ReleaseDebuff(DebuffType.Stern);
        }


        // ������ �ൿ�Դϴ�.
        private IEnumerator TakeFear(float duration)
        {
            // ���� ���¸� �ൿ�Ұ� ���·� �����մϴ�.
            currentState = CombatState.Actunable;
            isActunableDebuff = true;
            currentDebuff = DebuffType.Fear;
            // ���� UI�� ǥ���մϴ�.
            characterUI.debuffUI.InitDebuff(DebuffType.Fear);

            // ������ ���� �̵��ӵ��� 70% �ӵ��� ��󿡰Լ� �־����ϴ�.
            float defaultMovementSpeed = status.MovementSpeed;
            UpdateMovementSpeed(status.MovementSpeed * 0.7f);

            float time = 0;
            while (true)
            {
                // ���� UI�� �ð��� ǥ���մϴ�.
                characterUI.debuffUI.ShowDebuff(DebuffType.Fear, duration - time);
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
                if (time >= duration)
                {
                    break;
                }
            }

            // ������ ������ UI�� ������Ʈ�ϰ� �̵��ӵ��� �ǵ����� �ൿ�� �����մϴ�.
            currentState = CombatState.Actable;
            isActunableDebuff = false;
            currentDebuff = DebuffType.Defualt;
            UpdateMovementSpeed(defaultMovementSpeed);
            characterUI.debuffUI.ReleaseDebuff(DebuffType.Fear);
        }

        // ��Ȥ�� �ൿ�Դϴ�.
        private IEnumerator TakeTemptation(float duration)
        {
            // ���� ���¸� �ൿ�Ұ� ���·� �����մϴ�.
            currentState = CombatState.Actunable;
            isActunableDebuff = true;
            currentDebuff = DebuffType.Temptation;
            // ��Ȥ UI�� ǥ���մϴ�.
            characterUI.debuffUI.InitDebuff(DebuffType.Temptation);

            // ��Ȥ�� ���� �̵��ӵ��� 30% �ӵ��� ��󿡰� �ٰ����ϴ�.
            float defaultMovementSpeed = status.MovementSpeed;
            UpdateMovementSpeed(status.MovementSpeed * 0.3f);

            float time = 0;
            while (true)
            {
                // ��Ȥ UI�� �ð��� ǥ���մϴ�.
                characterUI.debuffUI.ShowDebuff(DebuffType.Temptation, duration - time);
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
                if (time >= duration)
                {
                    break;
                }
            }

            // ��Ȥ�� ������ UI�� ������Ʈ�ϰ� �̵��ӵ��� �ǵ����� �ൿ�� �����մϴ�.
            currentState = CombatState.Actable;
            isActunableDebuff = false;
            currentDebuff = DebuffType.Defualt;
            UpdateMovementSpeed(defaultMovementSpeed);
            characterUI.debuffUI.ReleaseDebuff(DebuffType.Temptation);
        }

        // ���� �� �ൿ�Դϴ�.
        private IEnumerator TakeParalysis(float duration)
        {
            // ���� UI�� ǥ���մϴ�.
            characterUI.debuffUI.InitDebuff(DebuffType.Paralysis);

            // ����� ���� �̵��ӵ��� 0���� ����ϴ�.
            float defaultMovementSpeed = status.MovementSpeed;
            UpdateMovementSpeed(0);

            float time = 0;
            while (true)
            {
                // ���� UI�� �ð��� ǥ���մϴ�.
                characterUI.debuffUI.ShowDebuff(DebuffType.Paralysis, duration - time);
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
                if (time >= duration)
                {
                    break;
                }
            }

            // ���� ������ UI�� ������Ʈ �ϰ� �̵��ӵ��� �ǵ����ϴ�.
            UpdateMovementSpeed(defaultMovementSpeed);
            characterUI.debuffUI.ReleaseDebuff(DebuffType.Paralysis);
        }

        // ���� �� �ൿ�Դϴ�.
        private IEnumerator TakeBloody(float duration)
        {
            // ���� UI�� ǥ���մϴ�.
            characterUI.debuffUI.InitDebuff(DebuffType.Bloody);

            // ������ �� �ʸ��� �ִ�ü���� 2%�� �����մϴ�.
            int bloodyDamage = status.MaxHp / 50;

            float time = 0;
            while (true)
            {
                if (time % 1 == 0)
                {
                    TakeDamage(bloodyDamage);
                }
                // ���� UI�� �ð��� ǥ���մϴ�.
                characterUI.debuffUI.ShowDebuff(DebuffType.Bloody, duration - time);
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
                if (time >= duration)
                {
                    break;
                }
            }

            // ������ ������ UI�� ������Ʈ�մϴ�.
            characterUI.debuffUI.ReleaseDebuff(DebuffType.Bloody);
        }


        // ���� �� �ൿ�Դϴ�.
        private IEnumerator TakeCurse(float duration)
        {
            // ���� UI�� ǥ���մϴ�.
            characterUI.debuffUI.InitDebuff(DebuffType.Curse);

            // ���ִ� �߰� �������� �Խ��ϴ�.
            isCursed = true;

            float time = 0;
            while (true)
            {
                // ���� UI�� �ð��� ǥ���մϴ�.
                characterUI.debuffUI.ShowDebuff(DebuffType.Curse, duration - time);
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
                if (time >= duration)
                {
                    break;
                }
            }

            // UI��������Ʈ�ϰ� ���ֻ��¿��� �ǵ����ϴ�.
            characterUI.debuffUI.ReleaseDebuff(DebuffType.Curse);
            isCursed = false;
        }
        #endregion

        // �ൿ���� ��ġ�� ���ݿ� �°� ������Ʈ�մϴ�.
        public void UpdateBehaviour()
        {
            // �̵��ൿ�� ���ݹ����� �̵��ӵ��� �����մϴ�.
            NavMeshAgent nav = GetComponent<NavMeshAgent>();
            nav.speed = status.MovementSpeed;
            nav.stoppingDistance = status.AttackRange;

            // �����ൿ�� ���ݼӵ��� �����մϴ�.
            Controller controller = GetComponent<Controller>();
            controller.animator.SetFloat("AttackSpeed", status.AttackSpeed);
            controller.attack.attackDelay = controller.attack.defaultAttackAnimLength / status.AttackSpeed;
        }

        // �̵��ӵ��� ����Ǿ����� �̵��ൿ���� �����մϴ�.
        public void UpdateMovementSpeed(float speed)
        {
            status.MovementSpeed = speed;

            NavMeshAgent nav = GetComponent<NavMeshAgent>();
            nav.speed = status.MovementSpeed;
        }

        // ���ݹ����� ����Ǿ����� �����ൿ���� �����մϴ�.
        public void UpdateAttackRange(float range)
        {
            status.AttackRange = range;

            NavMeshAgent nav = GetComponent<NavMeshAgent>();
            nav.stoppingDistance = status.AttackRange;
        }

        // ���ݼӵ��� ����Ǿ����� �����ൿ���� �����մϴ�.
        public void UpdateAttackSpeed(float speed)
        {
            status.AttackSpeed = speed;

            Controller controller = GetComponent<Controller>();
            controller.animator.SetFloat("AttackSpeed", status.AttackSpeed);
            controller.attack.attackDelay = controller.attack.defaultAttackAnimLength / status.AttackSpeed;
        }
    }
}