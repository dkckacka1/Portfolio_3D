using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;
using RPG.Character.Status;
using RPG.Character.Equipment;
using UnityEngine.EventSystems;
using RPG.Main.Audio;

/*
 * ���ξ��� UI �Ŵ��� Ŭ����
 */

namespace RPG.Main.UI
{
    public class MainSceneUIManager : MonoBehaviour
    {
        [Header("UI")]
        public CharacterAppearance appearance;      // �κ� ĳ���� ����

        [Space()]
        [SerializeField] TextMeshProUGUI reinforceText;             // ��ȭ Ƽ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI incantText;                // ��æƮ Ƽ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI itemGachaTicketText;       // ������ ��í Ƽ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI EnergyText;                // ������ Ƽ�� �ؽ�Ʈ

        [Header("Canvas")]
        [SerializeField] Canvas statusCanvas;   // �������ͽ� ĵ����

        private void Start()
        {
            // ���ξ��� ���Խ� �κ� ĳ���͸� �����ϰ� Ƽ�� �ؽ�Ʈ�� �����ݴϴ�.
            appearance.EquipWeapon(GameManager.Instance.Player.currentWeapon.weaponApparenceID, GameManager.Instance.Player.currentWeapon.handleType);
            UpdateTicketCount();
            AudioManager.Instance.PlayMusic("MainBackGroundMusic", true);
            GameSLManager.SaveToJSON(GameManager.Instance.UserInfo, Application.dataPath + @"\Userinfo.json");
        }

        #region ButtonPlugin
        // �ش� ĵ������ �����ݴϴ�.
        public void ShowUI(Canvas canvas)
        {
            canvas.gameObject.SetActive(true);
        }

        // �ش� ĵ������ ���ݴϴ�
        public void ReleaseUI(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
        }

        // �������� ���þ����� �̵��մϴ�.
        public void LoadStageChoiceScene()
        {
            SceneLoader.LoadStageChoiceScene();
        }

        #endregion

        // Ƽ�� �ؽ�Ʈ�� ������Ʈ�մϴ�.
        public void UpdateTicketCount()
        {
            this.itemGachaTicketText.text = $"{GameManager.Instance.UserInfo.itemGachaTicket}";
            this.reinforceText.text = $"{GameManager.Instance.UserInfo.itemReinforceTicket}";
            this.incantText.text = $"{GameManager.Instance.UserInfo.itemIncantTicket}";
            this.EnergyText.text = $"{GameManager.Instance.UserInfo.energy}";
        }

        private void OnGUI()
        {
            // ġƮ�� ��ư�� �߰��մϴ�.
            if (GUI.Button(new Rect(10, 100, 80, 80), "�ְ� ���� ����"))
            {
                {
                    {
                        var weapon = GameManager.Instance.Player.currentWeapon;
                        GameManager.Instance.GetEquipmentData(199, out WeaponData weaponData);
                        weapon.ChangeData(weaponData);
                        weapon.Incant(GameManager.Instance.incantDic[5]);
                        weapon.Incant(GameManager.Instance.incantDic[6]);
                        for (int i = 0; i < 10; i++)
                        {
                            weapon.ReinforceItem();
                        }
                        appearance.EquipWeapon(GameManager.Instance.Player.currentWeapon.weaponApparenceID, GameManager.Instance.Player.currentWeapon.handleType);
                    }

                    {
                        var armor = GameManager.Instance.Player.currentArmor;
                        GameManager.Instance.GetEquipmentData(210, out ArmorData armorData);
                        armor.ChangeData(armorData);
                        armor.Incant(GameManager.Instance.incantDic[101]);
                        armor.Incant(GameManager.Instance.incantDic[104]);
                        for (int i = 0; i < 30; i++)
                        {
                            armor.ReinforceItem();
                        }
                    }

                    {
                        var helmet = GameManager.Instance.Player.currentHelmet;
                        GameManager.Instance.GetEquipmentData(309, out HelmetData helmetData);
                        helmet.ChangeData(helmetData);
                        helmet.Incant(GameManager.Instance.incantDic[203]);
                        helmet.Incant(GameManager.Instance.incantDic[204]);
                        for (int i = 0; i < 30; i++)
                        {
                            helmet.ReinforceItem();
                        }
                    }

                    {
                        var pants = GameManager.Instance.Player.currentPants;
                        GameManager.Instance.GetEquipmentData(405, out PantsData pantsData);
                        pants.ChangeData(pantsData);
                        pants.Incant(GameManager.Instance.incantDic[301]);
                        pants.Incant(GameManager.Instance.incantDic[304]);
                        for (int i = 0; i < 30; i++)
                        {
                            pants.ReinforceItem();
                        }
                    }

                    GameManager.Instance.UserInfo.lastedArmorID = 210;
                    GameManager.Instance.UserInfo.armorPrefixIncantID = 101;
                    GameManager.Instance.UserInfo.armorSuffixIncantID = 104;
                    GameManager.Instance.UserInfo.armorReinforceCount = 30;

                    GameManager.Instance.UserInfo.lastedHelmetID = 309;
                    GameManager.Instance.UserInfo.helmetPrefixIncantID = 204;
                    GameManager.Instance.UserInfo.helmetSuffixIncantID = 203;
                    GameManager.Instance.UserInfo.helmetReinforceCount = 30;

                    GameManager.Instance.UserInfo.lastedPantsID = 405;
                    GameManager.Instance.UserInfo.pantsPrefixIncantID = 301;
                    GameManager.Instance.UserInfo.pantsSuffixIncantID = 304;
                    GameManager.Instance.UserInfo.pantsReinforceCount = 30;
                }


                GameManager.Instance.Player.SetEquipment();
                GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            }

            if (GUI.Button(new Rect(10, 190, 80, 80), "���� �߰�"))
            {
                GameManager.Instance.UserInfo.itemReinforceTicket += 100;
                GameManager.Instance.UserInfo.itemIncantTicket += 100;
                GameManager.Instance.UserInfo.itemGachaTicket += 100;
                UpdateTicketCount();
            }
        }
    }
}