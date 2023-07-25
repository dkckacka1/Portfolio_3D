using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using UnityEngine.UI;

/*
 * ��� ��ȭ�� UI ����Ʈ Ŭ����
 */

namespace RPG.Main.UI.BlackSmith
{
    public class UIEffecter_Reinforce : UIEffecter
    {
        [SerializeField] Image[] flareImages;   // �÷��� �̹���

        [SerializeField] float minHeight = 80;  // �÷����� �ִ� ����
        [SerializeField] float maxHeight = 120; // �÷����� �ּ� ����

        float flareAngle;   // �÷����� �ޱ۰�

        protected override void Awake()
        {
            base.Awake();

            // �� �÷�� ���� ���ִ� �ޱ۰��� �����մϴ�.
            flareAngle = 360 / flareImages.Length;
        }

        private void OnEnable()
        {
            float currentMinAngle = 0f;

            // �� �÷�� �ޱۿ� �°� �����մϴ�.
            // �� �÷����� ���̰� �ٸ����� �����մϴ�.
            for (int i = 0; i < flareImages.Length; i++)
            {
                float angle = Random.Range(currentMinAngle, flareAngle * (i + 1));

                flareImages[i].rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                flareImages[i].rectTransform.sizeDelta = new Vector2(flareImages[i].rectTransform.rect.width, Random.Range(minHeight, maxHeight));
                currentMinAngle += flareAngle;
            }
        }
    }

}