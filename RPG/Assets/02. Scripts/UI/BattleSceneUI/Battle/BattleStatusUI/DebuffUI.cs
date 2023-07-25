using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * ���� ĳ������ ����� ��Ȳ�� �����ִ� UI
 */

namespace RPG.Battle.UI
{
    public class DebuffUI : MonoBehaviour
    {
        [Header("Bloody")]
        [SerializeField] GameObject bloodyObject;           // ���� ���� ������Ʈ
        [SerializeField] TextMeshProUGUI remainBloodyText;  // ���� ���� �ð�
        int bloodyOverlapping;                              // ���� ��ø Ƚ��

        [Header("Curse")]
        [SerializeField] GameObject curseObject;            // ���� ���� ������Ʈ
        [SerializeField] TextMeshProUGUI remainCurseText;   // ���� ���� �ð�

        [Header("Fear")]
        [SerializeField] GameObject FearObject;             // ���� ���� ������Ʈ
        [SerializeField] TextMeshProUGUI remainFearText;    // ���� ���� �ð�

        [Header("Paralysis")]
        [SerializeField] GameObject paralysisObject;            // ���� ���� ������Ʈ
        [SerializeField] TextMeshProUGUI remainParalysisText;   // ���� ���� �ð�

        [Header("Stern")]
        [SerializeField] GameObject sternObject;                // ���� ���� ������Ʈ
        [SerializeField] TextMeshProUGUI remainSternText;       // ���� ���� �ð�

        [Header("Temptation")]
        [SerializeField] GameObject TemptationObject;           // ��Ȥ ���� ������Ʈ
        [SerializeField] TextMeshProUGUI remainTemptationText;  // ���� ��Ȥ �ð� 

        // ��� ����� UI�� �ʱ�ȭ�մϴ�..
        public void ResetAllDebuff()
        {
            ResetDebuff(DebuffType.Stern);
            ResetDebuff(DebuffType.Bloody);
            ResetDebuff(DebuffType.Paralysis);
            ResetDebuff(DebuffType.Temptation);
            ResetDebuff(DebuffType.Fear);
            ResetDebuff(DebuffType.Curse);
        }

        // ����� UI�� �ʱ�ȭ�մϴ�.
        public void ResetDebuff(DebuffType type)
        {
            switch (type)
            {
                case DebuffType.Stern:
                    sternObject.SetActive(false);
                    break;
                case DebuffType.Bloody:
                    bloodyObject.SetActive(false);
                    bloodyOverlapping = 0;
                    break;
                case DebuffType.Paralysis:
                    paralysisObject.SetActive(false);
                    break;
                case DebuffType.Temptation:
                    TemptationObject.SetActive(false);
                    break;
                case DebuffType.Fear:
                    FearObject.SetActive(false);
                    break;
                case DebuffType.Curse:
                    curseObject.SetActive(false);
                    break;
            }
        }

        // ����� UI�� �����ݴϴ�
        public void InitDebuff(DebuffType type)
        {
            switch (type)
            {
                case DebuffType.Stern:
                    sternObject.SetActive(true);
                    break;
                case DebuffType.Bloody:
                    bloodyOverlapping++;
                    if (!bloodyObject.activeInHierarchy)
                    {
                        bloodyObject.SetActive(true);
                    }
                    break;
                case DebuffType.Paralysis:
                    paralysisObject.SetActive(true);
                    break;
                case DebuffType.Temptation:
                    TemptationObject.SetActive(true);
                    break;
                case DebuffType.Fear:
                    FearObject.SetActive(true);
                    break;
                case DebuffType.Curse:
                    curseObject.SetActive(true);
                    break;
            }
        }

        // ����� UI�� �����ݴϴ�.
        public void ReleaseDebuff(DebuffType type)
        {
            switch (type)
            {
                case DebuffType.Stern:
                    sternObject.SetActive(false);
                    break;
                case DebuffType.Bloody:
                    bloodyOverlapping--;
                    if (bloodyOverlapping <= 0)
                    {
                        bloodyObject.SetActive(false);
                    }
                    break;
                case DebuffType.Paralysis:
                    paralysisObject.SetActive(false);
                    break;
                case DebuffType.Temptation:
                    TemptationObject.SetActive(false);
                    break;
                case DebuffType.Fear:
                    FearObject.SetActive(false);
                    break;
                case DebuffType.Curse:
                    curseObject.SetActive(false);
                    break;
            }
        }

        // ����� UI�� �����ݴϴ�.
        public void ShowDebuff(DebuffType type, float duration)
        {
            // �˸´� UI ������Ʈ�� �����ְ� ���� �ð��� ǥ���մϴ�.
            switch (type)
            {
                case DebuffType.Stern:
                    remainSternText.text = $"{duration.ToString("N1")}";
                    break;
                case DebuffType.Bloody:
                    if (bloodyOverlapping > 1)
                    {
                        remainBloodyText.text = bloodyOverlapping.ToString();
                    }
                    else
                    {
                        remainSternText.text = $"{duration.ToString("N1")}";
                    }
                    break;
                case DebuffType.Paralysis:
                    remainParalysisText.text = $"{duration.ToString("N1")}";
                    break;
                case DebuffType.Temptation:
                    remainTemptationText.text = $"{duration.ToString("N1")}";
                    break;
                case DebuffType.Fear:
                    remainFearText.text = $"{duration.ToString("N1")}";
                    break;
                case DebuffType.Curse:
                    remainCurseText.text = $"{duration.ToString("N1")}";
                    break;
            }
        }
    }

}