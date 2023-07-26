using RPG.Battle.Core;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * �������� ��ũ�Ѻ��� �� ���� ǥ�õ� ���� �Դϴ�.
 */

namespace RPG.Stage.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class StageFloorUI : MonoBehaviour
    {
        public RectTransform CachedRectTrasnfrom => GetComponent<RectTransform>(); // ���� ��Ʈ Ʈ�������� ����
        public Button ShowStageBtn;     // ����� ��ư

        [Header("UnLockObject")]
        [SerializeField] GameObject unLockObject;           // ������ ������Ʈ
        [SerializeField] TextMeshProUGUI stageFloorText;    // �� �� �ؽ�Ʈ
        [Header("LockObject")]
        [SerializeField] GameObject lockObject; // ��� ������Ʈ

        private StageData stageData;    // �������� ������

        // ���� �����ϴ� ����Ʈ �׸��� �ε���
        public int Index { get; set; }

        //���� ����
        public float Height
        {
            get { return CachedRectTrasnfrom.sizeDelta.y; }     // ��Ʈ Ʈ�������� ������ ��Ÿ�� y��ġ�Դϴ�.
            set
            {
                Vector2 sizeDelta = CachedRectTrasnfrom.sizeDelta;
                sizeDelta.y = value;
                CachedRectTrasnfrom.sizeDelta = sizeDelta;
            }
        }

        // ���� �������� �����͸� ǥ�����ݴϴ�.
        public void UpdateContent(StageData stageData)
        {
            this.stageData = stageData;
            stageFloorText.text = stageData.ID.ToString() + "��!";

            if (GameManager.Instance.UserInfo.risingTopCount < this.stageData.ID)
                // ������ ���� ���� ���� ������ �������� �������� ������ ���ؼ�
                // ���� ������� �����͸� �������� �����մϴ�.
            {
                lockObject.SetActive(true);
                unLockObject.SetActive(false);
                ShowStageBtn.interactable = false;
            }
            else
            {
                lockObject.SetActive(false);
                unLockObject.SetActive(true);
                ShowStageBtn.interactable = true;
            }
        }

        // ���� ���� ���� ��ġ
        public Vector2 Top
        {
            get
            {
                Vector3[] corners = new Vector3[4];
                CachedRectTrasnfrom.GetLocalCorners(corners);
                return CachedRectTrasnfrom.anchoredPosition + new Vector2(0.0f, corners[1].y); // corner[1] = �»�� ���� ��ǥ
            }
            set
            {
                Vector3[] corners = new Vector3[4];
                CachedRectTrasnfrom.GetLocalCorners(corners);
                CachedRectTrasnfrom.anchoredPosition = value - new Vector2(0.0f, corners[1].y);
            }
        }

        // ���� �Ʒ��� ���� ��ġ
        public Vector2 Bottom
        {
            get
            {
                Vector3[] corners = new Vector3[4];
                CachedRectTrasnfrom.GetLocalCorners(corners);
                return CachedRectTrasnfrom.anchoredPosition + new Vector2(0.0f, corners[3].y); // corner[3] = ���ϴ� ���� ��ǥ
            }
            set
            {
                Vector3[] corners = new Vector3[4];
                CachedRectTrasnfrom.GetLocalCorners(corners);
                CachedRectTrasnfrom.anchoredPosition = value - new Vector2(0.0f, corners[3].y);
            }
        }

        // �������� ����â�� ���� �������� �����͸� ǥ���մϴ�.
        public void ShowStage(StageInfomationUI ui)
        {
            ui.ShowStageInfomation(stageData);
        }
    }
}