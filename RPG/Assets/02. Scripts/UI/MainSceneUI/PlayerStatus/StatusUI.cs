using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 * ������ ����â UI Ŭ����
 */

namespace RPG.Main.UI.StatusUI
{
    public class StatusUI : MonoBehaviour
    {
        [SerializeField] UserinfoDescUI userinfoUI;     // ���� ���� UI
        [SerializeField] PlayerStatusDescUI statusUI;   // ĳ������ ���� ���� UI

        [SerializeField] float scaleAnimDuration = 1f;  // ������ �ִϸ��̼� �ð�

        private void OnEnable()
        {
            // ������ �ִϸ��̼��� �մϴ�.
            transform.localScale = Vector3.zero;
            // ����ġ���� ���ݴ� Ŀ���ٰ� �پ��� �ϱ����� Ease�� �����մϴ�.
            transform.DOScale(Vector3.one, scaleAnimDuration).SetEase(Ease.InOutBounce);
        }
    }
}