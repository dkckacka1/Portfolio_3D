using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���ʿ� �ٴ� ��æƮ ������
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/ArmorData", order = 2)]
    public class ArmorIncantData : IncantData
    {
        [Header("Attribute")]
        public int hpPoint;         // ü�� ����Ʈ
        public int defencePoint;    // ��� ����Ʈ
        public float movementSpeed; // �̵��ӵ�
        public float evasionPoint;  // ȸ����
    }
}