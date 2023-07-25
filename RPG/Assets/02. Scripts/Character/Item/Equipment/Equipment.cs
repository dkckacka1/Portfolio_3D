using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

/*
 * �������� Ŭ����
 */

namespace RPG.Character.Equipment
{
    public abstract class Equipment
    {
        public int reinforceCount = 0;              // ����� ��ȭ ��ġ
        public string itemName;                     // �������� �̸�
        public EquipmentItemType equipmentType;     // �������� Ÿ��
        public TierType equipmentTier;              // �������� ���
        public string description;                  // �������� ����

        public EquipmentData data;                  // �������� ������

        public Incant prefix;       // ���� ��æƮ
        public Incant suffix;       // ���� ��æƮ

        // ������������ ���� ������ �κ񿡼� ������ �Ѿ�� ĳ���� ������ ���� ����ĳ���ͷ� ���鶧 ���
        public Equipment(Equipment equipment)
        {
            data = equipment.data;
            itemName = equipment.itemName;
            equipmentType = equipment.equipmentType;
            equipmentTier = equipment.equipmentTier;
            description = equipment.description;

            reinforceCount = equipment.reinforceCount;
            prefix = equipment.prefix;
            suffix = equipment.suffix;
        }

        // �������� �����ͷ� ���� ������ ���������Ϳ��� �κ�ĳ���͸� ���鶧 ���
        public Equipment(EquipmentData data)
        {
            this.data = data;
            itemName = data.EquipmentName;
            equipmentType = data.equipmentType;
            equipmentTier = data.equipmentTier;
            description = data.description;
        }

        // �������� �����͸� ��ü�մϴ�.
        public virtual void ChangeData(EquipmentData data)
        {
            RemoveAllIncant();
            reinforceCount = 0;

            this.data = data;
            itemName = data.EquipmentName;
            equipmentType = data.equipmentType;
            equipmentTier = data.equipmentTier;
            description = data.description;
        }

        #region Incant
        // ��æƮ ID�� ��æƮ�� ã�� �������ۿ� �ο��մϴ�.
        public void Incant(int incantID)
        {
            Incant incant = GameManager.Instance.incantDic[incantID];

            if (incant == null)
            {
                Debug.Log("�߸��� ��æƮ ȣ��");
                return;
            }

            // ��æƮ Ÿ�԰� ������ Ÿ���� �´��� Ȯ��
            if (this.equipmentType != incant.itemType)
            {
                Debug.Log("��� Ÿ�԰� ��æƮ ��� Ÿ���� �ٸ��ϴ�.");
                return;
            }

            switch (incant.incantType)
            {
                case IncantType.prefix:
                    prefix = incant;
                    break;
                case IncantType.suffix:
                    suffix = incant;
                    break;
            }
        }

        // ��æƮ Ŭ������ �������ۿ� ��æƮ�մϴ�.
        public void Incant(Incant incant)
        {
            if (incant == null)
            {
                Debug.Log("�߸��� ��æƮ ȣ��");
                return;
            }

            // ��æƮ Ÿ�԰� ������ Ÿ���� �´��� Ȯ��
            if (this.equipmentType != incant.itemType)
            {
                Debug.Log("��� Ÿ�԰� ��æƮ ��� Ÿ���� �ٸ��ϴ�.");
                return;
            }

            switch (incant.incantType)
            {
                case IncantType.prefix:
                    prefix = incant;
                    break;
                case IncantType.suffix:
                    suffix = incant;
                    break;
            }
        }

        // ��� ��æƮ�� �����մϴ�.
        public void RemoveAllIncant()
        {
            if (prefix != null)
            {
                prefix = null;
            }

            if (suffix != null)
            {
                suffix = null;
            }
        }

        // ��æƮ�� �ִ��� ����
        public bool isIncant()
        {
            return (prefix != null || suffix != null);
        }
        #endregion

        // ���� ��æƮID�� ã���ϴ�.
        public int GetPrefixIncantID()
        {
            if (prefix == null)
            {
                return -1;
            }

            return prefix.incantID;
        }

        // ���� ��æƮID�� ã���ϴ�.
        public int GetSuffixIncantID()
        {
            if (suffix == null)
            {
                return -1;
            }

            return suffix.incantID;
        }

        // ��æƮ�� Ư���� ȿ���� �ٴ��� ����
        public bool hasAbilitySkill()
        {
            return (hasPrefixAbilitySkill() || hasSuffixAbilitySkill());
        }

        // ���� ��æƮ�� ȿ���� �ٴ��� ����
        public bool hasPrefixAbilitySkill()
        {
            if (prefix == null) return false;
            if (!prefix.isIncantAbility) return false;

            return true;
        }

        // ���� ��æƮ�� ȿ���� �ٴ��� ����
        public bool hasSuffixAbilitySkill()
        {
            if (suffix == null) return false;
            if (!suffix.isIncantAbility) return false;

            return true;
        }

        // ��ȭ�� �Ǿ��ִ��� ����
        public bool isReinforce()
        {
            return !(reinforceCount == 0);
        }

        // ��� ��ȭ�մϴ�.
        public void ReinforceItem()
        {
            reinforceCount++;
        }

        // ��ȭ ��ġ�� �����մϴ�.
        public void RemoveReinforce()
        {
            reinforceCount = 0;
        }

        public override string ToString()
        {
            return
                $"����̸� : {itemName}\n" +
                $"���Ƽ�� : {equipmentTier}\n" +
                $"������� : {equipmentType}\n" +
                $"������æƮ : {(prefix != null ? prefix.incantName : "����")}\n" +
                $"������æƮ : {(suffix != null ? suffix.incantName : "����")}";
        }

        // ��� ��� ���ڿ��� �����մϴ�.
        public string ToStringTier()
        {
            switch (equipmentTier)
            {
                case TierType.Normal:
                    return "�븻";
                case TierType.Rare:
                    return "����";
                case TierType.Unique:
                    return "����ũ";
                case TierType.Legendary:
                    return "����";
            }

            return "";
        }

        // ��� Ÿ�� ���ڿ��� �����մϴ�.
        public string ToStringEquipmentType()
        {
            switch (equipmentType)
            {
                case EquipmentItemType.Weapon:
                    return "����";
                case EquipmentItemType.Armor:
                    return "����";
                case EquipmentItemType.Pants:
                    return "����";
                case EquipmentItemType.Helmet:
                    return "����";
            }

            return "";
        }
    }

}