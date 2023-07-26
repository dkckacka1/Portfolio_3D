using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Battle.Core;
using DG.Tweening;

/*
 * ��Ƽ�������� �����ִ� UI Ŭ����
 */
namespace RPG.Battle.UI
{
    public class LootingItem : MonoBehaviour
    {
        public Transform targetPos;     // ���� UI�� ��ġ

        [SerializeField] Image rootImage;               // ���� �Ɵ�� �̹���
        [SerializeField] float minDistance;             // ����UI ���� �ٰ��� �ּҰŸ�
        [SerializeField] float bouncePointX;            // �ٿ�� X��ġ
        [SerializeField] float bouncePointY;            // �ٿ�� Y��ġ
        [SerializeField] float jumpTime;                // ������ �ð�
        [SerializeField] float moveTime;                // �̵� �ð�
        [SerializeField] List<LootingImage> lootings;   // ���� �������� ��������Ʈ

        public float jumpPower; // ���� �Ŀ�

        float timer;                // ���� �̵��� Ÿ�̸�
        float timeRate;             // ���� �̵� �ð� ����
        bool canMove = false;   // �̵� �������� ����

        // Ȱ��ȭ �� ���� �̵� ������ �ʱ�ȭ�մϴ�.
        private void OnEnable()
        {
            timer = 0;
            timeRate = 0;

            // ������ ��ġ�� �����մϴ�.
            Vector3 jumpPosition = new Vector3(transform.position.x + Random.Range(-bouncePointX, bouncePointX), transform.position.y);
            // ���� ��ġ���� �ٿ�մϴ�.
            // �ٿ�� ������ �̵������ϵ��� �����մϴ�
            transform.DOJump(jumpPosition, jumpPower, 3, jumpTime).OnComplete(() => { canMove = true; });
        }

        private void Update()
        {
            if (canMove)
            // �̵� �����ϴٸ�
            {
                if (targetPos == null) return;

                // ���� UI��ġ���� ���� �̵��� �����մϴ�.
                MoveLerp(this.transform, transform.position, targetPos.position, moveTime);
                if (Vector3.Distance(transform.position, targetPos.position) < minDistance)
                // �ּҰŸ� ���� �ٰ��Դٸ�
                {
                    //�̵� �Ұ� ���·� �����
                    // ������Ʈ Ǯ�� ���� �������� ��ȯ�մϴ�.
                    // ���� UI�� �ִϸ��̼��� �÷����մϴ�.
                    canMove = false;
                    BattleManager.ObjectPool.ReturnLootingItem(this);
                    targetPos.GetComponent<Animation>().Play();
                }
            }
        }

        // ���þ����ۿ� ����  UI�� �����մϴ�.
        public void SetUp(Transform targetPos)
        {
            this.targetPos = targetPos;
        }

        // ���þ����� Ÿ�Կ� ���� �����մϴ�.
        public void Init(Vector3 position, DropItemType type)
        {
            transform.position = position;

            try
            {
                rootImage.sprite = lootings.Find(item => item.type.Equals(type)).sprite;
            }
            catch
            {
                Debug.Log($"ã�� {type}�� �̹����� �����ϴ�.");
            }
        }

        // ���� �̵��� �����մϴ�.
        private void MoveLerp(Transform transform, Vector3 startPos, Vector3 endPos, float time)
        {
            timeRate = 1.0f / time;
            if (timer < 1.0f)
            {
                timer += Time.deltaTime * timeRate;
                transform.position = Vector3.Lerp(startPos, endPos, timer);
            }
        }
    }

}