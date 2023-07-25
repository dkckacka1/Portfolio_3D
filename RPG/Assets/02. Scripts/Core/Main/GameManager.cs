using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Character.Equipment;
using RPG.Character.Status;
using RPG.Battle.Core;
using RPG.Battle.Ability;

/*
 * ������ ��ü������ �������ִ� �Ŵ��� Ŭ����
 */

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        // �̱��� Ŭ���� ����
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log("GameManager is null");
                    return null;
                }

                return instance;
            }
        }

        private UserInfo userInfo;              // ������ ���� ����
        [SerializeField] PlayerStatus player;   // ���� �÷����� ĳ����
        public ConfigureData configureData;     // ���� ȯ�漳�� ������
        public int choiceStageID;               // ������ ������ �������� ID

        // Encapsule
        public UserInfo UserInfo
        {
            get
            {
                if (userInfo == null)
                {
                    Debug.LogError("Userinfo�� NULL �Դϴ�.");
                    return null;
                }
                return userInfo;
            }
            set => userInfo = value;
        }

        public PlayerStatus Player 
        {
            get
            { 
                if(player == null)
                {
                    Debug.LogError("PlayerStatus �� NULL�Դϴ�.");
                    return null;
                }
                return player;
            }
            set => player = value; 
        }

        #region DIC
        public Dictionary<int, StageData> stageDataDic = new Dictionary<int, StageData>();              // �������� ���� Dic
        public Dictionary<int, EnemyData> enemyDataDic = new Dictionary<int, EnemyData>();              // �� ���� Dic
        public Dictionary<int, EquipmentData> equipmentDataDic = new Dictionary<int, EquipmentData>();  // ��� ������ ������ Dic
        public Dictionary<int, Incant> incantDic = new Dictionary<int, Incant>();                       // ��� ��æƮ Dic
        public Dictionary<int, Ability> abilityPrefabDic = new Dictionary<int, Ability>();              // ��ų ����Ʈ Dic
        public Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();            // ����� Dic
        #endregion

        [Header("TEST")]
        [SerializeField] bool isTest;

        private void Awake()
        {
            // �̱��� Ŭ���� �۾�
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);

            // ���� �⺻ ������ ����
            Application.targetFrameRate = 60;

            if (isTest)
                // ���� �׽�Ʈ ���̶��
            {
                // �ٷ� ���ӵ����͸� �ε��ϰ� �ű� ���� ����, ĳ���� ����, ȯ�漳�� �����ͱ��� ���� �����.
                DataLoad();

                this.userInfo = CreateUserInfo();
                this.player.SetPlayerStatusFromUserinfo(userInfo);
                this.configureData = new ConfigureData();
            }
        }

        // ��� �����͸� �ε��մϴ�.
        public void DataLoad()
        {
            ResourcesLoader.LoadEquipmentData(ref equipmentDataDic);
            ResourcesLoader.LoadIncant(ref incantDic);
            ResourcesLoader.LoadEnemyData(ref enemyDataDic);
            ResourcesLoader.LoadStageData(ref stageDataDic);
            ResourcesLoader.LoadSkillPrefab(ref abilityPrefabDic);
            ResourcesLoader.LoadAudioData(ref audioDic);
        }

        #region UserInfo
        // ���ο� ���� ������ �����մϴ�.
        public UserInfo CreateUserInfo()
        {
            UserInfo userInfo = new UserInfo();
            // �⺻ �ڿ��� �ݴϴ�.
            userInfo.itemReinforceTicket = 10;
            userInfo.itemIncantTicket = 10;
            userInfo.itemGachaTicket = 10;
            userInfo.risingTopCount = 1;
            userInfo.energy = 0;
            
            // ĳ���Ͱ� �⺻������ ������ ��� �������Դϴ�.
            userInfo.lastedWeaponID = 100;
            userInfo.weaponReinforceCount = 0;
            userInfo.weaponPrefixIncantID = -1;
            userInfo.weaponSuffixIncantID = -1;

            userInfo.lastedArmorID = 200;
            userInfo.armorReinforceCount = 0;
            userInfo.armorPrefixIncantID = -1;
            userInfo.armorSuffixIncantID = -1;

            userInfo.lastedHelmetID = 300;
            userInfo.helmetReinforceCount = 0;
            userInfo.helmetPrefixIncantID = -1;
            userInfo.helmetSuffixIncantID = -1;

            userInfo.lastedPantsID = 400;
            userInfo.pantsReinforceCount = 0;
            userInfo.pantsPrefixIncantID = -1;
            userInfo.pantsSuffixIncantID = -1;

            return userInfo;
        }
        #endregion

        // ORDER : ���׸� ���� ������������ �������� �����͸� �������� �Լ�
        // ��� ������ �����͸� �����ɴϴ�.
        public bool GetEquipmentData<T>(int id,out T sourceData) where T : EquipmentData
        {
            EquipmentData data;
            if (!equipmentDataDic.TryGetValue(id, out data))
                // ã�� ID�� ���ٸ�
            {
                Debug.LogError("ã�� �����Ͱ� �����ϴ�.");
                sourceData = null;
                return false;
            }

            // ã�� �����͸� T �� ��ȯ�մϴ�.
            sourceData = data as T;
            if (sourceData == null)
                // ��ȯ ���� ���ٸ�
            {
                Debug.LogError("ã�� �����Ͱ� �߸��� �������Դϴ�.");
                return false;
            }

            return true;
        }
    }
}