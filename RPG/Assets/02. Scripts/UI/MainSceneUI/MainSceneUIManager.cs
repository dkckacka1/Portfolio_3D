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