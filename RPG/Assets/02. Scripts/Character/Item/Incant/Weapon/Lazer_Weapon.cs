using RPG.Battle.Core;
using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ��æƮ ���� ������
 */

namespace RPG.Character.Equipment
{
    public class Lazer_Weapon : WeaponIncant
    {
        public Lazer_Weapon(WeaponIncantData data) : base(data)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            // ���� �� 20 �������� ���� �������� �߻��մϴ�.
            var ability = BattleManager.ObjectPool.GetAbility(6);
            ability.InitAbility(player.transform, HitLazer);
        }

        public void HitLazer(BattleStatus target)
        {
            // ������ 20
            target.TakeDamage(20);
        }
    }

}