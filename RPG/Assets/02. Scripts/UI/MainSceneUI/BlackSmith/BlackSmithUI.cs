using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using RPG.Core;
using RPG.Character.Equipment;

/*
 * ��� ��ȭâ UI Ŭ����
 */

namespace RPG.Main.UI.BlackSmith
{
    public class BlackSmithUI : MonoBehaviour
    {
        [SerializeField] ItemPopupUI popupUI;       // ���� ������ â UI
        [SerializeField] ItemChoiceUI choiceUI;     // ��� ���� â UI

        [SerializeField] TextMeshProUGUI remainIncantText;      // ���� ��æƮ Ƽ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI remainReinfoceText;    // ���� ��ȭ Ƽ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI remainGachaText;       // ���� ��í Ƽ�� �ؽ�Ʈ

        // â�� Ȱ��ȭ �ɶ� �ʱ�ȭ�մϴ�.
        private void OnEnable()
        {
            choiceUI.InitButtonImage();
            // �⺻������ ���⸦ �����մϴ�.
            choiceUI.ChoiceWeapon(popupUI);
            // ��æƮ�� �����մϴ�.
            popupUI.InitIncant();
            InitRemainText();
        }

        // ���� Ƽ�� �ؽ�Ʈ�� �����ݴϴ�.
        public void InitRemainText()
        {
            remainIncantText.text = GameManager.Instance.UserInfo.itemIncantTicket.ToString();
            remainReinfoceText.text = GameManager.Instance.UserInfo.itemReinforceTicket.ToString();
            remainGachaText.text = GameManager.Instance.UserInfo.itemGachaTicket.ToString();
        }
    } 
}
