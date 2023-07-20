using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Character.Status;

/*
 * ��� ��æƮ ���� ���
 */

namespace RPG.Character.Equipment
{
    public class Regenerative_Helmet : HelmetIncant
    {
        public Regenerative_Helmet(HelmetIncantData data) : base(data)
        {
            // ��ų ��Ÿ���� 20��
            skillCoolTime = 20f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            // ��Ƽ�� ��ų : ��� �� ü���� 100 ȸ���մϴ�.
            player.Heal(100);
        }
    }
}
