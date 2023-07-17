using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ������ ������ Ŭ����
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewPants", menuName = "CreateScriptableObject/CreatePants", order = 4)]
    public class PantsData : EquipmentData
    {
        public int defencePoint;                        // �� ��ġ
        public int hpPoint;                             // ü�� ��ġ
        [Range(0f, 0.5f)] public float movementSpeed;   // �̵��ӵ� ��ġ
    }
}
