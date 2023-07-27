using RPG.Battle.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��� ���� Ŭ����
 */

namespace RPG.Battle.AI
{
    public class DeadState : State, IState
    {
        float deadTimer = 0f;            // ���� Ÿ�̸�
        float deadTime = 2f;     // ���� �ִϸ��̼��� ���ö� ���� ��ٸ� �ð�
        bool callDeadEvent = true;      // ���� �̺�Ʈ�� ��µ� �������� Ȯ��

        public DeadState(Controller controller) : base(controller)
        {
        }

        public void OnStart()
        {
            // ���� ���¿� �����ϸ� ���� AI ���¸� �������·� �������ݴϴ�.
            controller.currentState = AIState.Dead;
            deadTimer = 0f;
            callDeadEvent = true;
            // �����ϴ� �����̾��ٸ� ������ ����ϰ� ���� �ִϸ��̼��� �����ݴϴ�.
            controller.StopAttack();
            controller.animator.SetTrigger("Dead");
            // �׺�޽��� ���ݴϴ�.
            controller.nav.enabled = false;

            // TODO : ���� ���°� �� �ൿ�� �ƴ�
            if (controller is EnemyController)
            {
                // ���� �� ��Ʈ�ѷ����ٸ� �������� �����մϴ�.
                (controller as EnemyController).LootingItem();
            }
        }

        public void OnEnd()
        {
            // ��� ���� �ൿ ����
        }

        public void OnUpdate()
        {
            // ���� Ÿ�̸Ӹ� ����մϴ�.
            deadTimer += Time.deltaTime;
            if (callDeadEvent && deadTimer > deadTime)
                // ���� �ð��� �����ٸ�
            {
                callDeadEvent = false;
                // ��Ʈ�ѷ��� ���� �޼��带 ȣ���մϴ�.
                controller.DeadController();
            }
        }
    }
}

