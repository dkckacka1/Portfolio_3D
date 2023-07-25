using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Character.Status;
using RPG.Battle.Control;
using RPG.Battle.Event;
using UnityEngine.Events;

/*
 * �̵� �ൿ ���� Ŭ����
 */

namespace RPG.Battle.Behaviour
{
    public class Movement
    {
        public bool canMove = true; // �̵� �������� ����
        public bool isMove;                     // ���� �̵������� ����

        public MoveEvent moveEvent;             // �̵��� ȣ���� �̺�Ʈ
        public IEnumerator moveEventCorotine;   // �̵� �ڷ�ƾ

        BattleStatus character; // ���� ĳ����
        NavMeshAgent nav;       // ���� ������Ʈ�� �׺꿡����Ʈ
        float attackRange;  // ���� ����

        // ���� ĳ���Ϳ� �׺긦 �����մϴ�.
        public Movement(BattleStatus character, NavMeshAgent nav)
        {
            this.character = character;
            this.nav = nav;
            moveEvent = new MoveEvent();
            moveEventCorotine = MoveEvent();
        }

        // �̵� �̺�Ʈ�� ȣ���մϴ�.
        public void AddMoveEvent(UnityAction<BattleStatus> action)
        {
            moveEvent.AddListener(action);
        }

        // �׺긦 �ʱ�ȭ�մϴ�. ���� �̵����̿��ٸ� �̵� ���
        public void ResetNav()
        {
            if (nav.enabled == true)
                nav.ResetPath();
        }

        // ��� Ʈ�������� ��ġ�� �̵��մϴ�.
        public void MoveNav(Transform target)
        {
            nav.SetDestination(target.position);
        }

        // Ÿ�� ���ͷ� �̵��մϴ�.
        public void MoveNav(Vector3 target)
        {
            nav.SetDestination(target);
        }

        //��� Ʈ��������ġ�� �̵��մϴ�. �׺�Ž� ��� X
        public void MovePos(Transform target)
        {
            Vector3 movementVector = new Vector3(target.position.x, 0, target.position.z);
            character.transform.LookAt(movementVector);
            //transform.Translate(Vector3.forward * status.movementSpeed * Time.deltaTime);
        }

        //��� ��ġ�� �̵��մϴ�. �׺�Ž� ��� X
        public void MovePos(Vector3 target)
        {
            character.transform.LookAt(target);
            character.transform.Translate(Vector3.forward * character.status.MovementSpeed * Time.deltaTime);
        }

        // ���ݹ����̻����� �ٰ����� �ʵ���
        // ��� Ʈ���������� ��ġ ���� ����մϴ�.
        public bool MoveDistanceResult(Transform target)
        {
            return Vector3.Distance(target.transform.position, this.character.transform.position) > character.status.AttackRange;
        }

        // �̵� �� ȣ���� �̺�Ʈ �ڷ�ƾ
        IEnumerator MoveEvent()
        {
            while (true)
            {
                if (isMove)
                {
                    moveEvent.Invoke(character);
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}