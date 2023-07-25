using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RPG.Character.Equipment;

/*
 * ��� �������� ������ �����ִ� UI Ŭ����
 */

namespace RPG.Main.UI.StatusUI
{
    public class ItemDescUI : MonoBehaviour
    {
        [SerializeField] Image itemImage;                   // ������ �̹���
        [SerializeField] TextMeshProUGUI itemNameText;      // ������ ���� �ؽ�Ʈ

        // ���������� ������ �����ݴϴ�.
        public void ShowEquipment(Equipment equipment)
        {
            itemImage.sprite = equipment.data.equipmentSprite;

            // ��æƮ ���θ� ����ؼ� �̸��� �����ݴϴ�.
            string text = "";

            if (equipment.prefix != null)
            {
                text += equipment.prefix.incantName + " ";
            }

            if (equipment.suffix != null)
            {
                text += equipment.suffix.incantName + " ";
            }

            text += equipment.itemName;
            itemNameText.text = text;
        }
    }
}