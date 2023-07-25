using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ��æƮ�� ��æƮ Ŭ����
 */

namespace RPG.Character.Equipment
{
    public class ArmorIncant : Incant
    {
        public int hpPoint;         // ü�� ��ġ
        public int defencePoint;    // �� ��ġ
        public float movementSpeed; // �̵��ӵ�
        public float evasionPoint;  // ȸ����

        // ���� �����Ϳ� �´� ���� ��æƮ ������
        public ArmorIncant(ArmorIncantData data) : base(data)
        {
            hpPoint = data.hpPoint;
            defencePoint = data.defencePoint;
            movementSpeed = data.movementSpeed;
            evasionPoint = data.evasionPoint;
        }

        // �ʴ� ���ֵ� ȿ��
        public virtual void PerSecEvent(BattleStatus status)
        {
        }

        // ������ ���� �� ȿ��
        public virtual void TakeDamageEvent(BattleStatus character, BattleStatus target)
        {
        }

        // ���� ��æƮ�� ���� ���� ����
        public override string GetAddDesc()
        {
            string returnStr = string.Empty;

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
                    returnStr = $"�̵� �ӵ�(+{movementSpeed})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"�̵� �ӵ�(+{movementSpeed})");
                }
            }

            if (evasionPoint > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"ȸ����(+{evasionPoint * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"ȸ����(+{evasionPoint * 100}%)");
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
                    returnStr = $"�̵� �ӵ�({movementSpeed})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"�̵� �ӵ�({movementSpeed})");
                }
            }

            if (evasionPoint < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"ȸ����({evasionPoint * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"ȸ����({evasionPoint * 100}%)");
                }
            }


            return returnStr;

        }
    }
}