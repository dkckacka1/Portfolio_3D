using System;
using System.IO;
using UnityEngine;

/*
 * ������ ���̺�� �ε� ���õ� �Լ��� ��Ƴ��� static Ŭ�����Դϴ�.
 */

namespace RPG.Core
{
    public static class GameSLManager
    {
        public const string saveFileName = @"\Userinfo.json";   // ���� ����� �ҷ��Ë��� ���

        // ���� ������ JSON ���Ϸ� �����մϴ�.
        public static void SaveToJSON(UserInfo userinfo, string path)
        {
            var json = JsonUtility.ToJson(userinfo, true);

            File.WriteAllText(path, json);
        }

        [Obsolete]
        // ���� ������ JSON���Ϸ� �����մϴ�.
        public static void SaveToJSON(UserInfo userinfo)
        {
            var json = JsonUtility.ToJson(userinfo, true);

            File.WriteAllText(string.Join("/", Application.dataPath, saveFileName), json);
        }

        // ���� ��ο� ���̺� ������ �ִ��� Ȯ���մϴ�.
        public static bool isSaveFileExist(string path = null)
        {
            if (path == null)
            {
                return File.Exists(string.Join("/", Application.dataPath, saveFileName));
            }
            else
            {
                return File.Exists(path);
            }
        }

        // ���� ���� JSON������ �ε��մϴ�.
        public static UserInfo LoadFromJson()
        {
            string json;
            if (!File.Exists(string.Join("/", Application.dataPath, saveFileName)))
            {
                Debug.Log("SaveFile is not Exist");
                return null;
            }
            else
            {
                json = File.ReadAllText(string.Join("/", Application.dataPath, saveFileName));
            }

            UserInfo userinfo = JsonUtility.FromJson<UserInfo>(json);

            return userinfo;
        }

        // ���� ȯ�漳�� �����͸� �����մϴ�.
        public static void SaveConfigureData(ConfigureData data)
        {
            PlayerPrefs.SetFloat("MusicVolume", data.musicVolume);
            PlayerPrefs.SetFloat("SoundVolume", data.soundVolume);
        }

        // ���� ȯ�漳�� �����͸� �ҷ��ɴϴ�.
        public static ConfigureData LoadConfigureData()
        {
            ConfigureData configure = new ConfigureData();


            configure.musicVolume = PlayerPrefs.GetFloat("MusicVolume", 100f);
            configure.soundVolume = PlayerPrefs.GetFloat("SoundVolume", 100f);

            return configure;
        }
    }
}