using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������ ����� ���� ����� �����ϴ� �Ŵ��� Ŭ�����Դϴ�.
 */

namespace RPG.Main.Audio
{
    public class AudioManager : MonoBehaviour
    {
        // ��Ŭ�� Ŭ���� ����
        public static AudioManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    Debug.Log("AudioManager is NULL");
                    return null;
                }

                return instance;
            }
        }
        private static AudioManager instance;

        public float musicVolume = 100f;    // ������ ���� ����
        public float soundVolume = 100f;    // ������ ȿ���� ����

        [SerializeField] AudioSource musicSource;   // ���� �ҽ�
        [SerializeField] AudioSource soundSource;   // ȿ���� �ҽ�

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        // ���� Ŭ���� ����մϴ�.
        public void PlayMusic(string musicName, bool isLooping)
        {
            if (GameManager.Instance.audioDic.TryGetValue(musicName, out AudioClip audioClip))
                // ����� Ŭ���� �ҷ��� �����մϴ�.
            {
                musicSource.clip = audioClip;
                musicSource.loop = isLooping;
                musicSource.Play();
            }
            else
            {
                Debug.Log("musicName is NULL");
            }
        }

        // ȿ���� Ŭ���� ����մϴ�.
        public void PlaySound(string soundName)
        {
            if (GameManager.Instance.audioDic.TryGetValue(soundName, out AudioClip audioClip))
                // ����� Ŭ���� �ҷ��� �����մϴ�.
            {
                soundSource.clip = audioClip;
                soundSource.Play();
            }
            else
            {
                Debug.Log("soundName is NULL");
            }
        }

        // ȿ������ �ѹ� ����մϴ�.
        public void PlaySoundOneShot(string soundName)
        {
            if (GameManager.Instance.audioDic.TryGetValue(soundName, out AudioClip audioClip))
                // ����� Ŭ���� �ҷ��� ����մϴ�.
            {
                soundSource.PlayOneShot(audioClip);
            }
        }

        // ���� �������� �����մϴ�.
        public void ChangeMusicVolume(float value)
        {
            value = Mathf.Clamp(value, 0, 100);

            musicVolume = value;
            musicSource.volume = musicVolume / 100;

            GameManager.Instance.configureData.musicVolume = musicVolume;
        }

        // ȿ���� �������� �����մϴ�.
        public void ChangeSoundVolume(float value)
        {
            value = Mathf.Clamp(value, 0, 100);

            soundVolume = value;
            soundSource.volume = soundVolume / 100;

            GameManager.Instance.configureData.soundVolume = soundVolume;
        }
    }
}