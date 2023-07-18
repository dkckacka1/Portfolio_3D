using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���⿡ �ٴ� ��æƮ ������
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/WeaponData", order = 1)]
    public class WeaponIncantData : IncantData
    {
        [Header("Attribute")]
        public int attackDamage;        // ���ݷ� ��ġ
        public float attackSpeed;       // ���� �ӵ�
        public float attackRange;       // ���� ����
        public float movementSpeed;     // �̵� �ӵ�
        public float criticalChance;    // ġ��Ÿ Ȯ��
        public float criticalDamage;    // ġ��Ÿ ������
        public float attackChance;      // ���߷�
    }
}