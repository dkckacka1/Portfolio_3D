using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ������ ������ Ŭ����
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewArmor", menuName = "CreateScriptableObject/CreateArmor", order = 2)]
    public class ArmorData : EquipmentData
    {
        public int defencePoint;                        // �� ��ġ
        public int hpPoint;                             // ü�� ��ġ
        [Range(0f, 0.5f)] public float movementSpeed;   // �̵��ӵ�
        [Range(0f, 0.2f)] public float evasionPoint;    // ȸ�� ��ġ
    }
}