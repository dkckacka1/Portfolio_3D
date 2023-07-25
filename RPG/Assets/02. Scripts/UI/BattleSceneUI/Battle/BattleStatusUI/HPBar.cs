using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ü�¹� UI Ŭ����
 */

namespace RPG.Battle.UI
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] Slider hpSlider;       // ü�¹� �����̴�
        [SerializeField] Slider remainSlider;   // �ǰ� ü�¹� �����̴�

        Coroutine hpDownCoroutine;  // ü�� ���� �ڷ�ƾ

        // ü�¹ٸ� �ʱ�ȭ�մϴ�
        public virtual void InitHpSlider(int maxHp)
        {
            hpSlider.maxValue = maxHp;
            hpSlider.value= maxHp;
            remainSlider.maxValue = maxHp;
            remainSlider.value = maxHp;
        }

        public virtual void ChangeCurrentHP(int currentHp)
        {
            if (hpSlider.value >= currentHp)
            // ���� ü���� ���ҵ� ���
            {
                if (hpDownCoroutine != null)
                    // �̹� ü�� �������̾��ٸ� ��ҽ�ŵ�ϴ�.
                {
                    StopCoroutine(hpDownCoroutine);
                    hpDownCoroutine = null;
                }

                // ü�¹� ���� �ڷ�ƾ�� �����մϴ�.
                hpDownCoroutine = StartCoroutine(HPDownCoroutine());
            }
            else
            // ���� ü���� ������ ���
            {
                remainSlider.value = currentHp;
            }
            hpSlider.value = currentHp;
        }

        // ü�¹� �����ϴ� �ڷ�ƾ�Դϴ�.
        private IEnumerator HPDownCoroutine()
        {
            // �ǰ� ü�¹ٰ� �ٷ� ���ҵ��� �ʵ��� ��� ����մϴ�.
            yield return new WaitForSeconds(0.25f);
            // ���� ������ �����̴� ��
            float changeValue = (remainSlider.value - hpSlider.value) * 2;

            while (true)
            {
                // ���� ����ŭ �ǰ� �����̵带 �����մϴ�.
                remainSlider.value -= changeValue * Time.deltaTime;
                yield return null;

                if (remainSlider.value <= hpSlider.value)
                    // �ǰ� �����̴� ���� ���� ü�� �����̴� ������ ������ ��� �ݺ��� �ߴ�
                {
                    break;
                }
            }
        }
    }
}