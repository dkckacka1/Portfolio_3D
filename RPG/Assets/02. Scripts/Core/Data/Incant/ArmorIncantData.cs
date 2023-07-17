using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : �ּ��� ���⼭����

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/ArmorData", order = 2)]
    public class ArmorIncantData : IncantData
    {
        [Header("Attribute")]
        public int hpPoint;
        public int defencePoint;
        public float movementSpeed;
        public float evasionPoint;
    }
}