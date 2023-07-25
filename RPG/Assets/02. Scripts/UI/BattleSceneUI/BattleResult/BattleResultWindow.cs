using RPG.Battle.Core;
using RPG.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ���� ��� â UI Ŭ����
 */

namespace RPG.Battle.UI
{
    public class BattleResultWindow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI floorText;     // ���� �� �ؽ�Ʈ
        [SerializeField] GetItemText energyTxt;         // ���� ������ �ؽ�Ʈ
        [SerializeField] GetItemText gachaTxt;          // ���� ��í Ƽ�� �ؽ�Ʈ
        [SerializeField] GetItemText reinforceTxt;      // ���� ��ȭ Ƽ�� �ؽ�Ʈ
        [SerializeField] GetItemText incantTxt;         // ���� ��æƮ Ƽ�� �ؽ�Ʈ

        [Header("ChangeBattleState")]
        [SerializeField] TextMeshProUGUI titleText; // ��� �ؽ�Ʈ
        [SerializeField] Button reStartBtn;         // �絵�� ��ư
        [SerializeField] TextMeshProUGUI btnText;   // ��ư �ؽ�Ʈ

        // UI�� �ʱ�ȭ�մϴ�.
        public void InitUI(int floor)
        {
            UpdateUI(floor);
        }

        // ���� �� ���� ǥ���մϴ�.
        public void UpdateUI(int floor)
        {
            floorText.text = $"���� �� �� : \t{floor}��";
        }

        // �й� UI�� ǥ���մϴ�.
        public void ShowDefeatUI()
        {
            // �絵�� ���θ� Ȯ���ϱ����� ���� ������ �Һ� �������� �����ɴϴ�.
            int consumeEnergy = GameManager.Instance.stageDataDic[BattleManager.Instance.currentStageFloor].ConsumEnergy;
            titleText.text = "���� ���";
            btnText.text = $"���� ��\n�絵�� (-{consumeEnergy})";
            reStartBtn.onClick.RemoveAllListeners();
            // �絵�� ��ư�� �̺�Ʈ�� �����մϴ�.
            reStartBtn.onClick.AddListener(() =>
            {
                GameManager.Instance.UserInfo.energy -= consumeEnergy;
                BattleManager.Instance.ReStartBattle();
            });

            // ���� ������ �������翡 ���� �絵�� ��ư�� Ȱ��ȭ�մϴ�.
            if (GameManager.Instance.UserInfo.energy < consumeEnergy)
            {
                reStartBtn.interactable = false;
            }
            else
            {
                reStartBtn.interactable = true;
            }
        }

        // ���� UI�� ǥ���մϴ�.
        public void ShowPauseUI()
        {
            titleText.text = "���� ����";
            btnText.text = "������\n���ư���";
            reStartBtn.onClick.RemoveAllListeners();
            reStartBtn.onClick.AddListener(() => { BattleManager.Instance.ReturnBattle(); });
        }

        // ���� �������� ǥ���մϴ�.
        public void UpdateEnergy()
        {
            if (BattleManager.Instance.gainEnergy == 0)
            {
                return;
            }

            energyTxt.GainText(BattleManager.Instance.gainEnergy, 0.5f);
        }

        // ���� ��í Ƽ���� ǥ���մϴ�.
        public void UpdateGacha()
        {
            if (BattleManager.Instance.gainGacha == 0)
            {
                return;
            }

            gachaTxt.GainText(BattleManager.Instance.gainGacha, 0.5f);
        }

        // ���� ��æƮ Ƽ���� ǥ���մϴ�.
        public void UpdateIncant()
        {
            if (BattleManager.Instance.gainIncant == 0)
            {
                return;
            }

            incantTxt.GainText(BattleManager.Instance.gainIncant, 0.5f);
        }

        // ���� ��ȭ Ƽ���� ǥ���մϴ�.
        public void UpdateReinforce()
        {
            if (BattleManager.Instance.gainReinforce == 0)
            {
                return;
            }

            reinforceTxt.GainText(BattleManager.Instance.gainReinforce, 0.5f);
        }

    }

}