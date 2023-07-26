using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * �������� ���� ��ü���� UI�� �������ִ� �Ŵ��� Ŭ�����Դϴ�.
 */

namespace RPG.Stage.UI
{
    public class StageSceneUIManager : MonoBehaviour
    {
        [SerializeField] StageInfomationUI ui;              // �������� ���� UI

        [SerializeField] TextMeshProUGUI reinforceText; // ��ȭ Ƽ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI incantText;    // ��æƮ Ƽ�� �ؽ�Ʈ 
        [SerializeField] TextMeshProUGUI GachaText;     // ��í Ƽ�� �ؽ�Ʈ 
        [SerializeField] TextMeshProUGUI EnergyText;    // ���� ������ ��

        private void Start()
        {
            // ���� ���� ó������ ������ �������������Դϴ�.
            ui.ShowStageInfomation(GameManager.Instance.stageDataDic[1]);
            ShowUserinfo(GameManager.Instance.UserInfo);
        }

        // ���� ������ ǥ���մϴ�.
        void ShowUserinfo(UserInfo userinfo)
        {
            reinforceText.text = userinfo.itemReinforceTicket.ToString();
            incantText.text = userinfo.itemIncantTicket.ToString();
            GachaText.text = userinfo.itemGachaTicket.ToString();
            EnergyText.text = userinfo.energy.ToString();
        }
    } 
}
