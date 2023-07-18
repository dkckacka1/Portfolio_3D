using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��� �ٴ� ��æƮ ������ Ŭ����
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/DefaultData", order = 0)]
    public class IncantData : Data
    {
        public string className;                            // ������ Ŭ���� �̸�
        public IncantType incantType;                       // ����, ���� ��æƮ Ÿ��
        public EquipmentItemType itemType;                  // ��� ��� Ÿ���� ��æƮ����
        public TierType incantTier = TierType.Normal;       // ��æƮ�� ���
        public string incantName;                           // ��æƮ�� �̸�
        [Header("Ability")]
        public bool isIncantAbility;                        // ��æƮ�� ��ų�� ���� �پ� �ִ��� ����
        [TextArea()]
        public string abilityDesc;                          // ��æƮ�� ����
        public Sprite abilityIcon;                          // ��æƮ�� ������
    }

}