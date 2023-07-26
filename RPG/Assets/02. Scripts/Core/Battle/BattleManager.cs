using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using RPG.Core;
using RPG.Battle.Control;
using RPG.Character.Status;
using RPG.Battle.UI;
using DG.Tweening;
using RPG.Main.Audio;

/*
 * ������ �����ϴ� �Ŵ��� Ŭ����
 */

namespace RPG.Battle.Core
{
    public class BattleManager : MonoBehaviour
    {
        // ORDER : #1) �̱��� ���� ��� ����
        // �̱��� Ŭ����
        private static BattleManager instance;
        public static BattleManager Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogWarning("BattleManager is NULL");
                    return null;
                }

                return instance;
            }
        }
        private static BattleSceneUIManager battleUI;                           // ���� UI ���� �Ŵ���
        public static BattleSceneUIManager BattleUI
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogWarning("BattleManager is NULL");
                    return null;
                }
                return battleUI;
            }
        }
        private static ObjectPooling objectPool;                                // ������Ʈ Ǯ
        public static ObjectPooling ObjectPool
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogWarning("BattleManager is NULL");
                    return null;
                }
                return objectPool;
            }
        }

        [Header("BattleCore")]
        // Component
        public BattleSceneState currentBattleState = BattleSceneState.Default;  // ���� ���� ����


        [Header("Controller")]
        public PlayerController livePlayer;                                     // ���� �÷��̾�
        public List<EnemyController> liveEnemies = new List<EnemyController>(); // ������ ������


        [Header("Stage")]
        public int currentStageFloor = 1;               // ���� �������� ����
        public float battleReadyTime = 2f;              // ���� ���� �ð�
        public float playerCreatePositionXOffset = 15f; // �÷��̾� ���� x��ǥ ��ġ
        public float EnemyCreatePositionXOffset = -18f; // �� ���� x��ǥ ��ġ
        public StageFomation stageFomation;             // ���� ���� ����

        private StageData stageData;    // �������� ������

        // Looting
        public int gainEnergy = 0;      // ������ ������
        public int gainGacha = 0;       // ������ ��í Ƽ��
        public int gainReinforce = 0;   // ������ ��ȭ Ƽ��
        public int gainIncant = 0;      // ������ ��æƮ Ƽ��
        
        private int currentStageGainEnergy = 0;     // ������ ���� ������
        private int currentStageGainGacha = 0;      // ������ ���� ��í Ƽ��
        private int currentStageGainReinforce = 0;  // ������ ���� ��ȭ Ƽ��
        private int currentStageGainIncant = 0;     // ������ ���� ��æƮ Ƽ��

        private readonly Dictionary<BattleSceneState, UnityEvent> battleEventDic = new Dictionary<BattleSceneState, UnityEvent>();  // ���� �̺�Ʈ ����
        private delegate void voidFunc();   // �Լ� ȣ�� ���� ��� ��������Ʈ

        [Header("Test")]
        [SerializeField] bool isTest;   // �׽�Ʈ ����

        private void Awake()
        {
            // �̱��� ����
            if (instance == null)
            {
                instance = this;
                battleUI = GetComponentInChildren<BattleSceneUIManager>();
                objectPool = GetComponentInChildren<ObjectPooling>();
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            // ������Ʈ Ǯ UI ����
            ObjectPool.SetUp(BattleUI.battleCanvas);
        }

        private void Start()
        {
            if (GameManager.Instance == null || isTest == true)
            {
                return;
            }

            // ���� ������ �����մϴ�.
            currentStageFloor = GameManager.Instance.choiceStageID;
            AudioManager.Instance.PlayMusic("BattleBackGroundMusic",true);
            // ������ �غ��մϴ�.
            Ready();
        }

        // ���� ���� ���¸� �����մϴ�.
        public void SetBattleState(BattleSceneState state)
        {
            this.currentBattleState = state;
            // ���� ���¿� ���� �̺�Ʈ�� ȣ���մϴ�.
            Publish(currentBattleState);
        }
        #region ���� ����



        // ������ �غ��մϴ�.
        private void Ready()
        {
            // �ߴ� ��ư�� �����ݴϴ�.
            BattleUI.InitResultBtn(false);
            // �� ������ �����ݴϴ�.
            BattleUI.ShowFloor(currentStageFloor);
            // ���� ���������� �ҷ��ɴϴ�.
            LoadCurrentStage();
            // ���� ���¸� �����մϴ�.
            SetBattleState(BattleSceneState.Ready);
            // ��� ��ų ����Ʈ�� ȸ���մϴ�.
            objectPool.ReleaseAllAbility();
            // ���� ���� �ð����� ����� ������ �����մϴ�.
            StartCoroutine(MethodCallTimer(() =>
            {
                Battle();
            }, battleReadyTime));
        }

        // �������� �¸��߽��ϴ�.
        private void Win()
        {
            if (isTest)
            {
                return;
            }

            if (GameManager.Instance.stageDataDic.ContainsKey(currentStageFloor + 1))
                // �������� �����Ѵٸ� 
            {
                // ���� ���¸� �¸��� �����մϴ�
                SetBattleState(BattleSceneState.Win);
                // ���� ������ �����մϴ�.
                currentStageFloor++;
                // �÷��̾� ĳ���Ͱ� �߾��� �ٶ󺸰� �̵��ϴ� �ִϸ��̼����� �����մϴ�.
                livePlayer.transform.LookAt(livePlayer.transform.position + Vector3.left);
                livePlayer.animator.ResetTrigger("Idle");
                livePlayer.animator.SetTrigger("Move");
                // �÷��̾ ���� �غ� �ð����� �÷��̾� ���� ��ġ�� �̵��ѵ� ���� �غ� ���·� �����մϴ�.
                livePlayer.transform.DOMoveX(EnemyCreatePositionXOffset, battleReadyTime).OnComplete(() => { Ready(); });
            }
            else
                // ���� ���� ���ٸ� ������ �����ݴϴ�.
            {
                // ���� ���¸� �������� �����ϰ� ���������� ����մϴ�.
                SetBattleState(BattleSceneState.Ending);
                AudioManager.Instance.PlayMusic("EndingBackGroundMusic", true);
            }

            // ���� ������ ������Ʈ �մϴ�.
            UpdateUserinfo();
        }

        // �������� �й��߽��ϴ�.
        private void Defeat()
        {
            if (isTest)
            {
                return;
            }

            // �й� ������ ����մϴ�.
            AudioManager.Instance.PlayMusic("DefeatMusicBackGround", false);
            // �й� ����
            SetBattleState(BattleSceneState.Defeat);
        }

        // ������ �����մϴ�.
        private void Battle()
        {
            // ���� ���·� �����մϴ�.
            SetBattleState(BattleSceneState.Battle);
        }

        // ������ �ߴ��մϴ�.
        private void Pause()
        {
            SetBattleState(BattleSceneState.Pause);
        }

        #endregion


        // ���� �� ID�� �������� �����͸� �ҷ��ɴϴ�
        private StageData LoadStageData()
        {
            if (GameManager.Instance.stageDataDic.TryGetValue(currentStageFloor, out StageData stage))
            {
                return stage;
            }

            Debug.Log("Stage Data in NULL!");
            return null;
        }

        // ĳ���Ͱ� �׾����ϴ�.
        public void CharacterDead(Controller controller)
        {
            if (controller is PlayerController)
                // ���� ĳ���Ͱ� �÷��̾� ĳ���͸�
            {
                // �й��մϴ�.
                Defeat();
            }
            else if (controller is EnemyController)
                // ���� ĳ���Ͱ� �� ĳ���Ͷ��
            {
                var enemy = controller as EnemyController;
                // ���� ������ ���� ĳ���͸� �����մϴ�.
                liveEnemies.Remove(enemy);
                // ������Ʈ Ǯ�� ���� ĳ���͸� �־��ݴϴ�.
                ObjectPool.ReturnEnemy((controller as EnemyController));
                if (liveEnemies.Count <= 0)
                    // ������ ���� �Ѹ� ���ٸ�
                {
                    // �¸��մϴ�.
                    Win();
                }
            }
        }

            // ���� �׾��� �� �������� �����մϴ�.
        public void LootingItem(EnemyController enemy)
        {
            EnemyData enemyData;
            if (GameManager.Instance.enemyDataDic.TryGetValue((enemy.battleStatus.status as EnemyStatus).enemyID, out enemyData))
                // ���� �� ĳ������ ������ ID�� ��ȸ�Ͽ� ���� ���̺��� �����ɴϴ�.
            {
                // �������� ������ ����մϴ�.
                // �������� ����� �� ���� ���� ���� ��ġ���� ����ϵ��� �մϴ�.
                ObjectPool.GetLootingItem(Camera.main.WorldToScreenPoint(enemy.transform.position), DropItemType.Energy, BattleUI.backpack.transform);
                // �������� ȹ���մϴ�.
                gainItem(DropItemType.Energy, enemyData.dropEnergy);

                foreach (var dropTable in enemyData.dropitems)
                    // ���������� ������̺��� ��ȸ�մϴ�.
                {
                    float random = Random.Range(0f, 100f);
                    if (random <= dropTable.percent)
                        // Ȯ�� ������ �����ߴٸ�
                    {
                        // �������� ����մϴ�.
                        // ������ Ÿ�Կ� �´� �̹����� �����ݴϴ�.
                        ObjectPool.GetLootingItem(Camera.main.WorldToScreenPoint(enemy.transform.position), dropTable.itemType, BattleUI.backpack.transform);
                        switch (dropTable.itemType)
                        {
                            case DropItemType.GachaItemScroll:
                                gainItem(DropItemType.GachaItemScroll, 1);
                                break;
                            case DropItemType.reinfoceScroll:
                                gainItem(DropItemType.reinfoceScroll, 1);
                                break;
                            case DropItemType.IncantScroll:
                                gainItem(DropItemType.IncantScroll, 1);
                                break;
                        }
                    }
                }
            }
        }

        // �������� ȹ���մϴ�.
        private void gainItem(DropItemType type, int count)
        {
            // ���� ������ Ÿ�Կ� ���� ������ŭ ȹ���մϴ�.
            switch (type)
            {
                case DropItemType.Energy:
                    currentStageGainEnergy += count;
                    break;
                case DropItemType.GachaItemScroll:
                    currentStageGainGacha += count;
                    break;
                case DropItemType.reinfoceScroll:
                    currentStageGainReinforce += count;
                    break;
                case DropItemType.IncantScroll:
                    currentStageGainIncant += count;
                    break;
            }
        }
        #region BattleSceneStateEvent

        #endregion
        // ���� ������ ������Ʈ �մϴ�.
        private void UpdateUserinfo()
        {
            // ���ӸŴ����� ���� ������ �����մϴ�.
            UserInfo userInfo = GameManager.Instance.UserInfo;
            // ������ ���� ���� ���� ���� ���� ������ ���ٸ� ���Ž�ŵ�ϴ�.
            if (userInfo.risingTopCount < currentStageFloor)
            {
                userInfo.risingTopCount = currentStageFloor;
            }

            // ���� ������ ȹ���� �����۵��� ������ �־��ݴϴ�.
            userInfo.energy += currentStageGainEnergy;
            userInfo.itemGachaTicket += currentStageGainGacha;
            userInfo.itemIncantTicket += currentStageGainIncant;
            userInfo.itemReinforceTicket += currentStageGainReinforce;

            // ȹ���� �����۵��� ȹ���� �Ѿ����ۿ� �����ݴϴ�.
            gainEnergy += currentStageGainEnergy;
            gainGacha += currentStageGainGacha;
            gainIncant += currentStageGainIncant;
            gainReinforce += currentStageGainReinforce;

            // ȹ���� �����۵��� �ʱ�ȭ��ŵ�ϴ�.
            currentStageGainEnergy = 0;
            currentStageGainGacha = 0;
            currentStageGainIncant = 0;
            currentStageGainReinforce = 0;

        }

        // ���� ���������� �ʱ�ȭ�մϴ�.
        private void ResetStage()
        {
            if (livePlayer != null)
            {
                livePlayer.gameObject.SetActive(false);
            }

            // ��� ���� Ǯ�� �־��ݴϴ�.
            foreach (var enemy in liveEnemies)
            {
                ObjectPool.ReturnEnemy(enemy);
            }

            liveEnemies.Clear();
        }




        #region LoadStage

        private void LoadCurrentStage()
        {
            stageData = LoadStageData();
            SetUpStage(stageData);
        }

        private void SetUpStage(StageData stage)
        {
            // PlayerSetting

            if (livePlayer == null)
            // �÷��̾ ���ٸ� ����
            {
                livePlayer = BattleManager.ObjectPool.CreatePlayer(GameManager.Instance.Player);
                livePlayer.battleStatus.currentState = CombatState.Actable;
            }

            Vector3 playerPosition = new Vector3(playerCreatePositionXOffset, stage.playerSpawnPosition.y, stage.playerSpawnPosition.z);
            (livePlayer.battleStatus.status as PlayerStatus).SetPlayerDefaultStatus(GameManager.Instance.Player);
            livePlayer.battleStatus.UpdateBehaviour();
            livePlayer.transform.position = playerPosition;
            livePlayer.transform.LookAt(livePlayer.transform.position + Vector3.left);
            livePlayer.animator.SetTrigger("Move");
            livePlayer.transform.DOMoveX(stage.playerSpawnPosition.x, battleReadyTime).OnComplete(() =>
            {
                livePlayer.animator.ResetTrigger("Move");
                livePlayer.animator.SetTrigger("Idle");
            });

            // EnemiesSetting
            Fomation fomation = stageFomation.FomationList.Find(temp => temp.fomationEnemyCount == stage.enemyDatas.Length);
            for (int i = 0; i < stage.enemyDatas.Length; i++)
            {
                EnemyData enemyData;
                if (GameManager.Instance.enemyDataDic.TryGetValue(stage.enemyDatas[i], out enemyData))
                {
                    Vector3 enemyPosition = new Vector3(EnemyCreatePositionXOffset, fomation.positions[i].y, fomation.positions[i].z);
                    EnemyController enemy = ObjectPool.GetEnemyController(enemyData, enemyPosition);
                    enemy.transform.LookAt(enemy.transform.position + Vector3.right);
                    enemy.animator.SetTrigger("Move");
                    enemy.transform.DOLocalMoveX(fomation.positions[i].x, battleReadyTime).OnComplete(() =>
                    {
                        enemy.animator.ResetTrigger("Move");
                        enemy.animator.SetTrigger("Idle");
                    });
                    enemy.battleStatus.currentState = CombatState.Actable;
                    liveEnemies.Add(enemy);
                }
            }
        }
        #endregion

        private IEnumerator MethodCallTimer(voidFunc func, float duration)
        {
            yield return new WaitForSeconds(duration);
            func.Invoke();
        }

        #region eventListener

        // �̺�Ʈ ����
        public void SubscribeEvent(BattleSceneState state, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                battleEventDic.Add(state, thisEvent);
            }
        }

        // �̺�Ʈ ���� ����
        public void UnsubscribeEvent(BattleSceneState state, UnityAction unityAction)
        {
            UnityEvent thisEvent;

            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.RemoveListener(unityAction);
            }
        }

        // ���� ���� �̺�Ʈ���� ȣ���մϴ�.
        public void Publish(BattleSceneState state)
        {
            UnityEvent thisEvent;

            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }





        #endregion

        // ORDER : #5) ���� �ڽ��� ��ġ���� ���� ����� ��Ʈ�ѷ��� ��ȯ�ϴ� �Լ�
        /// <summary>
        /// ���� ����� T�� ã�Ƽ� �����մϴ�.
        /// </summary>
        /// <typeparam name="T">Controller����</typeparam>
        public T ReturnNearDistanceController<T>(Transform transform) where T : Controller
        {
            if (typeof(T) == typeof(PlayerController))
            // ã�� ��Ʈ�ѷ��� �÷��̾� ��Ʈ�ѷ����
            {
                if (livePlayer != null)
                {
                    return livePlayer as T;
                }
            }
            else if (typeof(T) == typeof(EnemyController))
                // ã�� ��Ʈ�ѷ��� ���ʹ���Ʈ�ѷ� ���
            {
                // ������ ������Ʈ�� �����ɴϴ�.
                List<EnemyController> list = liveEnemies.Where(enemy => !enemy.battleStatus.isDead).ToList();
                // ������ ���� ���ٸ� null ����
                if (list.Count == 0) return null;

                // ����Ʈ�� ��ȸ�ϸ鼭 ���� ����� ���� ã���ϴ�.
                Controller nearTarget = list[0];
                float distance = Vector3.Distance(nearTarget.transform.position, transform.position);
                for (int i = 1; i < list.Count; i++)
                {
                    float newDistance = Vector3.Distance(list[i].transform.position, transform.position);

                    if (distance > newDistance)
                    {
                        nearTarget = list[i];
                        distance = newDistance;
                    }
                }

                // T Ÿ������ ����ȯ �ؼ� ���� ���ݴϴ�.
                return (T)nearTarget;
            }

            return null;
        }

        #region ButtonPlugin

        public void SetPause()
        {
            Pause();
        }

        public void ToMainScene()
        {
            ResetStage();
            SceneLoader.LoadMainScene();
        }

        public void ReStartBattle()
        {
            ResetStage();
            SceneLoader.LoadBattleScene(currentStageFloor);
        }

        public void ReturnBattle()
        {
            Battle();
            BattleUI.ReleaseResultUI();
        }
        #endregion
    }

}