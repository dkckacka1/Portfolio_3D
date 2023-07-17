using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Battle.Core;

/*
 * ���� ���õ� �Լ����� ��Ƴ��� static Ŭ�����Դϴ�.
 */

namespace RPG.Core
{
    public static class SceneLoader
    {
        // �������� ID�� ������ �ش� �������� �����ɴϴ�.
        public static void LoadBattleScene(int stageID)
        {
            GameManager.Instance.choiceStageID = stageID;
            SceneManager.LoadScene("BattleScene");
        }

        // �������� �����͸� ������ �ش� �������� �����ɴϴ�.
        public static void LoadBattleScene(StageData data)
        {
            GameManager.Instance.choiceStageID = data.ID;
            SceneManager.LoadScene("BattleScene");
        }

        // ���ξ��� �ε��մϴ�.
        public static void LoadMainScene()
        {
            SceneManager.LoadScene("MainScene");
        }

        // �������� ���� ���� �ε��մϴ�.
        public static void LoadStageChoiceScene()
        {
            SceneManager.LoadScene("StageChoiceScene");
        }
    }

}