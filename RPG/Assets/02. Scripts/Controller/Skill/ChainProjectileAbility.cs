using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;
using RPG.Battle.Core;

/*
 * ü�� ����ü ȿ�� ����Ʈ Ŭ����
 */

namespace RPG.Battle.Ability
{
    public class ChainProjectileAbility : ChainAbility
    {
        [SerializeField] float speed;                   // ����ü �ӵ�
        [SerializeField] float distanceCheck;           // �Ÿ� üũ 
        [SerializeField] int hitChainAbilityID = -1;      // ����ü�� ������ ��� �߰� ȿ�� ����Ʈ ID

        EnemyController currentTarget;  // ���� ���

        int chainHitCount = 0; // ���� ü�� Ƚ��

        // ����� �����մϴ�.
        public override void SetTarget(EnemyController target)
        {
            base.SetTarget(target);
            currentTarget = targetList[0];
        }

        public override void ReleaseAbility()
        {
            base.ReleaseAbility();

        }

        void Update()
        {
            // ����ü�� ����� �ٶ󺸰� �����ġ�� �̵��մϴ�.
            transform.LookAt(currentTarget.transform.position + abilityPositionOffset);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (Vector3.Distance(this.transform.position, currentTarget.transform.position + abilityPositionOffset) < distanceCheck)
                // ����� ����ü �Ÿ��� ������ġ�� �ִٸ�
            {
                if (hitChainAbilityID != -1)
                    // �߰� ü�� ȿ�� ����Ʈ�� �ִٸ�
                {
                    // ������Ʈ Ǯ���� �����ɴϴ�.
                    BattleManager.ObjectPool.GetAbility(hitChainAbilityID, this.transform, chainAction, null, Space.World);
                }
                // ���� �̺�Ʈ�� ȣ���մϴ�.
                hitAction.Invoke(currentTarget.battleStatus);
                chainHitCount++;
                // ü�� ���� ī��Ʈ�� Ÿ�� ����Ʈ ī��Ʈ�� �����ϴٸ�
                if (targetList.Count == chainHitCount)
                {
                    chainHitCount = 0;
                    targetList.Clear();
                    // ȿ�� ����Ʈ�� ��ȯ�մϴ�.
                    BattleManager.ObjectPool.ReturnAbility(this);
                    return;
                }

                // ���� ����� Ÿ�ٸ���Ʈ�� ����������� �����մϴ�.
                currentTarget = targetList[chainHitCount];
            }
        }
    }
}