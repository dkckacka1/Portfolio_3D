using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Core;
using RPG.Character.Status;
using RPG.Main.Audio;

/*
 * ȿ�� ����Ʈ�� �⺻ �߻� Ŭ����
 */

namespace RPG.Battle.Ability
{
    public abstract class Ability : MonoBehaviour
    {
        public int abilityID;               // ȿ�� ����Ʈ ID
        public string abilityName;          // ȿ�� ����Ʈ �̸�
        [Space()]
        [TextArea()]
        public string abilityDesc;          // ȿ�� ����Ʈ ����
        public float abilityTime;           // ȿ�� ����Ʈ �ð�
        public string AbilitySoundName;     // ȿ�� ����Ʈ ����� �̸�

        [HideInInspector] public ParticleSystem particle;   // ��ƼŬ �ý���
        protected UnityAction<BattleStatus> hitAction;      // ����Ʈ�� ��󿡰� ���߽� �׼�
        protected UnityAction<BattleStatus> chainAction;    // ����Ʈ�� ���� �׼�

        // ó���� ��ų�� ��� ��Ÿ�� ������ 
        public Vector3 abilityPositionOffset;

        private void Awake()
        {
            particle = GetComponent<ParticleSystem>();
        }

        // ȿ�� ����Ʈ�� Ȱ��ȭ �Ǹ� ���带 ����ϰ� ��ȯ �ڷ�ƾ�� �����մϴ�.
        protected virtual void OnEnable()
        {
            PlaySound();
            StartCoroutine(ReleaseTimer());
        }

        // ȿ�� ����Ʈ�� �ʱ�ȭ �մϴ�.
        public virtual void InitAbility(Transform startPos,
            UnityAction<BattleStatus> hitAction = null,
            UnityAction<BattleStatus> chainAction = null,
            Space space = Space.Self)
        {
            // ����Ʈ ������ġ�� ����� ��ġ�� ���� ������ġ������ �����մϴ�.
            if (space == Space.Self)
            {
                this.transform.localPosition = startPos.localPosition;
            }
            else
            {
                this.transform.localPosition = startPos.position;
            }


            // �����ġ�� �̵��� offset��ŭ �߰� �̵��մϴ�.
            this.transform.Translate(abilityPositionOffset);
            this.hitAction = hitAction;
            this.chainAction = chainAction;
        }

        // ȿ�� ����Ʈ�� ������Ʈ Ǯ�� ��ȯ�մϴ�.
        public virtual void ReleaseAbility()
        {
            BattleManager.ObjectPool.ReturnAbility(this);
        }

        // ��ȯ �ð���ŭ ��� �� ������Ʈ Ǯ�� ��ȯ�մϴ�.
        public IEnumerator ReleaseTimer()
        {
            yield return new WaitForSeconds(abilityTime);
            ReleaseAbility();
        }

        // �Ҹ��� ����մϴ�.
        public void PlaySound()
        {
            if (AbilitySoundName != string.Empty)
            {
                AudioManager.Instance.PlaySoundOneShot(AbilitySoundName);
            }
        }
    }
}