using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Battle.Core;

/*
 * ���� �÷��̾��� ��Ƽ�� ��ų ��ư UI
 */

namespace RPG.Battle.UI
{
    public class AbilityButton : MonoBehaviour
    {
        public float abilityCoolTime = 10f;     // ��Ƽ�� ��ų�� �⺻ ��Ÿ��
        public float currentCoolTime;           // ���� ��ų ��Ÿ��

        public Button AbilityBtn;                               // ���� ��ų ��ư
        [SerializeField] Image AbilityIconImage;                // ��ų �̹���
        [SerializeField] TextMeshProUGUI AbilityCoolTimeText;   // ��ų ��Ÿ�� �ؽ�Ʈ
        [SerializeField] Image abilityCoolTimeImage;            // ��ų ��Ÿ�� �̹���

        [SerializeField] bool canUse;   // ��ų�� ��밡������ ����

        // UI�� �ʱ�ȭ�մϴ�.
        public void Init(Sprite sprite, float coolTime)
        {
            AbilityIconImage.sprite = sprite;
            abilityCoolTime = coolTime;
        }

        // ��Ƽ�� ��ų�� ����մϴ�.
        public void UseAbility()
        {
            if (BattleManager.Instance.currentBattleState != BattleSceneState.Battle)
                // �������� �ƴ϶�� ����
                return;

            if (canUse)
                // ��ų�� ����� �� �ִ� ���¶��
            {
                // ��ų�� ����ϰ� ��Ÿ��üũ�� �����Ѵ�.
                StartCoroutine(CheckCoolTime());
            }
        }

        // ORDER : #9) ��ų ��Ÿ���� �����ִ� UI
        // ��ų ��Ÿ�� üũ�� �����մϴ�.
        public IEnumerator CheckCoolTime()
        {
            // ��ų ��Ÿ���� �����մϴ�.
            SetCool();
            while (true)
            {
                if (BattleManager.Instance.currentBattleState == BattleSceneState.Battle)
                    // ���� ���� ���̶�� 
                {
                    // ��Ÿ���̹����� fillAmount�� ���ҽ�ŵ�ϴ�.
                    abilityCoolTimeImage.fillAmount -= Time.deltaTime / abilityCoolTime;
                    // ���� ��Ÿ���� ���ҽ�ŵ�ϴ�.
                    currentCoolTime -= Time.deltaTime;
                    // ��Ÿ�� �ؽ�Ʈ�� ������Ʈ �մϴ�.
                    AbilityCoolTimeText.text = currentCoolTime.ToString("N1");
                    if (abilityCoolTimeImage.fillAmount <= 0)
                        // ��Ÿ���� �����ٸ� �ݺ��� ����
                    {
                        break;
                    }
                }
                yield return null;
            }
            // ��ų ����� �� �ֵ��� ����
            CanSkill();
        }

        // ���� ��ų�� ����� �� �ֵ��� �����մϴ�.
        public void CanSkill()
        {
            canUse = true;
            abilityCoolTimeImage.gameObject.SetActive(false);
            AbilityCoolTimeText.gameObject.SetActive(false);
            AbilityBtn.interactable = true;
        }

        // ��Ÿ��üũ�� �����մϴ�.
        private void SetCool()
        {
            canUse = false;
            abilityCoolTimeImage.fillAmount = 1;
            currentCoolTime = abilityCoolTime;
            abilityCoolTimeImage.gameObject.SetActive(true);
            AbilityCoolTimeText.gameObject.SetActive(true);
            AbilityBtn.interactable = false;
        }
    }
}