using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Character.Equipment;
using UnityEngine.UI;

/*
 * ��æƮ ���� UI Ŭ����
 */

namespace RPG.Main.UI.BlackSmith
{
    public class IncantDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameTxt;           // ��æƮ �̸� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI tierTxt;           // ��æƮ ��� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI addDescTxt;        // ���� ȿ�� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI minusDescTxt;      // ���� ȿ�� �ؽ�Ʈ

        // ��æƮ�� �����ݴϴ�
        public void ShowIncant(Incant incant)
        {
            if (incant == null)
            {
                this.gameObject.SetActive(false);
                return;
            }

            nameTxt.text = incant.incantName;

            string tierStr = "";

            // ���� ��æƮ ����� ǥ���մϴ�.
            switch (incant.incantTier)
            {
                case TierType.Normal:
                    tierStr = "�븻";
                    break;
                case TierType.Rare:
                    tierStr = "����";
                    break;
                case TierType.Unique:
                    tierStr = "����ũ";
                    break;
                case TierType.Legendary:
                    tierStr = "����";
                    break;
            }

            tierTxt.text = tierStr;

            // ��æƮ�� ���� ȿ�� ���ڿ��� �����ɴϴ�.
            string addDesc = incant.GetAddDesc();

            if (addDesc == "")
            // ����ȿ���� ���ٸ� ���� ȿ�� �ؽ�Ʈ�� �����ݴϴ�.
            {
                addDescTxt.transform.parent.gameObject.SetActive(false);
            }
            else
            // ���� ȿ���� �ִٸ� ���� ȿ�� �ؽ�Ʈ�� �����ݴϴ�.
            {
                addDescTxt.text = addDesc;
                addDescTxt.transform.parent.gameObject.SetActive(true);
            }

            // ��æƮ�� ���� ȿ�� ���ڿ��� �����ɴϴ�.
            string minusDesc = incant.GetMinusDesc();

            if (minusDesc == "")
            // ���� ȿ���� ���ٸ� ���� ȿ�� �ؽ�Ʈ�� �����ݴϴ�.
            {
                minusDescTxt.transform.parent.gameObject.SetActive(false);
            }
            else
            // ���� ȿ���� �ִٸ� ���� ȿ�� �ؽ�Ʈ�� �����ݴϴ�.
            {
                minusDescTxt.text = minusDesc;
                minusDescTxt.transform.parent.gameObject.SetActive(true);
            }

            // �ڽŰ� �ڽ��� �ڽ� ������Ʈ�� ��� UI�� �籸���մϴ�.
            for (int i = 0; i < this.transform.childCount; i++)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.GetChild(i));
            }
            this.gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        }
    }
}

