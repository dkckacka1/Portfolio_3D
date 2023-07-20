using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

/*
 * ���� ��æƮ ���� ��Ź��
 */

namespace RPG.Character.Equipment
{
    public class Dull_Weapon : WeaponIncant
    {
        public Dull_Weapon(WeaponIncantData data) : base(data)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            // ���ݽ� 30% Ȯ���� ��븦 2�ʰ� ������ŵ�ϴ�.
            if (MyUtility.ProbailityCalc(70f, 0f, 100f))
            {
                enemy.TakeDebuff(DebuffType.Stern, 2f);
            }
        }
    }

}