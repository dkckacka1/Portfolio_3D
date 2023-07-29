using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Character.Status;

/*
 * ���� ��æƮ�� ��æƮ Ŭ����
 */

public class WeaponIncant : Incant
{
    public int attackDamage;        // ���ݷ�
    public float attackSpeed;       // ���� �ӵ�
    public float attackRange;       // ���� ����
    public float movementSpeed;     // �̵� �ӵ�
    public float criticalChance;    // ġ��Ÿ Ȯ��
    public float criticalDamage;    // ġ��Ÿ ������
    public float attackChance;      // ���߷�

        // ���� �����Ϳ� �´� ���� ��æƮ ������
    public WeaponIncant(WeaponIncantData data) : base(data)
    {
        attackDamage = data.attackDamage;
        attackSpeed = data.attackSpeed;
        attackRange = data.attackRange;
        movementSpeed = data.movementSpeed;
        criticalChance = data.criticalChance;
        criticalDamage = data.criticalDamage;
        attackChance = data.movementSpeed;
    }

    // ���� �� ���� ��ų ȿ��
    public virtual void AttackEvent(BattleStatus player, BattleStatus enemy)
    {
    }

        // ���� ��æƮ�� ���� ���� ����
    public override string GetAddDesc()
    {
        string returnStr = "";
        if (attackDamage > 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"���ݷ�(+{attackDamage})";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"���ݷ�(+{attackDamage})");
            }
        }

        if (attackSpeed > 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"���ݼӵ�(+{attackSpeed})";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"���ݼӵ�(+{attackSpeed})");
            }
        }

        if (attackRange > 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"���ݹ���(+{attackRange})";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"���ݹ���(+{attackRange})");
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

        if (criticalChance > 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"ġ��Ÿ ���߷�(+{criticalChance * 100}%)";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"ġ��Ÿ ���߷�(+{criticalChance * 100}%)");
            }
        }

        if (criticalDamage > 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"ġ��Ÿ ������(+{criticalDamage * 100}%)";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"ġ��Ÿ ������(+{criticalDamage * 100}%)");
            }
        }

        if (attackChance > 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"���߷�(+{attackChance * 100}%)";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"���߷�(+{attackChance * 100}%)");
            }
        }

        return returnStr;
    }

        // ���� ��æƮ�� ���� ���� ����
    public override string GetMinusDesc()
    {
        string returnStr = "";
        if (attackDamage < 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"���ݷ�({attackDamage})";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"���ݷ�({attackDamage})");
            }
        }

        if (attackSpeed < 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"���ݼӵ�({attackSpeed})";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"���ݼӵ�({attackSpeed})");
            }
        }

        if (attackRange < 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"���ݹ���({attackRange})";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"���ݹ���({attackRange})");
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

        if (criticalChance < 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"ġ��Ÿ ���߷�({criticalChance * 100}%)";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"ġ��Ÿ ���߷�({criticalChance * 100}%)");
            }
        }

        if (criticalDamage < 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"ġ��Ÿ ������({criticalDamage * 100}%)";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"ġ��Ÿ ������({criticalDamage * 100}%)");
            }
        }

        if (attackChance < 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"���߷�({attackChance * 100}%)";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"���߷�({attackChance * 100}%)");
            }
        }

        return returnStr;
    }
}
