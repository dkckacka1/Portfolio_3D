using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

/*
 * ���� ���â�� ǥ�õ� ������ ȹ�� �ؽ�Ʈ UI Ŭ����
 */ 

namespace RPG.Battle.UI
{
    public class GetItemText : MonoBehaviour
    {
        TextMeshProUGUI txt;    // �ؽ�Ʈ

        float showGain = 0;                         // ���� ����
        float DelayPerGainNum = 0;                  // �� ƽ�� �ö� ����
        [SerializeField] float gainDelay = 0.01f;   // �� ƽ�� �ð�

        private void Awake()
        {
            txt = GetComponent<TextMeshProUGUI>();
        }

        // Ȱ��ȭ �ɶ� �ʱ�ȭ�մϴ�.
        private void OnEnable()
        {
            txt.text = "0";
        }

        // �ؽ�Ʈ�� �����ݴϴ�.
        public void GainText(int gain, float time = 1f)
        {
            // ��ƽ�� ���� ������ �����մϴ�.
            DelayPerGainNum = gain / (time / gainDelay);
            // �ڷ�ƾ�� �����մϴ�.
            StartCoroutine(GainCoroutine(gain));
        }

        // �ؽ�Ʈ�� ���������� �ö� �� �ֵ��� �ϴ� �ڷ�ƾ
        IEnumerator GainCoroutine(int gain)
        {
            while (true)
            {
                // �����̸�ŭ ����մϴ�.
                yield return new WaitForSecondsRealtime(gainDelay);
                // ƽ��ŭ �ö� ������ ����ϴ�.
                showGain += DelayPerGainNum;

                // �ؽ�Ʈ�� ������Ʈ�մϴ�.
                txt.text = showGain.ToString("F0");

                // ǥ�õ� ������ ���� �������� Ŀ���ٸ�
                if ((int)(showGain + 0.5) >= gain)
                {
                    // �ؽ�Ʈ�� ������Ʈ�ϰ� �ݺ����� �ߴ��մϴ�.
                    txt.text = gain.ToString();
                    break;
                }
            }
        }
    }

}