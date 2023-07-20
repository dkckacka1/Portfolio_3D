using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ��æƮ ���� �����
 */

namespace RPG.Character.Equipment
{
    public class Regenerative_Armor : ArmorIncant
    {
        public Regenerative_Armor(ArmorIncantData data) : base(data)
        {
        }

        public override void PerSecEvent(BattleStatus status)
        {
            // �� �ʸ��� ü���� 1ȸ���˴ϴ�.
            status.Heal(1);
        }
    }

}