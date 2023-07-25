using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��信 ���� ��æƮ Ŭ����
 */

namespace RPG.Character.Equipment
{
    public class HelmetIncant : Incant
    {
        public int hpPoint;                     // ü�� ��ġ
        public int defencePoint;                // �� ��ġ
        public float decreseCriticalDamage;     // ġ��Ÿ ������ ������
        public float evasionCritical;           // ȸ����

        public float skillCoolTime;             // ��ų ��Ÿ��

        // ��� �����Ϳ� ���� ������
        public HelmetIncant(HelmetIncantData data) : base(data)
        {
            hpPoint = data.hpPoint;
            defencePoint = data.defencePoint;
            decreseCriticalDamage = data.decreseCriticalDamage;
            evasionCritical = data.evasionCritical;
        }

        // ġ��Ÿ ���ݽ� ȿ��
        public virtual void criticalAttackEvent(BattleStatus player, BattleStatus enemy)
        {
        }

        // ��� ��Ƽ�� ��ų
        public virtual void ActiveSkill(BattleStatus player)
        {
        }

        // ��� ��æƮ�� ���� ���� ����
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

            if (decreseCriticalDamage > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"ġ��Ÿ����������(+{decreseCriticalDamage * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"ġ��Ÿ����������(+{decreseCriticalDamage * 100}%)");
                }
            }

            if (evasionCritical > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"ġ��Ÿȸ����(+{evasionCritical * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"ġ��Ÿȸ����(+{evasionCritical * 100}%)");
                }
            }

            return returnStr;
        }

        // ��� ��æƮ�� ���� ���� ����
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

            if (decreseCriticalDamage < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"ġ��Ÿ����������({decreseCriticalDamage * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"ġ��Ÿ����������({decreseCriticalDamage * 100}%)");
                }
            }

            if (evasionCritical < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"ġ��Ÿȸ����({evasionCritical * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"ġ��Ÿȸ����({evasionCritical * 100}%)");
                }
            }

            return returnStr;
        }
    }
}