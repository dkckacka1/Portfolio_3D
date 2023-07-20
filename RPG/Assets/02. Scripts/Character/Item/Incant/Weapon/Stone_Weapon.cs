using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Battle.Core;

/*
 * ���� ��æƮ ���� ������ 
 */

namespace RPG.Character.Equipment
{
    public class Stone_Weapon : WeaponIncant
    {
        public Stone_Weapon(WeaponIncantData data) : base(data)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            // ���� �� 10 �������� ������ ȭ���� �߻��մϴ�.
            var ability = BattleManager.ObjectPool.GetAbility(1);
            ability.InitAbility(player.transform, HitStone );
        }

        public void HitStone(BattleStatus target)
        {
            // ������ 10
            target.TakeDamage(10);
        }
    }
}