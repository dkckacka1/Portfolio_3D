using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * �÷��̾� ���� HP ü�¹� UI Ŭ����
 */

namespace RPG.Battle.UI
{
    public class PlayerHPBar : HPBar
    {
        public TextMeshProUGUI hpText;  // ���� ü�� �ؽ�Ʈ

        private int maxHp;  // �ִ� ü�� ��ġ

        // ü���� ��ȭ���� �� �޼���
        public override void ChangeCurrentHP(int currentHp)
        {
            base.ChangeCurrentHP(currentHp);
            hpText.text = $"{currentHp}  /  {maxHp}";

        }

        // ü�¹ٸ� �ʱ�ȭ�մϴ�.
        public override void InitHpSlider(int maxHp)
        {
            base.InitHpSlider(maxHp);
            this.maxHp = maxHp;
            hpText.text = $"{maxHp}  /  {maxHp}";
        }
    } 
}
