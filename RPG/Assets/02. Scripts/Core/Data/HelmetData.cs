using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��� ������ ������ Ŭ����
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewHelmet", menuName = "CreateScriptableObject/CreateHelmet", order = 3)]
    public class HelmetData : EquipmentData
    {
        public int defencePoint;                                // �� ��ġ
        public int hpPoint;                                     // ü�� ��ġ
        [Range(0f, 0.2f)] public float decreseCriticalDamage;   // ġ��Ÿ ���̹� ���� 
        [Range(0f, 0.2f)] public float evasionCritical;         // ġ��Ÿ ȸ����
    }
}
