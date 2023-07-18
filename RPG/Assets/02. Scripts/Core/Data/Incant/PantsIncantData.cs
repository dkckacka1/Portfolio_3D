using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ �ٴ� ��æƮ ������
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/PantsData", order = 4)]
    public class PantsIncantData : IncantData
    {
        [Header("Attribute")]
        public int hpPoint;             // ü�� ��ġ
        public int defencePoint;        // ���� ��ġ
        public float movementSpeed;     // �̵��ӵ�
    }
}