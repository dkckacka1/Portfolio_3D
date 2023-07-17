using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

/*
 *  ������ ���� ������ Ŭ�����Դϴ�.
 */

namespace RPG.Core
{
    public class UserInfo
    {
        public int itemReinforceTicket;     // ��� ��ȭ��
        public int itemIncantTicket;        // ��� ��æƮ��
        public int itemGachaTicket;         //// ��� �̱��
        public int risingTopCount;          //
        public int energy;

        // Weapon
        public int lastedWeaponID;          // ���������� ������ ���� ID
        public int weaponReinforceCount;    // ���� ��ȭ ��ġ
        public int weaponPrefixIncantID;    // ���� ���� ��æƮ ID
        public int weaponSuffixIncantID;    // ���� ���� ��æƮ ID

        // Armor                               
        public int lastedArmorID;           // ���������� ������ ���� ID
        public int armorReinforceCount;     // ���� ��ȭ ��ġ
        public int armorPrefixIncantID;     // ���� ���� ��æƮ ID
        public int armorSuffixIncantID;     // ���� ���� ��æƮ ID

        // Helmet                              
        public int lastedHelmetID;          // ���������� ������ ��� ID
        public int helmetReinforceCount;    // ��� ��ȭ ��ġ
        public int helmetPrefixIncantID;    // ��� ���� ��æƮ ID
        public int helmetSuffixIncantID;    // ��� ���� ��æƮ ID

        // Pants                               
        public int lastedPantsID;           // ���������� ������ ���� ID
        public int pantsReinforceCount;     // ���� ��ȭ ��ġ
        public int pantsPrefixIncantID;     // ���� ���� ��æƮ ID
        public int pantsSuffixIncantID;     // ���� ���� ��æƮ ID

        // ĳ���� ������ ���� ���������� ������Ʈ �մϴ�.
        public void UpdateUserinfoFromStatus(PlayerStatus status)
        {
            lastedWeaponID = status.currentWeapon.data.ID;
            weaponReinforceCount = status.currentWeapon.reinforceCount;
            weaponPrefixIncantID = status.currentWeapon.GetPrefixIncantID();
            weaponSuffixIncantID = status.currentWeapon.GetSuffixIncantID();

            lastedArmorID = status.currentArmor.data.ID;
            armorReinforceCount = status.currentArmor.reinforceCount;
            armorPrefixIncantID = status.currentArmor.GetPrefixIncantID();
            armorSuffixIncantID = status.currentArmor.GetSuffixIncantID();

            lastedHelmetID = status.currentHelmet.data.ID;
            helmetReinforceCount = status.currentHelmet.reinforceCount;
            helmetPrefixIncantID = status.currentHelmet.GetPrefixIncantID();
            helmetSuffixIncantID = status.currentHelmet.GetSuffixIncantID();
                
            lastedPantsID = status.currentPants.data.ID;
            pantsReinforceCount = status.currentPants.reinforceCount;
            pantsPrefixIncantID = status.currentPants.GetPrefixIncantID();
            pantsSuffixIncantID = status.currentPants.GetSuffixIncantID();
        }
    }
}