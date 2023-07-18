using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ��æƮ�� ��æƮ Ŭ����
 */

namespace RPG.Character.Equipment
{
    public class PantsIncant : Incant
    {
        public int hpPoint;              // ü�� ��ġ
        public int defencePoint;         // ��
        public float movementSpeed;      // �̵� �ӵ�

        public float skillCoolTime; // ��ų ��Ÿ��

        // ���� �����Ϳ� �´� ���� ��æƮ ������
        public PantsIncant(PantsIncantData data) : base(data)
        {
            hpPoint = data.hpPoint;
            defencePoint = data.defencePoint;
            movementSpeed = data.movementSpeed;

    }

        // �̵��� ������ ȿ��
        public virtual void MoveEvent(BattleStatus player)
        {
            Debug.Log("MoveEvent is Nothing");
        }

        // ���� ��Ƽ�� ��ų
        public virtual void ActiveSkill(BattleStatus player)
        {
            Debug.Log("Pants ActiveSkill is Nothing");
        }

        // ���� ��æƮ�� ���� ���� ����
        public override string GetAddDesc()
        {
            string returnStr = "";
            if (hpPoint > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"ü��(+{hpPoint})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"ü��(+{hpPoint})");
                }
            }

            if (defencePoint > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"����(+{defencePoint})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"����(+{defencePoint})");
                }
            }

            if (movementSpeed > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"�̵��ӵ�(+{movementSpeed})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"�̵��ӵ�(+{movementSpeed})");
                }
            }

            return returnStr;
        }

        // ���� ��æƮ�� ���� ���� ����
        public override string GetMinusDesc()
        {
            string returnStr = "";
            if (hpPoint < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"ü��({hpPoint})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"ü��({hpPoint})");
                }
            }

            if (defencePoint < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"����({defencePoint})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"����({defencePoint})");
                }
            }

            if (movementSpeed < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"�̵��ӵ�({movementSpeed})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"�̵��ӵ�({movementSpeed})");
                }
            }

            return returnStr;
        }
    }
}