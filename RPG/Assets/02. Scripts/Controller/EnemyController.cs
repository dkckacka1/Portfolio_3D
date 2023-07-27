
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.Behaviour;
using RPG.Battle.AI;

/*
 * �� ĳ������ ��Ʈ�ѷ� Ŭ����
 */

namespace RPG.Battle.Control
{
    public class EnemyController : Controller
    {
        // ����� �����մϴ�.
        public override bool SetTarget(out Controller controller)
        {
            // ����� �÷��̾� ��Ʈ�ѷ��� �����մϴ�.
            controller = BattleManager.Instance.ReturnNearDistanceController<PlayerController>(transform);
            if(controller != null)
            {
                this.target = controller;
                attack.SetTarget(controller.battleStatus);
            }

            return (controller != null);
        }

        // �������� �����մϴ�.
        public void LootingItem()
        {
            BattleManager.Instance.LootingItem(this);
        }
    }
}
