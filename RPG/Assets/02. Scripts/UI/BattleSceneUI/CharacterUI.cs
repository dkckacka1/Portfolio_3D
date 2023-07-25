using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Character.Status;

/*
 * ���� ĳ���� UI Ŭ����
 */

namespace RPG.Battle.UI
{
    public abstract class CharacterUI : MonoBehaviour
    {
        public Canvas battleCanvas;     // ���� ĵ����
        public BattleStatus status;     // ĳ������ ����

        [Header("HPUI")]
        public HPBar hpBar; // ĳ������ ü�¹�

        [Header("DebuffUI")]
        public DebuffUI debuffUI;   // ����� UI

        [Header("BattleText")]
        public Vector3 battleTextOffset;    // ���� �ؽ�Ʈ�� ��Ÿ�� ������

        private void Awake()
        {
            // �ʱ�ȭ�մϴ�.
            status = GetComponent<BattleStatus>();
            SetUp();
        }
        public virtual void SetUp()
        {
            battleCanvas = BattleManager.BattleUI.battleCanvas;
        }

        // UI�� �����մϴ�.
        public virtual void Init()
        {
            if (hpBar != null)
            {
                hpBar.gameObject.SetActive(true);
                hpBar.InitHpSlider(status.status.MaxHp);
                debuffUI.ResetAllDebuff();
            }
        }

        // UI�� ���ݴϴ�.
        public virtual void ReleaseUI()
        {
            if (hpBar != null)
            {
                hpBar.gameObject.SetActive(false);
            }
        }

        // ü�¹ٸ� ������Ʈ �մϴ�.
        public void UpdateHPUI(int currentHP)
        {
            if(hpBar != null)
            {
                hpBar.ChangeCurrentHP(currentHP);
            }
        }

        // ���� �ؽ�Ʈ�� ǥ���մϴ�.
        public void TakeDamageText(string damage, DamagedType type = DamagedType.Normal)
        {
            // ���� �ڽ��� ��ġ + ������ ��ġ�� ǥ���մϴ�.
            BattleManager.ObjectPool.GetText(damage.ToString(), this.transform.position + battleTextOffset, type);
        }
    }
}