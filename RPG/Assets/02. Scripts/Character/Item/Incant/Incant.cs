using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

/*
 * ��� �ٴ� ��æƮ�� �߻� Ŭ����
 */

namespace RPG.Character.Equipment
{
    public abstract class Incant
    {
        public int incantID;    // ��æƮ ���̵�

        // ������ ���� �� �ִ��� Ÿ��
        public EquipmentItemType itemType;
        // ��æƮ ���
        public TierType incantTier;
        // ���� ���� Ÿ��
        public IncantType incantType;

        // ��æƮ�� �̸�
        public string incantName;

        public bool isIncantAbility;    // ��æƮ�� ���� ��ų�� �ٴ��� ����
        public string abilityDesc;      // ��æƮ ��ų ����
        public Sprite abilityIcon;      // ��æƮ ��ų ������

        // ��æƮ �����Ϳ� �°� ��æƮ�� �����մϴ�.
        public Incant(IncantData data)
        {
            incantID = data.ID;
            incantType = data.incantType;
            itemType = data.itemType;
            incantTier = data.incantTier;
            incantName = data.incantName;
            isIncantAbility = data.isIncantAbility;
            abilityDesc = data.abilityDesc;
            abilityIcon = data.abilityIcon;
        }


        // ��æƮ�� ���� ���� ����
        public abstract string GetAddDesc();
        // ��æƮ�� ���� ���� ����
        public abstract string GetMinusDesc();
    }

}