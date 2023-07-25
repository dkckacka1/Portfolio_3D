using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;
using RPG.Character.Status;
using RPG.Character.Equipment;

/*
 * �÷��̾� ĳ������ ������ ���� �ִ� â UI Ŭ����
 */

namespace RPG.Main.UI.StatusUI
{
    public class PlayerStatusDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI attackStatusText;      // ���� ��ġ �ؽ�Ʈ
        [SerializeField] TextMeshProUGUI defenceStatusText;     // ��� ��ġ �ؽ�Ʈ

        [Header("Ability")]
        [SerializeField] GameObject abilityPropertyObject;              // ��æƮ ȿ���� �θ� ������Ʈ
        [SerializeField] AbilityDescUI weaponPrefixAbilityDescUI;       // ���� ���� ��æƮ ȿ�� ���� UI
        [SerializeField] AbilityDescUI weaponSuffixAbilityDescUI;       // ���� ���� ��æƮ ȿ�� ���� UI
        [SerializeField] AbilityDescUI armorPrefixAbilityDescUI;        // ���� ���� ��æƮ ȿ�� ���� UI
        [SerializeField] AbilityDescUI armorSuffixAbilityDescUI;        // ���� ���� ��æƮ ȿ�� ���� UI
        [SerializeField] AbilityDescUI helmetPrefixAbilityDescUI;       // ��� ���� ��æƮ ȿ�� ���� UI
        [SerializeField] AbilityDescUI helmetSuffixAbilityDescUI;       // ��� ���� ��æƮ ȿ�� ���� UI
        [SerializeField] AbilityDescUI pantsPrefixAbilityDescUI;        // ���� ���� ��æƮ ȿ�� ���� UI
        [SerializeField] AbilityDescUI pantsSuffixAbilityDescUI;        // ���� ���� ��æƮ ȿ�� ���� UI

        // â�� Ȱ��ȭ�Ǹ� �÷��̾� ĳ������ ������ �����ݴϴ�.
        private void OnEnable()
        {
            ShowPlayerStatus();
        }

        public void ShowPlayerStatus()
        {
            // ����, ���, ��æƮ ������ �����ְ� ���̾ƿ��� �籸���մϴ�.
            ShowAttackStatus(GameManager.Instance.Player);
            ShowDefenceStatus(GameManager.Instance.Player);
            ShowPlayerAbility(GameManager.Instance.Player);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)abilityPropertyObject.transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)abilityPropertyObject.transform.parent.transform);
        }

        // ���� ������ �����ݴϴ�.
        // ������ �󺧰� ���� ����̵忡 ��ġ��ŵ�ϴ�.
        private void ShowAttackStatus(PlayerStatus status)
        {
            attackStatusText.text = $"" +
                $"{MyUtility.returnSideText("���ݷ� : ", $"{status.AttackDamage}")}\n" +
                $"{MyUtility.returnSideText("���ݹ��� : ", $"{status.AttackRange}")}\n" +
                $"{MyUtility.returnSideText("���ݼӵ� : ", $"�ʴ� {status.AttackSpeed}ȸ Ÿ��")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ Ȯ�� : ", $"{status.CriticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ���ݷ� : ", $"���ݷ��� {status.CriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("���߷� : ", $"{status.AttackChance * 100}%")}";
        }

        // ��� ������ �����ݴϴ�.
        // ������ �󺧰� ���� ����̵忡 ��ġ��ŵ�ϴ�.
        private void ShowDefenceStatus(PlayerStatus status)
        {
            defenceStatusText.text = $"" +
                $"{MyUtility.returnSideText("ü�� : ", $"{status.MaxHp}")}\n" +
                $"{MyUtility.returnSideText("���� : ", $"{status.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("ȸ���� : ", $"{status.EvasionPoint * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ȸ���� : ", $"{status.EvasionCritical * 100}%")}\n" +
                $"{MyUtility.returnSideText("ġ��Ÿ ������ : ", $"ġ��Ÿ ���ݷ��� {status.DecreseCriticalDamage * 100}% ����")}\n" +
                $"{MyUtility.returnSideText("�̵��ӵ� : ", $"{status.MovementSpeed}")}";
        }

        // �÷��̾� ����� ��æƮ�� ȿ���� �����ݴϴ�.
        private void ShowPlayerAbility(PlayerStatus status)
        {
            if (!status.hasAbility())
                // ��� ����� ȿ���� ���ٸ� ȿ�� ���� UI�� �θ� ������Ʈ�� ��Ȱ��ȭ �մϴ�.
            {
                abilityPropertyObject.SetActive(false);
                return;
            }
            
            // �� ����� ��æƮ ����, ��æƮ�� ȿ�� ���θ� Ȯ���ؼ� ������ ǥ���մϴ�.
            // ���������� ���̾ƿ��� �籸���մϴ�.

            Weapon weapon = status.currentWeapon;
            if (weapon.hasPrefixAbilitySkill())
            {
                weaponPrefixAbilityDescUI.gameObject.SetActive(true);
                weaponPrefixAbilityDescUI.ShowAbility(weapon.prefix);
            }
            else
            {
                weaponPrefixAbilityDescUI.gameObject.SetActive(false);
            }

            if (weapon.hasSuffixAbilitySkill())
            {
                weaponSuffixAbilityDescUI.gameObject.SetActive(true);
                weaponSuffixAbilityDescUI.ShowAbility(weapon.suffix);
            }
            else
            {
                weaponSuffixAbilityDescUI.gameObject.SetActive(false);
            }

            Armor armor = status.currentArmor;
            if (armor.hasPrefixAbilitySkill())
            {
                armorPrefixAbilityDescUI.gameObject.SetActive(true);
                armorPrefixAbilityDescUI.ShowAbility(armor.prefix);
            }
            else
            {
                armorPrefixAbilityDescUI.gameObject.SetActive(false);
            }

            if (armor.hasSuffixAbilitySkill())
            {
                armorSuffixAbilityDescUI.gameObject.SetActive(true);
                armorSuffixAbilityDescUI.ShowAbility(armor.suffix);
            }
            else
            {
                armorSuffixAbilityDescUI.gameObject.SetActive(false);
            }

            Helmet helmet = status.currentHelmet;
            if (helmet.hasPrefixAbilitySkill())
            {
                helmetPrefixAbilityDescUI.gameObject.SetActive(true);
                helmetPrefixAbilityDescUI.ShowAbility(helmet.prefix);
            }
            else
            {
                helmetPrefixAbilityDescUI.gameObject.SetActive(false);
            }

            if (helmet.hasSuffixAbilitySkill())
            {
                helmetSuffixAbilityDescUI.gameObject.SetActive(true);
                helmetSuffixAbilityDescUI.ShowAbility(helmet.suffix);
            }
            else
            {
                helmetSuffixAbilityDescUI.gameObject.SetActive(false);
            }

            Pants pants = status.currentPants;
            if (pants.hasPrefixAbilitySkill())
            {
                pantsPrefixAbilityDescUI.gameObject.SetActive(true);
                pantsPrefixAbilityDescUI.ShowAbility(pants.prefix);
            }
            else
            {
                pantsPrefixAbilityDescUI.gameObject.SetActive(false);
            }

            if (pants.hasSuffixAbilitySkill())
            {
                pantsSuffixAbilityDescUI.gameObject.SetActive(true);
                pantsSuffixAbilityDescUI.ShowAbility(pants.suffix);
            }
            else
            {
                pantsSuffixAbilityDescUI.gameObject.SetActive(false);
            }

            abilityPropertyObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)abilityPropertyObject.transform);
        }
    }
}