using RPG.Battle.Core;
using RPG.Core;
using RPG.Main.Audio;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * ���� �ߴ� UI Ŭ����
 */

namespace RPG.Battle.UI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] Slider musicSlider;                    // ���� �����̴�
        [SerializeField] TextMeshProUGUI musicVolumeValueTxt;   // ���� ���� �ؽ�Ʈ
        [SerializeField] Slider soundSlider;                    // ȿ���� �����̴�
        [SerializeField] TextMeshProUGUI soundVolumeValueTxt;   // ȿ���� ���� �ؽ�Ʈ

        // �ʱ�ȭ�մϴ�.
        public void Init()
        {
            // ���ӸŴ����� ����� �Ŵ����� ����
            this.transform.parent.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            // �������� �����ɴϴ�.
            musicSlider.value = AudioManager.Instance.musicVolume;
            soundSlider.value = AudioManager.Instance.soundVolume;
        }

        private void OnDisable()
        {
            GameSLManager.SaveConfigureData(GameManager.Instance.configureData);
        }

        // ���� �������� �����մϴ�.
        public void ChangeMusicVolum()
        {
            musicVolumeValueTxt.text = musicSlider.value.ToString("F0");
            AudioManager.Instance.ChangeMusicVolume(musicSlider.value);
        }

        // ȿ���� �������� �����մϴ�.
        public void ChangeSoundVolum()
        {
            soundVolumeValueTxt.text = soundSlider.value.ToString("F0");
            AudioManager.Instance.ChangeSoundVolume(soundSlider.value);
        }

        // UI�� �����ݴϴ�.
        public void Release()
        {
            this.transform.parent.gameObject.SetActive(false);
        }

        // ������ ���ư��ϴ�
        public void ReturnBattle()
        {
            BattleManager.Instance.ReturnBattle();
        }

        // ������ �ߴ��մϴ�.
        public void StopBattle()
        {
            SceneLoader.LoadMainScene();
        }
    }
}