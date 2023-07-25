using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Character.Equipment;
using RPG.Core;
using UnityEngine.Events;
using RPG.Main.Audio;

/*
 * ��� â UI Ŭ����
 */

namespace RPG.Main.UI.BlackSmith
{
    public class ItemPopupUI : MonoBehaviour
    {
        public Equipment choiceItem;

        [Header("itemPopupProperty")]
        [SerializeField] Button incantExcuteBtn;            // ��æƮ ���� ��ư
        [SerializeField] Button reinforceExcuteBtn;         // ��ȭ ���� ��ư
        [SerializeField] Button gachaExcuteBtn;             // ��í ���� ��ư
        [SerializeField] TextMeshProUGUI TodoText;          // ��ư ���� �ؽ�Ʈ

        [Header("EquipmentData")]
        [SerializeField] Image equipmentImage;                      // ��� �̹���
        [SerializeField] TextMeshProUGUI equipmentDescText;         // ��� ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI weaponEquipmentStatusText; // ���� ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI armorEquipmentStatusText;  // ���� ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI helmetEquipmentStatusText; // ��� ���� �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI pantsEquipmentStatusText;  // ���� ���� �ؽ�Ʈ
        [SerializeField] VerticalLayoutGroup layout;                // ���̾ƿ� �׷�

        [Header("PrefixIncant")]
        [SerializeField] IncantDescUI prefixDescUI;         // ���� ��æƮ ���� UI
        [SerializeField] IncantAbilityUI prefixAbilityUI;   // ���� ��æƮ ȿ�� ���� UI

        [Header("SuffixIncant")]
        [SerializeField] IncantDescUI suffixDescUI;         // ���� ��æƮ ���� UI
        [SerializeField] IncantAbilityUI suffixAbilityUI;   // ���� ��æƮ ȿ�� ���� UI

        [Header("Effecter")]
        [SerializeField] UIEffecter reinforceEffecter;  // ��ȭ ����Ʈ 

        CharacterAppearance appearance; // ���� �κ� ĳ���� ����
        Animation animation;            // ���� â�� �ִϸ��̼�

        private void Awake()
        {
            animation = GetComponent<Animation>();
            appearance = FindObjectOfType<CharacterAppearance>();
        }

        // â�� Ȱ��ȭ�Ǿ��ٸ� �ִϸ��̼��� �ʱ�ȭ�մϴ�.
        private void OnEnable()
        {
            animation.Rewind();
            animation.Play();
            animation.Sample();
            animation.Stop();
        }

        // ��í ��ư ������ �� �����մϴ�.
        public void InitGacha()
        {
            TodoText.fontSize = 18.5f;
            TodoText.text = $"" +
                $"�������� ���Ӱ� �����ðڽ��ϱ�?\n" +
                $"(����� ��æƮ�� ��ȭ ��ġ�� ������ϴ�.)\n" +
                $"(�븻 : {Constant.getNormalPercent}%, ���� : {Constant.getRarelPercent}%, ����ũ : {Constant.getUniquePercent}%, ���� : {Constant.getLegendaryPercent}%)";

            InitExcuteBtn();

            incantExcuteBtn.gameObject.SetActive(false);
            reinforceExcuteBtn.gameObject.SetActive(false);
            gachaExcuteBtn.gameObject.SetActive(true);


        }

        // ��æƮ ��ư ������ �� �����մϴ�.
        public void InitIncant()
        {
            TodoText.fontSize = 22;
            TodoText.text = $"�����ۿ� ��æƮ�� �����Ͻðڽ��ϱ�?\n" +
                $"(���ο� ���� ��æƮ ���߿� �ϳ��� ��æƮ\n" +
                $"�Ǹ� ������ ��æƮ�� ��ü�˴ϴ�.)";

            InitExcuteBtn();

            incantExcuteBtn.gameObject.SetActive(true);
            reinforceExcuteBtn.gameObject.SetActive(false);
            gachaExcuteBtn.gameObject.SetActive(false);

        }

        // ��ȭ ��ư ������ �� �����մϴ�.
        public void InitReinforce()
        {
            TodoText.text = $"�������� ��ȭ�Ͻðڽ��ϱ�?\n" +
                $"(������ ��ȭȮ�� : {RandomSystem.ReinforceCalc(choiceItem)}%)";

            InitExcuteBtn();

            incantExcuteBtn.gameObject.SetActive(false);
            reinforceExcuteBtn.gameObject.SetActive(true);
            gachaExcuteBtn.gameObject.SetActive(false);
        }

        // ���� ��ư�� �����մϴ�.
        public void InitExcuteBtn()
        {
            if (GameManager.Instance.UserInfo.itemIncantTicket <= 0)
            {
                incantExcuteBtn.interactable = false;
            }
            else
            {
                incantExcuteBtn.interactable = true;
            }

            if (GameManager.Instance.UserInfo.itemReinforceTicket <= 0)
            {
                reinforceExcuteBtn.interactable = false;
            }
            else
            {
                reinforceExcuteBtn.interactable = true;
            }

            if (GameManager.Instance.UserInfo.itemGachaTicket <= 0)
            {
                gachaExcuteBtn.interactable = false;
            }
            else
            {
                gachaExcuteBtn.interactable = true;
            }
        }
        
        // ��� ��æƮ �մϴ�.
        public void Incant()
        {
            GameManager.Instance.UserInfo.itemIncantTicket--;

            Incant incant;
            // ���� ������ �������� Ÿ���� ������ ����� ��æƮ�� �����ɴϴ�.
            RandomSystem.TryGachaIncant(choiceItem.equipmentType, GameManager.Instance.incantDic, out incant);

            // �������� ��æƮ �մϴ�.
            choiceItem.Incant(incant);

            // �������� ������ ǥ���ϰ� �ش� ���������� ���� ĳ���Ϳ� ������ŵ�ϴ�.
            ShowItem(choiceItem);
            GameManager.Instance.Player.SetEquipment();
            InitIncant();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            AudioManager.Instance.PlaySoundOneShot("IncantSound");
        }

        // ��� �̽��ϴ�.
        public void Gacha()
        {
            GameManager.Instance.UserInfo.itemGachaTicket--;

            EquipmentData data;
            // ������ ����� ���� �����۰� ���� �������� �̽��ϴ�.
            RandomSystem.TryGachaRandomData(GameManager.Instance.equipmentDataDic, choiceItem.equipmentType, out data);
            if (data == null)
            {
                return;
            }

            // ��í �ִϸ��̼��� �����ݴϴ�.
            if (animation.isPlaying)
            {
                ShowItem(choiceItem);
                animation.Stop();
            }

            // ������ �������� �����͸� �������ݴϴ�.
            choiceItem.ChangeData(data);
            if (choiceItem.equipmentType == EquipmentItemType.Weapon)
            {
                appearance.EquipWeapon((data as WeaponData).weaponApparenceID, (data as WeaponData).weaponHandleType);
            }

            //���� �������� ���� �����ݴϴ�.
            GameManager.Instance.Player.SetEquipment();
            InitGacha();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);


            AudioManager.Instance.PlaySoundOneShot("GachaSound");
            animation.Play();
        }
        // ������ �������� ��ȭ�մϴ�.
        public void Reinforce()
        {
            GameManager.Instance.UserInfo.itemReinforceTicket--;

            if (MyUtility.ProbailityCalc(100 - RandomSystem.ReinforceCalc(choiceItem), 0, 100))
                // ��ȭ�� �����ߴٸ�
            {
                // ��� ��ȭ�ϰ� ��ȭ ����Ʈ�� �����ݴϴ�.
                choiceItem.ReinforceItem();
                reinforceEffecter.Play();
            }

            // �κ� ĳ���Ͱ� ��� �����մϴ�.
            GameManager.Instance.Player.SetEquipment();
            ShowItem(choiceItem);
            InitReinforce();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            AudioManager.Instance.PlaySoundOneShot("ReinforceSound");
        }

        // �������� �����ݴϴ�.
        public void ShowItemAnim()
        {
            ShowItem(choiceItem);
        }


        // ���������� �����մϴ�.
        public void ChoiceItem(Equipment item)
        {
            choiceItem = item;
            ShowItem(item);
        }

        // ������ ������ �����ݴϴ�.
        private void ShowItem(Equipment item)
        {
            // �������� �̹����� ������ ǥ���մϴ�.
            equipmentImage.sprite = item.data.equipmentSprite;
            equipmentDescText.text = $"" +
                $"{MyUtility.returnSideText("��� �̸� : ", $"{((item.reinforceCount > 0) ? $"+{item.reinforceCount} " : "")}{item.itemName}")}\n" +
                $"{MyUtility.returnSideText("��� ���� : ", item.ToStringEquipmentType())}\n" +
                $"{MyUtility.returnSideText("��� ��� : ", item.ToStringTier())}\n" +
                $"{MyUtility.returnSideText("���� ��æƮ : ", (item.prefix != null ? item.prefix.incantName : "����"))}\n" +
                $"{MyUtility.returnSideText("���� ��æƮ : ", (item.suffix != null ? item.suffix.incantName : "����"))}";

            // ��� ��� Ÿ�� ������ �����ְ� �˸´� ���Ÿ�� ���� �����ݴϴ�.

            weaponEquipmentStatusText.transform.parent.gameObject.SetActive(false);
            armorEquipmentStatusText.transform.parent.gameObject.SetActive(false);
            pantsEquipmentStatusText.transform.parent.gameObject.SetActive(false);
            helmetEquipmentStatusText.transform.parent.gameObject.SetActive(false); 

            switch (item.equipmentType)
            {
                case EquipmentItemType.Weapon:
                    ShowWeaponText(weaponEquipmentStatusText, item as Weapon);
                    break;
                case EquipmentItemType.Armor:
                    ShowArmorText(armorEquipmentStatusText, item as Armor);
                    break;
                case EquipmentItemType.Pants:
                    ShowPantsText(pantsEquipmentStatusText, item as Pants);
                    break;
                case EquipmentItemType.Helmet:
                    ShowHelmetText(helmetEquipmentStatusText, item as Helmet);
                    break;
            }

            // ��� ����Ǿ��ִ� ��æƮ�� �����ݴϴ�.
            prefixDescUI.ShowIncant(choiceItem.prefix);
            prefixAbilityUI.ShowIncant(choiceItem.prefix);
            suffixDescUI.ShowIncant(choiceItem.suffix);
            suffixAbilityUI.ShowIncant(choiceItem.suffix);

            // ���̾ƿ��� �籸���մϴ�.
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layout.transform);
        }

        // �� �������� Ÿ�Կ� �´� ������ �����մϴ�.
        // ������ �� �󺧰� ���� ��ġ�� ����̵忡 �����մϴ�.

        private void ShowWeaponText(TextMeshProUGUI equipmentStatusText, Weapon weapon)
        {
            equipmentStatusText.text = $"" +
                $"{MyUtility.returnSideText("���ݷ� : ", weapon.AttackDamage.ToString())}\n" +
                $"{MyUtility.returnSideText("���ݼӵ� : ", $"�ʴ� {weapon.AttackSpeed}ȸ Ÿ��")}\n" +
                $"{MyUtility.returnSideText("���ݹ��� : ", weapon.AttackRange.ToString())}\n" +
                $"{MyUtility.returnSideText("�̵��ӵ� : ", weapon.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ Ȯ�� : ", $"{weapon.CriticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ������ : ", $"���ݷ��� {weapon.CriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("���߷� : ", $"{weapon.AttackChance * 100}%")}";

            equipmentStatusText.transform.parent.gameObject.SetActive(true);
        }

        private void ShowArmorText(TextMeshProUGUI equipmentStatusText, Armor armor)
        {
            equipmentStatusText.text = $"" +
                $"{MyUtility.returnSideText("ü�� : ", armor.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("���� : ", $"{armor.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("�̵��ӵ� : ", armor.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("ȸ���� : ", $"{armor.EvasionPoint}%")}";

            equipmentStatusText.transform.parent.gameObject.SetActive(true);
        }

        private void ShowHelmetText(TextMeshProUGUI equipmentStatusText, Helmet helmet)
        {
            equipmentStatusText.text = $"" +
                $"{MyUtility.returnSideText("ü�� : ", helmet.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("���� : ", $"{helmet.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ������ ������ : ", $"{helmet.DecreseCriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ȸ���� : ", $"{helmet.EvasionCritical * 100}%")}";

            equipmentStatusText.transform.parent.gameObject.SetActive(true);
        }

        private void ShowPantsText(TextMeshProUGUI equipmentStatusText, Pants pants)
        {
            equipmentStatusText.text = $"" +
                $"{MyUtility.returnSideText("ü�� : ", pants.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("���� : ", $"{pants.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("�̵��ӵ� : ", $"{pants.MovementSpeed}")}";

            equipmentStatusText.transform.parent.gameObject.SetActive(true);
        }
    }
}