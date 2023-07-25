using RPG.Character.Equipment;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ��æƮ ȿ�� UI Ŭ����
 */

namespace RPG.Main.UI.BlackSmith
{
    public class IncantAbilityUI : MonoBehaviour
    {
        [SerializeField] Image abilityImage;                // ��æƮ �̹���
        [SerializeField] TextMeshProUGUI abilityDescTxt;    // ��æƮ ���� �ؽ�Ʈ

        // ��æƮ�� �����ݴϴ�.
        public void ShowIncant(Incant incant)
        {
            // ��æƮ�� ���ų� ��æƮ�� ȿ�������� ��� ��Ȱ��ȭ�մϴ�.
            if (incant == null)
            {
                this.gameObject.SetActive(false);
                return;
            }

            if (!incant.isIncantAbility)
            {
                this.gameObject.SetActive(false);
                return;
            }

            // ȿ���� �ִٸ� �����ݴϴ�.
            abilityImage.sprite = incant.abilityIcon;   
            abilityDescTxt.text = incant.abilityDesc;   
            this.gameObject.SetActive(true);
        }
    }

}