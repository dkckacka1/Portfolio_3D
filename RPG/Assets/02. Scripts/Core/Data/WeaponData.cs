using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ������ ������ Ŭ����
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "CreateScriptableObject/CreateWeapon", order = 1)]
    public class WeaponData : EquipmentData
    {
        public int weaponApparenceID;   // ���� ���� ID

        public int attackDamage;                            // ���ݷ� ��ġ
        [Range(1, 2.5f)] public float attackSpeed;          // ���� �ӵ�
        [Range(1, 5f)] public float attackRange;            // ���� ����
        [Range(1, 5f)] public float movementSpeed;          // �̵� �ӵ�
        [Range(0, 1f)] public float criticalChance;         // ġ��Ÿ Ȯ��
        [Range(0, 1f)] public float criticalDamage;         // ġ��Ÿ ������
        [Range(0.6f, 1.2f)] public float attackChance;      // ���߷�

        public weaponHandleType weaponHandleType = weaponHandleType.OneHandedWeapon; // ���� Ÿ�� (�Ѽհ����� ��հ�����)
    }
}
