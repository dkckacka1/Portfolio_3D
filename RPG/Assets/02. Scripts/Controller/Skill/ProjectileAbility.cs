using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Control;
using RPG.Character.Status;
using RPG.Battle.Core;

/*
 * ����ü ȿ�� ����Ʈ Ŭ����
 */

namespace RPG.Battle.Ability
{
    public class ProjectileAbility : Ability
    {
        [SerializeField] float speed;               // ����ü �ӵ�
        [SerializeField] bool isPiercing = false;   // ����ü ���� ����

        [SerializeField] int hitAbilityID = -1;      // ����ü�� �������� �� �߰� ȿ�� ����Ʈ ID

        public override void InitAbility(Transform startPos, UnityAction<BattleStatus> hitAction, UnityAction<BattleStatus> chainAction = null, Space space = Space.Self)
        {
            // ����ü�� ������ ����ü�� �����ϴ� ����� �ٶ󺸰��ִ� �������� �����մϴ�.
            this.transform.rotation = startPos.rotation;
            base.InitAbility(startPos, hitAction, chainAction, space);
        }

        // Update is called once per frame
        void Update()
        {
            // �������� ����ü�� �̵���ŵ�ϴ�.
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
            // ����ü�� Ʈ���� �ݰ泻�� ������ �ݶ��̴��� �ִٸ�
        {
            var enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                var enemyStatus = enemyController.battleStatus;
                if (enemyStatus != null)
                    // �ش� �ݶ��̴��� ����Ʈ�ѷ��� ������ �ִٸ�
                {
                    // ���� �̺�Ʈ�� ȣ���մϴ�.
                    hitAction.Invoke(enemyStatus);
                    if (hitAbilityID >= 0)
                        // ���� ȿ�� ����Ʈ�� �����ϸ�
                    {
                        // ������Ʈ Ǯ���� ȿ�� ����Ʈ�� �����ɴϴ�.
                        BattleManager.ObjectPool.GetAbility(hitAbilityID, transform, chainAction);
                    }

                    if (isPiercing == false)
                        // ������ �ȵǴ� ����ü���
                    {
                        // ������Ʈ Ǯ�� ��ȯ�մϴ�.
                        BattleManager.ObjectPool.ReturnAbility(this);
                    }
                }
            }

        }
    }

}