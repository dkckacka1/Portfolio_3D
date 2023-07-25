using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;

/*
 * ����â���� ������ ������ ǥ�����ִ� UI Ŭ����   
 */

namespace RPG.Main.UI.StatusUI
{
    public class UserinfoDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI riseTopCountText;  // �ִ�� ���� ���� �ؽ�Ʈ
        [SerializeField] ItemDescUI weaponDesc;             // ���� ���� UI
        [SerializeField] ItemDescUI armorDesc;              // ���� ���� UI
        [SerializeField] ItemDescUI helmetDesc;             // ��� ���� UI
        [SerializeField] ItemDescUI pantsDesc;              // ���� ���� UI

        // â�� Ȱ��ȭ �Ǹ� ���� ������ �����ݴϴ�
        private void OnEnable()
        {
            ShowUserinfo();
        }

        // ���� ������ �����ݴϴ�.
        public void ShowUserinfo()
        {
            riseTopCountText.text = MyUtility.returnSideText("�ִ�� ���� �� �� :", GameManager.Instance.UserInfo.risingTopCount.ToString());

            weaponDesc.ShowEquipment(GameManager.Instance.Player.currentWeapon);
            armorDesc.ShowEquipment(GameManager.Instance.Player.currentArmor);
            helmetDesc.ShowEquipment(GameManager.Instance.Player.currentHelmet);
            pantsDesc.ShowEquipment(GameManager.Instance.Player.currentPants);
        }
    }
}