using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using RPG.Battle.Core;
using DG.Tweening;

/*
 * ���� �ؽ�Ʈ UI Ŭ����
 */

namespace RPG.Battle.UI
{
    public class BattleText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;                  // �ؽ�Ʈ ����
        [SerializeField] float speed;                           // �ؽ�Ʈ �ӵ�
        [SerializeField] float deleteTiming;                    // �ؽ�Ʈ�� ���̵� �ƿ��� �ð�
        [SerializeField] List<DamageTextMaterial> materials;    // ���� �ؽ�Ʈ ���׸���

        float dir;  // ���� �ؽ�Ʈ�� ������ ��ġ�� �ڸ������� �ֵ��� �ϴ� ������

        // Ȱ��ȭ�Ǹ� �ٷ� ���̵�ƿ��� �����մϴ�
        private void OnEnable()
        {
            dir = Random.Range(-0.2f, 0.2f);
            // ���̵� �ƿ��� �Ϸ�Ǹ� �����ؽ�Ʈ�� ������Ʈ Ǯ�� ��ȯ�մϴ�.
            text.DOFade(0, deleteTiming).OnComplete(() => { ReleaseText(); });
        }

        private void Update()
        {
            // �������� �̵��մϴ�.
            transform.position += (new Vector3(dir, 1, 0) * speed * Time.deltaTime);
        }

        #region Initialize
        // ���� �ؽ�Ʈ�� �ʱ�ȭ �մϴ�
        public void Init(string textStr, Vector3 position, DamagedType type = DamagedType.Normal)
        {
            try
            {
                // ���� ���� Ÿ�Կ� �°� �ؽ�Ʈ�� ���׸����� �����մϴ�
                text.fontMaterial = materials.Find(mat => mat.type.Equals(type)).material;
            }
            catch
            {
                Debug.Log("���׸��� ���� ����");
            }


            // �ؽ�Ʈ�� ���İ��� 1�� �����ϰ� �ؽ�Ʈ�� ��ġ�� �����մϴ�.
            this.text.alpha = 1;
            this.transform.position = Camera.main.WorldToScreenPoint(position);
            this.text.text = textStr;
        }

        // ������Ʈ Ǯ�� ��ȯ�մϴ�.
        public void ReleaseText()
        {
            BattleManager.ObjectPool.ReturnText(this);
        }
        #endregion


    }
}