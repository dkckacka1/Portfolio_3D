using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Ability;
using RPG.Battle.Control;
using RPG.Battle.Core;
using RPG.Character.Status;

/*
 * ���� ��æƮ ���� ������
 */

namespace RPG.Character.Equipment
{
    public class Electronic_Weapon : WeaponIncant
    {
        public Electronic_Weapon(WeaponIncantData data) : base(data)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            // ���ݽ� �ִ� 4����� ������ �帣���Ͽ� �������� �����ϴ�.
            var nearlyTarget = BattleManager.Instance.ReturnNearDistanceController<EnemyController>(player.transform);
            var ability = BattleManager.ObjectPool.GetAbility(4, player.transform, (status) => 
            {
                // ����� ����Ʈ�� �´´ٸ� �ٷ� ���� ����Ʈ �����ݴϴ�.
                BattleManager.ObjectPool.GetAbility(5, status.transform, (status) =>
                {
                }, null, Space.World);
                status.TakeDamage(15); 
            });

            // ���� ����� ������ �����մϴ�.
            (ability as ChainProjectileAbility).SetTarget(nearlyTarget);
        }
    }
}
