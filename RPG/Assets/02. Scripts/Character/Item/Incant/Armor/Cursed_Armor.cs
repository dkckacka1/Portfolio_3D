using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ��æƮ ���� ����
 */

namespace RPG.Character.Equipment
{
    public class Cursed_Armor : ArmorIncant
    {
        public Cursed_Armor(ArmorIncantData data) : base(data)
        {
        }

        public override void TakeDamageEvent(BattleStatus mine, BattleStatus whoHitMe)
        {
            // �ǰݽ� Ÿ���� ����� 5�ʰ� ���ֿ� ��Ʈ���ϴ�.
            whoHitMe.TakeDebuff(DebuffType.Curse, 5f);
        }
    }

}