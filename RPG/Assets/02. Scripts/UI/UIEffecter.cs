using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��������Ʈ�θ��� UI �ִϸ��̼� ����Ʈ Ŭ����
 */

namespace RPG.Core
{
    public abstract class UIEffecter : MonoBehaviour
    {
        public Canvas effectCanvas;     // ����Ʈ ĵ����

        public List<UIEffecter> effectList = new List<UIEffecter>();        // �ڱ� ���� UI����Ʈ ����Ʈ
        public bool isPlaying = false;                                      // ���� �÷��� ������ ����
        public bool isLoop = false;                                         // ���� �������� ����

        protected virtual void Awake()
        {
            // �ڱ� ���� UI ����Ʈ�� ����Ʈ�� �־��ݴϴ�.
            foreach (var effect in effectCanvas.GetComponentsInChildren<UIEffecter>())
            {
                effectList.Add(effect);
            }
        }

        // UI����Ʈ�� ����մϴ�.
        public void Play()
        {
            this.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        }

        // ����Ʈ�� ����մϴ�.
        public virtual void StartEffect()
        {
            effectCanvas.enabled = true;
            isPlaying = true;
        }

        // ����Ʈ�� �����մϴ�.
        public virtual void EndEffect()
        {
            // ���� ���� ���¸� ����
            if (isLoop) return;

            // �÷��̸� �����մϴ�.
            isPlaying = false;
            foreach (var effect in effectList)
            {
                if (effect.isPlaying)
                    return;
            }

            effectCanvas.enabled = false;
        }

        // ������ �����ŵ�ϴ�.
        public virtual void EndLoop()
        {
            isLoop = false;
            EndEffect();
        }
    }

}