using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Character.Equipment;

/*
 * ����â�� ��æƮ ȿ�� ���� UI Ŭ����
 */

namespace RPG.Main.UI.StatusUI
{
    public class AbilityDescUI : MonoBehaviour
    {
        [SerializeField] Image abilityImage;                // ȿ�� �̹���
        [SerializeField] TextMeshProUGUI abilityDescText;   // ȿ�� ���� �ؽ�Ʈ

        // ��æƮ�� ȿ���� �����ݴϴ�.
        public void ShowAbility(Incant incant)
        {
            if (!incant.isIncantAbility)
                // ��æƮ�� ���� ȿ���� ���ٸ� �����ݴϴ�.
            {
                gameObject.SetActive(false);
                return;
            }

            abilityImage.sprite = incant.abilityIcon;
            abilityDescText.text = incant.abilityDesc;
        }
    }
}