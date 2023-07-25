using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using TMPro;
using RPG.Main.Audio;
using UnityEngine.UI;

/*
 *  ȯ�漳�� â UI Ŭ����
 */

namespace RPG.Main.UI
{
    public class ConfigureUI : MonoBehaviour
    {
        [SerializeField] Slider musicSlider;                    // ���� �����̴�
        [SerializeField] TextMeshProUGUI musicVolumeValueTxt;   // ���� ���� �ؽ�Ʈ
        [SerializeField] Slider soundSlider;                    // ȿ���� �����̴�
        [SerializeField] TextMeshProUGUI soundVolumeValueTxt;   // ȿ���� ���� �ؽ�Ʈ

        // â�� Ȱ��ȭ �ɶ� �������� �����ɴϴ�
        private void OnEnable()
        {
            musicSlider.value = AudioManager.Instance.musicVolume;
            soundSlider.value = AudioManager.Instance.soundVolume;
        }

        // â�� ��Ȱ��ȭ �Ǹ� ȯ�漳������ �����մϴ�.
        private void OnDisable()
        {
            if (GameManager.Instance == null)
            {
                return;
            }
            GameSLManager.SaveConfigureData(GameManager.Instance.configureData);
        }

        // ���� �������� �����մϴ�.
        public void ChangeMusicVolume()
        {
            musicVolumeValueTxt.text = musicSlider.value.ToString("F0");
            AudioManager.Instance.ChangeMusicVolume(musicSlider.value);
        }

        // ȿ���� �������� �����մϴ�.
        public void ChangeSoundVoume()
        {
            soundVolumeValueTxt.text = soundSlider.value.ToString("F0");
            AudioManager.Instance.ChangeSoundVolume(soundSlider.value);
        }

        // ������ �����մϴ�
        public void GameExit()
        {
            // ���� �����Ϳ� ȯ�漳�� �����͸� �����մϴ�.
            GameSLManager.SaveToJSON(GameManager.Instance.UserInfo, Application.dataPath + @"\Userinfo.json");
#if UNITY_EDITOR
            GameSLManager.SaveConfigureData(GameManager.Instance.configureData);
            UnityEditor.EditorApplication.isPlaying = false;
#else
            GameSLManager.SaveConfigureData(GameManager.Instance.configureData);
            Application.Quit();
#endif
        }
    }
}