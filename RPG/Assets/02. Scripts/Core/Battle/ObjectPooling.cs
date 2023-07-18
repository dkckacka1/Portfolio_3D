using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.UI;
using RPG.Battle.Control;
using RPG.Character.Status;
using UnityEditor;
using RPG.Battle.Ability;
using RPG.Character.Equipment;
using RPG.Core;
using UnityEngine.Events;

/*
 * ���� �� ���� ������Ʈ Ǯ �ý��� Ŭ����
 */

namespace RPG.Battle.Core
{
    public class ObjectPooling : MonoBehaviour
    {
        [SerializeField] PlayerController playerController;     // �÷��̾� ĳ���� ������
        [SerializeField] EnemyController enemyController;       // �� ĳ���� ������
        [SerializeField] GameObject battleTextPrefab;           // ���� �ؽ�Ʈ ������
        [SerializeField] LootingItem lootingItem;               // ���þ����� ������
        [SerializeField] Transform playerParent;                // �÷��̾� ĳ���� �θ� ������Ʈ
        [SerializeField] Transform enemyParent;                 // �� ĳ���� �θ� ������Ʈ
        [SerializeField] Transform battleTextParent;            // ���� �ؽ�Ʈ �θ� ������Ʈ
        [SerializeField] Transform LootingItemParent;           // ���� ������ �θ� ������Ʈ
        [SerializeField] Transform abilityParent;               // ��ų ����Ʈ �θ� ������Ʈ

        Canvas battleCanvas;    // ���� UI ĵ����

        // ���� UI ĵ������ �����մϴ�.
        public void SetUp(Canvas canvas)
        {
            this.battleCanvas = canvas;
        }

        // �÷��̾� ĳ���͸� �����մϴ�.
        public PlayerController CreatePlayer(PlayerStatus status)
        {
            // �÷��̾� ĳ���͸� �����մϴ�.
            PlayerController controller = Instantiate<PlayerController>(playerController, playerParent);
            // �÷��̾� ������ �������ݴϴ�.
            var controllerStatus = (controller.battleStatus.status as PlayerStatus);
            controllerStatus.SetPlayerStatusFromStatus(status, controller.GetComponentInChildren<CharacterAppearance>());
            controller.gameObject.SetActive(true);

            return controller;
        }

        #region Enemy
        Queue<EnemyController> enemyControllerPool = new Queue<EnemyController>(); // �� ĳ���� ������Ʈ Ǯ

        // �� ĳ���͸� �����մϴ�.
        private EnemyController CreateController()
        {
            EnemyController enemy = Instantiate<EnemyController>(enemyController, enemyParent);
            return enemy;
        }
        
        // �� ĳ���͸� ������Ʈ Ǯ���� �����ɴϴ�.
        public EnemyController GetEnemyController(EnemyData data, Vector3 position)
        {
            EnemyController enemy;

            if (enemyControllerPool.Count > 0)
            {
                // Ǯ�� �����ִٸ� �����ִ� ��Ʈ�ѷ� ��Ȱ��
                enemy = enemyControllerPool.Dequeue();
            }
            else
            {
                // Ǯ�� ����ִٸ� ����
                enemy = CreateController();
            }

            // �� ĳ������ ������ ����
            (enemy.battleStatus.status as EnemyStatus).ChangeEnemyData(data);
            // �� ���� ����
            SetEnemyLook(ref enemy,ref data);
            // �� ��ġ�� ����
            enemy.gameObject.transform.localPosition= position;
            // Ȱ��ȭ
            enemy.gameObject.SetActive(true);

            return enemy;
        }

        // �� ĳ������ ������ �����մϴ�.
        public void SetEnemyLook(ref EnemyController enemy,ref EnemyData data)
        {
            // ������ �´� ������Ʈ�� �����ݴϴ�
            enemy.transform.GetChild(data.apperenceNum).gameObject.SetActive(true);
            enemy.GetComponent<CharacterAppearance>().EquipWeapon(data.weaponApparenceID, data.handleType);
        }


        // �� ĳ���͸� ������Ʈ Ǯ�� �־��ݴϴ�.
        public void ReturnEnemy(EnemyController enemy)
        {
            // Ǯ�� �־��ݴϴ�
            enemyControllerPool.Enqueue(enemy);
            // ���� ������ ���ݴϴ�
            enemy.transform.GetChild((enemy.battleStatus.status as EnemyStatus).apperenceNum).gameObject.SetActive(false);

            enemy.gameObject.SetActive(false);
        }

        #endregion

        #region BattleText

        Queue<BattleText> battleTextPool = new Queue<BattleText>(); // ���� �ؽ�Ʈ ������Ʈ Ǯ

        // ���� �ؽ�Ʈ�� �����մϴ�.
        public BattleText CreateText()
        {
            // �θ� ������Ʈ ������ ���� �ؽ�Ʈ�� �����մϴ�.
            GameObject obj = Instantiate(battleTextPrefab, battleTextParent.transform);
            BattleText text = obj.GetComponent<BattleText>();
            return text;
        }

        // ������Ʈ Ǯ�� �ִ� ���� �ؽ�Ʈ�� ��ȯ�մϴ�.
        public BattleText GetText(string textStr, Vector3 position, DamagedType type = DamagedType.Normal)
        {
            BattleText text;

            if (battleTextPool.Count > 0)
            {
                // Ǯ�� �ִ� �� ���
                text = battleTextPool.Dequeue();

            }
            else
            {
                // ���� ���� Ǯ�� �ֱ�
                text = CreateText();
            }

            // ���� �ؽ�Ʈ�� �����մϴ�.
            text.Init(textStr, position, type);
            text.gameObject.SetActive(true);

            return text;
        }

        // ���� �ؽ�Ʈ�� Ǯ�� ��ȯ�մϴ�.
        public void ReturnText(BattleText text)
        {
            text.gameObject.SetActive(false);
            battleTextPool.Enqueue(text);
        }
        #endregion

        #region LootingItem

        Queue<LootingItem> LootingItemPool = new Queue<LootingItem>();  // ���� ������ ������Ʈ Ǯ

        // ���þ������� �����մϴ�.
        public LootingItem CreateLooitngItem(Transform backpackTransform)
        {
            // �θ� ������Ʈ ������ ���þ����۸��������մϴ�.
            LootingItem item = Instantiate(lootingItem, LootingItemParent);
            // ���þ������� ã�ư� ���� ������Ʈ�� ��ġ�� �����մϴ�.
            item.SetUp(backpackTransform);
            return item;
        }

        // ������Ʈ Ǯ���� ���þ������� �����ɴϴ�
        public LootingItem GetLootingItem(Vector3 position, DropItemType type, Transform backpackTransform)
        {
            LootingItem lootingItem;

            if (LootingItemPool.Count > 0)
                // Ǯ�� �����ִٸ� ��Ȱ���մϴ�.
            {
                lootingItem = LootingItemPool.Dequeue();
            }
            else
            {
                // Ǯ�� ����ִٸ� ���Ӱ� �����մϴ�.
                lootingItem = CreateLooitngItem(backpackTransform);
            }

            // ���þ������� �����մϴ�.
            lootingItem.Init(position, type);
            lootingItem.gameObject.SetActive(true);

            return lootingItem;
        }

        // ���þ������� Ǯ�� ��ȯ�մϴ�
        public void ReturnLootingItem(LootingItem item)
        {
            item.gameObject.SetActive(false);
            LootingItemPool.Enqueue(item);
        }

        #endregion

        #region Skill

        List<Ability.Ability> abilityPool = new List<Ability.Ability>();    // ��ų ����Ʈ ������Ʈ Ǯ

        // ��ų ����Ʈ�� �����մϴ�
        private Ability.Ability CreateAbility(int abilityID)
        {
            // �ش� ID�� �´� ��ų����Ʈ�� �����մϴ�.
            Ability.Ability prefab = GameManager.Instance.abilityPrefabDic[abilityID];
            var ability = Instantiate(prefab, abilityParent);
            abilityPool.Add(ability);  
            return ability;
        }

        // ��ų ����Ʈ�� ������Ʈ Ǯ���� �����ɴϴ�.
        public Ability.Ability GetAbility(int abilityID)
        {
            Ability.Ability getAbility;

            if (abilityPool.Count > 0)
            {
                // �ʿ��� ��ų ����Ʈ ID�� Ǯ���ִ��� Ȯ���մϴ�.
                if ((getAbility = abilityPool.Find(ability => (ability.abilityID == abilityID) && (!ability.gameObject.activeInHierarchy))) == null)
                {
                    // Ǯ�� �˸��� ����Ʈ�� ���ٸ� ���Ӱ� �����մϴ�.
                    getAbility = CreateAbility(abilityID);
                }
            }
            else
            {
                // Ǯ�� �ƿ� ����� �ִٸ� �����ɴϴ�.
                getAbility = CreateAbility(abilityID);
            }

            getAbility.gameObject.SetActive(true);
            return getAbility;
        }

        // ��ų ����Ʈ�� ������ ȿ���� ���� �� ȿ���� ����۰� ������Ʈ Ǯ���� �����ɴϴ�
        public Ability.Ability GetAbility(int abilityID, Transform starPos, UnityAction<BattleStatus> hitAction = null, UnityAction<BattleStatus> chainAction = null, Space space = Space.Self)
        {
            Ability.Ability getAbility;

            if (abilityPool.Count > 0)
            {
                // �ʿ��� ��ų ����Ʈ ID�� Ǯ���ִ��� Ȯ���մϴ�.
                if ((getAbility = abilityPool.Find(ability => (ability.abilityID == abilityID) && (!ability.gameObject.activeInHierarchy))) == null)
                {
                    // Ǯ�� �˸��� ����Ʈ�� ���ٸ� ���Ӱ� �����մϴ�.
                    getAbility = CreateAbility(abilityID);
                }
            }
            else
            // Ǯ�� �ƿ� ����� �ִٸ� �����ɴϴ�.
            {
                getAbility = CreateAbility(abilityID);
            }

            // ��ų ����Ʈ�� �̺�Ʈ�� �����մϴ�.
            getAbility.InitAbility(starPos, hitAction, chainAction, space);
            getAbility.gameObject.SetActive(true);
            return getAbility;
        }

        // ��ų ����Ʈ�� Ǯ�� ��ȯ�մϴ�.
        public void ReturnAbility(Ability.Ability ability)
        {
            ability.transform.position = Vector3.zero;
            ability.gameObject.SetActive(false);
        }

        // ��� ��ų ����Ʈ�� Ǯ�� ��ȯ�մϴ�.
        public void ReleaseAllAbility()
        {
            foreach (var ability in abilityPool)
            {
                ReturnAbility(ability);
            }
        }

        #endregion
    }
}