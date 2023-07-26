using RPG.Battle.Core;
using RPG.Core;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ORDER : #8) �������� ���� ������ ���� ��ũ�Ѹ� UI ����
// ���� URL : https://wonjuri.tistory.com/entry/Unity-UI-%EC%9E%AC%EC%82%AC%EC%9A%A9-%EC%8A%A4%ED%81%AC%EB%A1%A4%EB%B7%B0-%EC%A0%9C%EC%9E%91
// ��Ʈ�������� ��ũ�Ѻ�� �ؿ������� ���� �ö󰡱� ������ ������ URL�� ������ �ٸ��ϴ�.
/*
 * ���� ��ũ�Ѻ� UI Ŭ�����Դϴ�
 */


namespace RPG.Stage.UI
{
    [RequireComponent(typeof(ScrollRect))]
    [RequireComponent(typeof(RectTransform))]
    public class StageScrollViewController : MonoBehaviour
    {
        protected List<StageData> stageDataList = new List<StageData>(); // ����Ʈ �׸��� �����͸� ����
        [SerializeField]
        protected GameObject cellBase = null; // ���� ���� ��
        [SerializeField]
        private float spacingHeight = 4.0f; // �� ���� ����

        private LinkedList<StageFloorUI> cellList = new LinkedList<StageFloorUI>(); // �� ���� ����Ʈ

        private Rect visibleRect; // ����Ʈ �׸��� ���� ���·� ǥ���ϴ� ������ ��Ÿ���� �簢��

        private Vector2 prevScrollPos; // �ٷ� ���� ��ũ�� ��ġ�� ����

        public RectTransform CachedRectTransform => GetComponent<RectTransform>();  // ��Ʈ Ʈ������ ����
        public ScrollRect CachedScrollRect => GetComponent<ScrollRect>();   // ��ũ�� ��Ʈ ����

        public RawImage BackGroundImage;    // ���� ��׶��� �̹���

        [SerializeField] float contentBackGroundSpeed;  // ��ũ���Կ� ���� �̵��� ��׶��� �̹��� �ӵ�

        private int nameIndex = 0;

        protected virtual void Start()
        {
            // ���� ���� ���� ��Ȱ��ȭ �صд�.
            cellBase.SetActive(false);

            // Scroll Rect ������Ʈ�� OnvalueChanged�̺�Ʈ�� �̺�Ʈ �����ʸ� �����Ѵ�.
            CachedScrollRect.onValueChanged.AddListener(OnScrollPosChanged);

            // ���� �Ŵ����κ��� �������� ���� ����Ʈ�� �޾ƿɴϴ�.
            if (GameManager.Instance != null)
            {
                var list = GameManager.Instance.stageDataDic.ToList();
                // ���� ���� ���� ���� �ؿ� �ֵ��� �������ݴϴ�.
                list.Sort((value1, value2) => { return (value1.Value.ID > value2.Value.ID) ? 1 : -1; });
                foreach (var stageData in list)
                {
                    stageDataList.Add(stageData.Value);
                }

                CachedScrollRect.SetLayoutHorizontal();
            }

            // ��ũ�� �並 �ʱ�ȭ�մϴ�.
            InitializeTableView();

            // ������������ �ε�ɶ� 1���� ������ �� �ֵ��� �Ѵ�.
            CachedScrollRect.verticalNormalizedPosition = 0f;
        }


        // ����׿� �����
        private void OnDrawGizmosSelected()
        {
            Vector3[] corners = new Vector3[4];
            corners[0].x = visibleRect.x;
            corners[0].y = visibleRect.y;

            corners[1].x = visibleRect.x;
            corners[1].y = visibleRect.y + visibleRect.height;

            corners[2].x = visibleRect.xMax;
            corners[2].y = visibleRect.y + visibleRect.height;

            corners[3].x = visibleRect.xMax;
            corners[3].y = visibleRect.y;


            Gizmos.color = Color.red;
            Gizmos.DrawSphere(corners[0], 100f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(corners[1], 100f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(corners[2], 100f);
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(corners[3], 100f);
        }

        /// <summary>
        /// ���̺� �並 �ʱ�ȭ �ϴ� �Լ�
        /// </summary>
        protected void InitializeTableView()
        {
            UpdateScrollViewSize(); // ��ũ���� ������ ũ�⸦ �����Ѵ�.
            UpdateVisibleRect(); // visibleRect�� �����Ѵ�.

            if (cellList.Count < 1)
                // ���� �ϳ��� ���� ���
                // ������ �� ���� �ϳ� �ۼ��ؾ��մϴ�.
            {
                Vector2 cellBottom = new Vector2(0.0f, 0.0f); // ���� ���� ���� �ٴ� ��ġ

                for (int i = 0; i < stageDataList.Count; i++)
                {
                    float cellHeight = GetCellHeightAtIndex(i); // ���� ���� ����
                    Vector2 cellTop = cellBottom + new Vector2(0.0f, cellHeight);   // ���� ���� ����� ��ġ (���� �ٴڿ��� ���� ���̸�ŭ ���ϸ� ����� ���� �ȴ�.)
                    if ((cellBottom.y <= visibleRect.y && cellBottom.y >= visibleRect.y - visibleRect.height) ||
                        (cellTop.y <= visibleRect.y && cellTop.y >= visibleRect.y - visibleRect.height))
                        // �������l ���� ��Ʈ���� ������ ���� ���� �������
                    {
                        // ���� �����մϴ�.
                        StageFloorUI cell = CreateCellForIndex(i);
                        // ���� ��ġ�� ������ ���� ���� �ֵ��� �����մϴ�.
                        cell.Bottom = cellBottom;
                        break;
                    }

                    // ��Ʈ ���� ������ ���� ���� ���� �� ���� �� ���� ��ġ�� �����մϴ�.
                    cellBottom = cellTop + new Vector2(0.0f, spacingHeight);
                }

                // visibleRect�� ������ �� ���� ������ ���� �ۼ��Ѵ�.
                SetFillVisibleRectWithCells();
            }
            else
            {
                // �̹� ���� ���� ���� ù ��° �� ���� ������� �����ϴ� ����Ʈ �׸���
                // �ε����� �ٽ� �����ϰ� ��ġ�� ������ �����Ѵ�.
                LinkedListNode<StageFloorUI> node = cellList.First;
                UpdateCellForIndex(node.Value, node.Value.Index);

                node = node.Next;
                while (node != null)
                    // ���� ��尡 ���������� �ݺ��մϴ�.
                {
                    UpdateCellForIndex(node.Value, node.Previous.Value.Index + 1);
                    node.Value.Top = node.Previous.Value.Bottom + new Vector2(0.0f, -spacingHeight);
                }

                // visibleRect�� ������ �� ���� ������ ���� �ۼ��Ѵ�.
                SetFillVisibleRectWithCells();
            }
        }

        /// <summary>
        /// ���� ���̰��� �����ϴ� �Լ�
        /// </summary>
        protected virtual float GetCellHeightAtIndex(int index)
        {
            // ���� ���� ��ȯ�ϴ� ó���� ����� Ŭ�������� �����մϴ�.
            // ������ ũ�Ⱑ �ٸ� ��� ��ӹ��� Ŭ�������� �� �����մϴ�.
            // �ٸ� ���� ������ ũ�Ⱑ �����ϹǷ� �⺻ ��Ʈ Ʈ���������� ���̸� ��ȯ�մϴ�.
            return cellBase.GetComponent<RectTransform>().sizeDelta.y;
        }

        /// <summary>
        /// ��ũ���� ���� ��ü�� ���̸� �����ϴ� �Լ�
        /// </summary>
        protected void UpdateScrollViewSize()
        {
            float contentHeight = 0.0f;
            for (int i = 0; i < stageDataList.Count; i++)
            {
                // ��ü �������� �����͸�ŭ �� ����ǳ��̸� �����ݴϴ�.
                contentHeight += GetCellHeightAtIndex(i);

                if (i > 0)
                {
                    // ���� �� ������ ������ �ִٸ� �߰��� �����ݴϴ�.
                    contentHeight += spacingHeight;
                }
            }

            // ��ũ���� ������ ���̸� �����մϴ�.
            Vector2 sizeDelta = CachedScrollRect.content.sizeDelta;
            sizeDelta.y = contentHeight;
            CachedScrollRect.content.sizeDelta = sizeDelta;
        }

        /// <summary>
        /// ���� �����ϴ� �Լ�
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>The cell ofr index.</returns>
        private StageFloorUI CreateCellForIndex(int index)
        {
            // ���� ���� ���� �̿��� ���ο� ���� �����Ѵ�.
            GameObject obj = Instantiate(cellBase) as GameObject;
            obj.name = "StageFloor " + nameIndex++;
            obj.SetActive(false);
            StageFloorUI cell = obj.GetComponent<StageFloorUI>();

            // �θ� ��Ҹ� �ٲٸ� �������̳� ũ�⸦ �Ҿ�����Ƿ� ������ �����صд�.
            Vector3 scale = cell.transform.localScale;
            Vector2 sizeDelta = cell.CachedRectTrasnfrom.sizeDelta;
            Vector2 offsetMin = cell.CachedRectTrasnfrom.offsetMin;
            Vector2 offsetMax = cell.CachedRectTrasnfrom.offsetMax;

            // ���� ��ũ�Ѻ� ����Ʈ�� �ڽĿ�����Ʈ�� �����մϴ�.
            cell.transform.SetParent(cellBase.transform.parent);

            // ���� �����ϰ� ũ�⸦ �����Ѵ�.
            cell.transform.localScale = scale;
            cell.CachedRectTrasnfrom.sizeDelta = sizeDelta;
            cell.CachedRectTrasnfrom.offsetMin = offsetMin;
            cell.CachedRectTrasnfrom.offsetMax = offsetMax;

            // ������ �ε����� ���� ����Ʈ �׸� �����ϴ� ���� ������ �����Ѵ�.
            UpdateCellForIndex(cell, index);

            // ������ ���� ������Ʈ�� �־��ݴϴ�.
            cellList.AddLast(cell);

            return cell;
        }

        /// <summary>
        /// ���� ������ �����ϴ� �Լ�
        /// </summary>
        /// <param name="cell">Cell.</param>
        /// <param name="index">Index.</param>
        private void UpdateCellForIndex(StageFloorUI cell, int index)
        {
            // ���� �����ϴ� ����Ʈ �׸��� �ε����� �����Ѵ�.
            cell.Index = index;

            if (cell.Index >= 0 && cell.Index <= stageDataList.Count - 1)
            // ���� �����ϴ� ����Ʈ �׸��� �ִٸ� ���� Ȱ��ȭ�ؼ� ������ �����ϰ� ���̸� �����Ѵ�.
            {
                cell.gameObject.SetActive(true);
                // ���� �������� �����͸� �Ѱ��ݴϴ�.
                cell.UpdateContent(stageDataList[cell.Index]);
                cell.Height = GetCellHeightAtIndex(cell.Index);
            }
            else
            {
                // ���� �����ϴ� ����Ʈ �׸��� ���ٸ� ���� ��Ȱ��ȭ ���� ǥ�õ��� �ʰ� �Ѵ�.
                cell.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// VisibleRect�� �����ϱ� ���� �Լ�
        /// </summary>
        private void UpdateVisibleRect()
        {
            // visibleRect�� ��ġ�� ��ũ���� ������ �������κ��� ������� ��ġ��.
            visibleRect.x = CachedScrollRect.content.anchoredPosition.x + CachedRectTransform.rect.width;
            visibleRect.y = CachedScrollRect.content.anchoredPosition.y;

            visibleRect.width = CachedRectTransform.rect.width;
            visibleRect.height = CachedRectTransform.rect.height;
        }


        /// <summary>
        /// VisibleRect ������ ǥ�õ� ��ŭ�� ���� �����Ͽ� ��ġ�ϴ� �Լ�
        /// </summary>
        private void SetFillVisibleRectWithCells()
        {
            if (cellList.Count < 1)
                // ������Ʈ�� ����ٸ� ����
            {
                return;
            }

            // ǥ�õ� ������ ���� �����ϴ� ����Ʈ �׸��� ���� ����Ʈ �׸��� �ְ�
            // ���� �� ���� visibleRect�� ������ ���´ٸ� �����ϴ� ���� �ۼ��Ѵ�.
            StageFloorUI lastCell = cellList.Last.Value; // ���Ḯ��Ʈ ���� ���� �����մϴ�.
            int nextCellDataIndex = lastCell.Index + 1;
            Vector2 nextCellBottom = lastCell.Top + new Vector2(0.0f, -spacingHeight);  // ���� ���� �ٴ� �� = ������ ���� ���� �� + ���� ����

            while (nextCellDataIndex < stageDataList.Count && nextCellBottom.y < visibleRect.y + visibleRect.height)
                // ���� �����Ͱ� �����ϸ�, �������� �ٴ��� ������ ���� ���� �����Ѵٸ�
            {
                // ���� �����մϴ�.
                StageFloorUI cell = CreateCellForIndex(nextCellDataIndex);
                // ���� ���� ��ġ�� �����մϴ�.
                cell.Bottom = nextCellBottom;

                // �������� ���� �� �ֵ��� �����մϴ�.
                lastCell = cell;
                nextCellDataIndex = lastCell.Index + 1;
                nextCellBottom = lastCell.Top + new Vector2(0.0f, -spacingHeight);
            }
        }


        /// <summary>
        /// ��ũ�Ѻ䰡 ���������� ȣ��Ǵ� �Լ�
        /// </summary>
        /// <param name="scrollPos">Scroll position.</param>
        private void OnScrollPosChanged(Vector2 scrollPos)
        {
            // �������� ��ũ�� �䰡 �������ٸ� ������ ��ġ�� ���� ��׶��� �̹����� UV���� �����մϴ�.
            // UV���� �����ؼ� ��׶��尡 ���ӵǵ��� ����ϴ�.
            Rect uvRect = BackGroundImage.uvRect;
            uvRect.y = scrollPos.y * (CachedScrollRect.content.rect.height / contentBackGroundSpeed);
            BackGroundImage.uvRect = uvRect;

            // ������ ������ �����մϴ�.
            UpdateVisibleRect();

            // ��ũ�Ѻ䰡 �Ʒ��� ���������� ���� ���������� üũ�ؼ� ���� ������Ʈ�մϴ�.
            UpdateCells((scrollPos.y < prevScrollPos.y) ? 1 : -1);
            prevScrollPos = scrollPos;
        }

        // ���� �����Ͽ� ǥ�ø� �����ϴ� �Լ�
        private void UpdateCells(int scrollDirection)
        {
            if (cellList.Count < 1)
                // ������Ʈ�� ����ִٸ� ����
            {
                return;
            }

            if (scrollDirection > 0)
                // ���� ��ũ���ϰ� ���� ���� ������ �������� ���� �ִ� ����
                // �Ʒ��� ���� ������� �̵����� ������ �����Ѵ�.
            {
                StageFloorUI lastCell = cellList.Last.Value;
                while (lastCell.Bottom.y > -visibleRect.y + visibleRect.height)
                    // ������ ���� �ٴڰ��� ������ ������ ����� ������ �����ƴٸ�
                {
                    // ������ ���� ù��° ���� ������ �ִ� ������ �� ������ �־��ݴϴ�.
                    StageFloorUI firstCell = cellList.First.Value;
                    UpdateCellForIndex(lastCell, firstCell.Index - 1);

                    // ������ ���� ����Ⱑ ù��° ���� �ٴڿ� ��ġ�Ҽ� �ֵ��� ��ġ�� �����մϴ�.
                    lastCell.Top = firstCell.Bottom + new Vector2(0.0f, -spacingHeight);

                    // ���������� ���Ḯ��Ʈ�� ù��° ���� ����ϴ�.
                    cellList.AddFirst(lastCell);
                    cellList.RemoveLast();

                    // ���Ḯ��Ʈ�� ������ ����Ǿ����� ���������� �����մϴ�.
                    lastCell = cellList.Last.Value;
                }

            }
            else if (scrollDirection < 0)
                // �Ʒ��� ��ũ���ϰ� ���� ���� ������ �������� �ؿ� �ִ� ����
                // ���� ���� ������� �̵����� ������ �����մϴ�.
            {
                StageFloorUI firstCell = cellList.First.Value;
                while (firstCell.Top.y < -visibleRect.y)
                    // ù��° ���� ����� ���� ������ ������ �ٴ� ������ �����ƴٸ�
                {
                    //ù��° ���� ������ ���� ������ �ִ� ������ ���� ������ �־��ݴϴ�.
                    StageFloorUI lastCell = cellList.Last.Value;
                    UpdateCellForIndex(firstCell, lastCell.Index + 1);

                    // ù��° ���� �ٴڰ��� ���������� ����Ⱑ �� �� �ֵ��� ��ġ�� �����մϴ�.
                    firstCell.Bottom = lastCell.Top + new Vector2(0.0f, spacingHeight);

                    // ù��° ���� ���Ḯ��Ʈ�� ������ ���� ����ϴ�.
                    cellList.AddLast(firstCell);
                    cellList.RemoveFirst();

                    // ���Ḯ��Ʈ�� ������ ����Ǿ����� ù��° ���� �����մϴ�.
                    firstCell = cellList.First.Value;
                }

                // ���� ��ũ�� �並 �����̸鼭 �� ������ ��Ÿ�� �� �����Ƿ� ������� ä���ݴϴ�.
                SetFillVisibleRectWithCells();
            }
        }

    }

}
