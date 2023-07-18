using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ ���� ��æƮ ������
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/HelmetData", order = 3)]
    public class HelmetIncantData : IncantData
    {
        [Header("Attribute")]
        public int hpPoint;                 // ü�� ��ġ
        public int defencePoint;            // ��� ��ġ
        public float decreseCriticalDamage; // ġ��Ÿ ������ ������
        public float evasionCritical;       // ġ��Ÿ ȸ����
    }
}