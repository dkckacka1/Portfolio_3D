using RPG.Battle.Ability;
using RPG.Battle.Core;
using RPG.Character.Equipment;
using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * �����͸� �ҷ����� �޼��带 ��Ƴ��� static Ŭ���� �Դϴ�.
 */

namespace RPG.Core
{
    public static class ResourcesLoader
    {
        public const string dataPath = "Data";              // �����͸� ã�� ���
        public const string prefabPath = "Prefab";          // �������� ã�� ���
        public const string audioPath = "Audio";            // ����� �ҽ��� ã�� ���
        public const string equipmentPath = "Equipment";    // ������͸� ã�� ���
        public const string incantPath = "Incant";          // ��æƮ �����͸� ã�� ���
        public const string enemyPath = "Enemy";            // �� �����͸� ã�� ���
        public const string stagePath = "Stage";            // �������� �����͸� ã�� ���
        public const string skillPath = "Skill";            // ��ų �����͸� ã�� ���

        // �������� �����͸� �ҷ��ɴϴ�.
        public static void LoadEquipmentData(ref Dictionary<int, EquipmentData> dic)
        {
            var list = Resources.LoadAll<EquipmentData>(string.Join("/", dataPath, equipmentPath));
            foreach (var data in list)
            {
                dic.Add(data.ID, data);
            }
        }

        [Obsolete]
        public static void LoadEquipmentData(string path, ref Dictionary<int, EquipmentData> dic)
        {
            var list = Resources.LoadAll<EquipmentData>(path);
            foreach (var data in list)
            {
                dic.Add(data.ID, data);
            }
        }

        // �� �����͸� �ҷ��ɴϴ�.
        public static void LoadEnemyData(ref Dictionary<int, EnemyData> dic)
        {
            var enemies = Resources.LoadAll<EnemyData>(string.Join("/", dataPath, enemyPath));
            foreach (var enemy in enemies)
            {
                //Debug.Log(enemy.enemyName + "Loaded");
                dic.Add(enemy.ID, enemy);
            }
        }

        [Obsolete]
        // �� �����͸� �ҷ��ɴϴ�.
        public static void LoadEnemyData(string path, ref Dictionary<int, EnemyData> dic)
        {
            var enemies = Resources.LoadAll<EnemyData>(path);
            foreach (var enemy in enemies)
            {
                //Debug.Log(enemy.enemyName + "Loaded");
                dic.Add(enemy.ID, enemy);
            }
        }

        // �������� �����͸� �ҷ��ɴϴ�.
        public static void LoadStageData(ref Dictionary<int, StageData> dic)
        {
            var items = Resources.LoadAll<StageData>(string.Join("/", dataPath, stagePath));
            foreach (var item in items)
            {
                dic.Add(item.ID, item);
            }
        }

        [Obsolete]
        // �������� �����͸� �ҷ��ɴϴ�.
        public static void LoadStageData(string path, ref Dictionary<int, StageData> dic)
        {
            var items = Resources.LoadAll<StageData>(path);
            foreach (var item in items)
            {
                dic.Add(item.ID, item);
            }
        }

        // ��æƮ �����͸� �ε��մϴ�.
        public static void LoadIncant(ref Dictionary<int, Incant> dic)
        {
            var list = Resources.LoadAll<IncantData>(string.Join("/", dataPath, incantPath));

            foreach (var incantData in list)
            {
                if (incantData.isIncantAbility == false)
                    // ��ų�� ���ٸ� �׳� ����
                {
                    Incant instance = null;
                    switch (incantData.itemType)
                        // �� ��æƮ �������� ���Ÿ������ �˸´� ��æƮ Ŭ������ �����մϴ�.
                    {
                        case EquipmentItemType.Weapon:
                            instance = new WeaponIncant(incantData as WeaponIncantData);
                            break;
                        case EquipmentItemType.Armor:
                            instance = new ArmorIncant(incantData as ArmorIncantData);
                            break;
                        case EquipmentItemType.Pants:
                            instance = new PantsIncant(incantData as PantsIncantData);
                            break;
                        case EquipmentItemType.Helmet:
                            instance = new HelmetIncant(incantData as HelmetIncantData);
                            break;
                    }

                    dic.Add(incantData.ID, instance);
                }
                else
                    // ���� ��æƮ�� ��ų�� �پ��ִٸ� �˸´� �ڽ� Ŭ������ ������ݴϴ�.
                {
                    // Ŭ�����̸� �����
                    string class_name = $"RPG.Character.Equipment.{incantData.className}_{incantData.itemType}";
                    // Ŭ���� �̸��� ���� Ÿ�� �����
                    Type incantType = Type.GetType(class_name);

                    // �Ű������� �ִ� �����ڸ� ȣ���ؾ���
                    // Activator.CreateInstance�� �����ε� �Լ��� ȣ����Ѿ��ϱ⿡ objects ���� �����
                    object[] objects = { incantData };

                    var incantInstance = Activator.CreateInstance(incantType, objects) as Incant;

                    dic.Add(incantInstance.incantID, incantInstance);
                }
            }
        }

        [Obsolete]
        // ��æƮ �����͸� �ҷ��ɴϴ�.
        public static void LoadIncant(string path, ref Dictionary<int, Incant> dic)
        {
            var list = Resources.LoadAll<IncantData>(path);

            foreach (var incant in list)
            {
                // Ŭ�����̸� �����
                string class_name = $"RPG.Character.Equipment.{incant.className}_{incant.itemType}";
                // Ŭ���� �̸��� ���� Ÿ�� �����
                Type incantType = Type.GetType(class_name);

                // �Ű������� �ִ� �����ڸ� ȣ���ؾ���
                // Activator.CreateInstance�� �����ε� �Լ��� ȣ����Ѿ��ϱ⿡ objects ���� �����
                object[] objects = { incant };

                var incantInstance = Activator.CreateInstance(incantType, objects) as Incant;


                dic.Add(incantInstance.incantID, incantInstance);
            }
        }


        // ��ų ����Ʈ �������� �ҷ��ɴϴ�.
        public static void LoadSkillPrefab(ref Dictionary<int, Ability> dic)
        {
            var skills = Resources.LoadAll<Ability>(string.Join("/", prefabPath, skillPath));
            foreach (var skill in skills)
            {
                dic.Add(skill.abilityID, skill);
            }
        }

        [Obsolete]
        // ��ų ����Ʈ �������� �ҷ��ɴϴ�.
        public static void LoadSkillPrefab(string path, ref Dictionary<int, Ability> dic)
        {
            var skills = Resources.LoadAll<Ability>(path);
            foreach (var skill in skills)
            {
                dic.Add(skill.abilityID, skill);
            }
        }

        //����� �ҽ��� �ҷ��ɴϴ�.
        public static void LoadAudioData(ref Dictionary<string, AudioClip> dic)
        {
            var audios = Resources.LoadAll<AudioClip>(audioPath);

            foreach (var audio in audios)
            {
                dic.Add(audio.name, audio);
            }
        }

        [Obsolete]
        //����� �ҽ��� �ҷ��ɴϴ�.
        public static void LoadAudioData(string path, ref Dictionary<string, AudioClip> dic)
        {
            var audios = Resources.LoadAll<AudioClip>(path);

            foreach (var audio in audios)
            {
                dic.Add(audio.name, audio);
            }
        }
    }
}
