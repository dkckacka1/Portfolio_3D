using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ����ǥ ��ư�� �������� ������ Ʃ�丮�� â UI Ŭ����
 */

namespace RPG.Main.UI.Tutorial
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] ScrollRect scrollRect;     // Ʃ�丮�� â�� ��ũ�� ��
        [SerializeField] RectTransform defaultText; // �⺻ �ؽ�Ʈ

        private int contentChildCount;

        private void Awake()
        {
            contentChildCount = scrollRect.content.transform.childCount;
        }

        // â�� Ȱ��ȭ�Ǹ� �⺻ �ؽ�Ʈ�� �����ݴϴ�.
        private void OnEnable()
        {
            ShowTutorial(defaultText);
        }

        // ��ư�� Ŭ���ϸ� Ʃ�丮���� �����ݴϴ�.
        public void ShowTutorial(RectTransform targetTutorial)
        {
            // ��ũ�� �並 �����մϴ�
            for (int i = 0; i < contentChildCount; i++)
            {
                scrollRect.content.GetChild(i).gameObject.SetActive(false);
            }

            targetTutorial.gameObject.SetActive(true);
            // ���̾ƿ��� �籸���մϴ�.
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)scrollRect.content.transform);
        }
    }

}