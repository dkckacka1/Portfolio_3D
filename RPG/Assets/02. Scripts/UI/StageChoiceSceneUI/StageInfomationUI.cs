using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RPG.Battle.Core;
using System.Linq;
using RPG.Core;

/*
 * �������� ������ ǥ�����ִ� UI Ŭ����
 */

namespace RPG.Stage.UI
{
    public class StageInfomationUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI floorTxt;          // ���� �� �� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI ConsumeEnergyTxt;  // �Һ� ������ �ؽ�Ʈ

        [SerializeField] List<EnemyInfomationUI> enemyInfoList; // �� ���� UI ����Ʈ

        [SerializeField] Button ChallengeBtn;   // ���� ��ư

        private StageData stageData;// ���� �������� ������

        // ���� �������� ������ ǥ���մϴ�.
        public void ShowStageInfomation(StageData data)
        {
            this.stageData = data;

            floorTxt.text = $"{stageData.ID} ��";
            ConsumeEnergyTxt.text = $"�Һ� ������ : {(stageData.ConsumEnergy != 0 ? $"-{stageData.ConsumEnergy}" : "0")}";
            Dictionary<int, int> stageEnemy = new Dictionary<int, int>();

            // �������� �����Ϳ��ִ� �� �����͸� ������ UI�� ǥ�����ݴϴ�.
            foreach (var enemy in data.enemyDatas)
            {
                if (stageEnemy.ContainsKey(enemy))
                {
                    stageEnemy[enemy]++;
                }
                else
                {
                    stageEnemy.Add(enemy, 1);
                }
            }


            var list = stageEnemy.ToList();

            for (int i = 0; i < enemyInfoList.Count; i++)
            {
                if (i >= list.Count)
                {
                    enemyInfoList[i].gameObject.SetActive(false);
                    continue;
                }

                EnemyData enemyinfodata = GameManager.Instance.enemyDataDic[list[i].Key];
                enemyInfoList[i].ShowEnemyInfomation(enemyinfodata, list[i].Value);
                enemyInfoList[i].gameObject.SetActive(true);
            }

            // ���� ������ ���� �������翡 ���� ���� ��ư�� Ȱ��ȭ�մϴ�.
            if (GameManager.Instance.UserInfo.energy < stageData.ConsumEnergy)
            {
                ChallengeBtn.GetComponentInChildren<TextMeshProUGUI>().text = "��������\n�����ؿ�!";
                ChallengeBtn.interactable = false;
            }
            else
            {
                ChallengeBtn.GetComponentInChildren<TextMeshProUGUI>().text = "���� �ϱ�!";
                ChallengeBtn.interactable = true;
            }
        }

        // ������ ���������� ������ �����մϴ�.
        public void Challenge()
        {
            GameManager.Instance.UserInfo.energy -= this.stageData.ConsumEnergy;
            SceneLoader.LoadBattleScene(this.stageData);
        }

        // ���� �κ������ �̵��մϴ�.
        public void ReturnMainMenu()
        {
            SceneLoader.LoadMainScene();
        }
    }

}