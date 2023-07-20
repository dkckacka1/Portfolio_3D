using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Battle.Core;

/*
 * ���� ��æƮ ���� ����
 */

namespace RPG.Character.Equipment
{
    public class Fear_Pants : PantsIncant
    {
        public Fear_Pants(PantsIncantData data) : base(data)
        {
            // ��ų ��Ÿ���� 15��
            skillCoolTime = 15f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            // ���� ����Ʈ ���
            var ability = BattleManager.ObjectPool.GetAbility(2, player.transform, Fear);
        }

        public void Fear(BattleStatus character)
        {
            // �ֺ��� ���� 4�� ���� ������ ������ �մϴ�.
            if (character.status is EnemyStatus)
            {
                character.TakeDebuff(DebuffType.Fear, 4f);
            }
        }
    }
}