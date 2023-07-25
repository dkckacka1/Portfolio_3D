using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;
using RPG.Battle.Core;

/*
 * ���� ȿ�� ����Ʈ Ŭ����
 */

namespace RPG.Battle.Ability
{
    public class ExplosionAbility : Ability
    {
        public float explosionRange = 1f; // ���� �ݰ�

        // ����Ʈ�� Ȱ��ȭ �Ǹ�
        protected override void OnEnable()
        {
            base.OnEnable();
            // ���� �ݰ� ���� �ִ� ��Ʈ�ѷ� ����Ʈ�� ����ϴ�.
            var list = CheckInsideExplosionController();

            if (hitAction != null)
                // ���� �̺�Ʈ�� �ִٸ�
            {
                foreach (var controller in list)
                {
                    // ���� �̺�Ʈ�� ȣ���մϴ�.
                    hitAction.Invoke(controller.battleStatus);
                }
            }

            if (chainAction != null)
                // ü�� �̺�Ʈ�� �ִٸ�
            {
                foreach (var controller in list)
                {
                    // ü�� �̺�Ʈ�� ȣ���մϴ�.
                    chainAction.Invoke(controller.battleStatus);
                }
            }
        }

        // �ݰ� ���� ��Ʈ�ѷ� ����Ʈ ����
        public List<Controller> CheckInsideExplosionController()
        {
            List<Controller> controllerList = new List<Controller>();

            if (BattleManager.Instance.livePlayer != null 
                && Vector3.Distance(this.transform.position, BattleManager.Instance.livePlayer.transform.position) < explosionRange)
                // �÷��̾ ����ְ� ���� �ݰ泻�� �ִٸ� ����Ʈ�� �߰��մϴ�.
            {
                controllerList.Add(BattleManager.Instance.livePlayer);
            }

            foreach (var enemy in BattleManager.Instance.liveEnemies)
                // ��� ����ִ� ���� ��ȸ�մϴ�.
            {
                if (BattleManager.Instance.liveEnemies != null
                    && Vector3.Distance(this.transform.position, enemy.transform.position) < explosionRange)
                    // ���� ���� �ݰ� ���� �ִٸ� ����Ʈ�� �߰��մϴ�.
                {
                    controllerList.Add(enemy);
                }
            }

            return controllerList;
        }
    }
}
