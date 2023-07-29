using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Control;
using RPG.Character.Status;
using RPG.Battle.Event;

/*
 * ���� �ൿ ���� Ŭ����
 */

namespace RPG.Battle.Behaviour
{
    public class Attack
    {
        public bool canAttack = true;           // ������ �� �ִ��� ����
        public bool isAttack = false;           // ���������� ����
        public float attackDelay = 1;           // ���� ������ 
        public float defaultAttackAnimLength;   // �⺻ ���� �ִϸ��̼� ����

        // AttackEvent
        AttackEvent attackEvent;                    // ���� �� �̺�Ʈ
        CriticalAttackEvent criticalAttackEvent;    // ġ��Ÿ ���� �� �̺�Ʈ

        // Component
        BattleStatus character;     // ���� ĳ����
        BattleStatus target;        // ������ ��� ĳ����

        // ���� �ൿ�� ������ ĳ���͸� �־��ݴϴ�.
        public Attack(BattleStatus character)
        {
            this.character = character; 
            attackEvent = new AttackEvent();
            criticalAttackEvent = new CriticalAttackEvent();
        }

        // ���� �̺�Ʈ�� �־��ݴϴ�.
        public void AddAttackEvent(UnityAction<BattleStatus, BattleStatus> action)
        {
            attackEvent.AddListener(action);
        }

        // ġ��Ÿ ���� �̺�Ʈ�� �־��ݴϴ�.
        public void AddCriticalAttackEvent(UnityAction<BattleStatus, BattleStatus> action)
        {
            criticalAttackEvent.AddListener(action);
        }

        // ����� �����մϴ�
        public void SetTarget(BattleStatus target)
        {
            this.target = target;
        }

        // ����� �����մϴ�.
        public void AttackTarget()
        {
            canAttack = false;
        }

        // ��󿡰� �������� �����ϴ�.
        public void TargetTakeDamage()
        {
            // ����� �׾��ų� ���ٸ� ����
            if (target.isDead) return;
            if (target == null)
            {
                Debug.Log($"Ÿ���� ������ AttackAnimEvent�� ȣ��Ǿ����ϴ�.");
                return;
            }

            // �� ������ �����մϴ�.
            float defenceAverage = (target.status.DefencePoint / 300);

            // �ּ� ���ط��� ���ط��� 10%
            defenceAverage = 1 - Mathf.Clamp(defenceAverage, 0, 0.9f);

            // �����մϴ�.
            if (AttackChangeCalc(character, target))
            // ������ �����ߴٸ�
            {
                // ���� �̺�Ʈ�� ȣ���մϴ�.
                attackEvent.Invoke(character, target);
                // ��� �ǰ� �̺�Ʈ�� ȣ���մϴ�.
                target.takeDamageEvent.Invoke(target, character);
                if (AttackCriticalCalc(character, target))
                // ġ��Ÿ�� �߻����ٸ�
                {
                    // ġ��Ÿ ���� �̺�Ʈ�� ȣ���մϴ�.
                    criticalAttackEvent.Invoke(character, target);
                    // ġ��Ÿ �������� ����ϰ� ��󿡰� ���ظ� �����ϴ�.
                    // ���� �ؽ�Ʈ�� ġ��Ÿ Ÿ������ �����մϴ�.
                    int criticalDamage = (int)(character.status.AttackDamage * (1 + character.status.CriticalDamage));
                    target.TakeDamage(DamageCalc(criticalDamage, defenceAverage), DamagedType.Ciritical);
                }
                else
                // ġ��Ÿ�� �߻����� �ʾҴٸ�
                {
                    // ��󿡰� ���ظ� �����ϴ�.
                    // ���� �ؽ�Ʈ�� �Ϲ� ������ Ÿ������ �����մϴ�.
                    target.TakeDamage(DamageCalc(character.status.AttackDamage, defenceAverage), DamagedType.Normal);
                }
            }
            else
            // ������ �������� �ʾҴٸ�
            {
                // ���� �ؽ�Ʈ�� MISS Ÿ������ �����մϴ�.
                target.TakeDamage(character.status.AttackDamage, DamagedType.MISS);
            }
        }

        // ������ ������ ����մϴ�.
        private int DamageCalc(int damage, float defenceAverage)
        {
            //Debug.Log($"���ݷ� : {damage}\n" +
            //    $"����� : {defenceAverage * 100}%" +
            //    $"���� ������ ��ġ : {(int)(damage * defenceAverage)}");

            Debug.Log((int)(damage * defenceAverage));
            return (int)(damage * defenceAverage);
        }

        // ���� ������ ����մϴ�.
        private bool AttackChangeCalc(BattleStatus character, BattleStatus target)
        {
            // ���� ���߷��� ���߷� * (1 - ��� ȸ����)
            float chance = character.status.AttackChance * (1 - target.status.EvasionPoint);

            float random = Random.Range(0, 1f);

            //Debug.Log($"{character.name}�� �����Ͽ� {target.name}�� Ÿ���߽��ϴ�.\n" +
            //    $"{character.name}�� ���߷� : {character.status.attackChance * 100}%\n" +
            //    $"{target.name}�� ȸ���� : {target.status.evasionPoint * 100}%\n" +
            //    $"������ ������ Ȯ���� {chance * 100}% �Դϴ�.\n" +
            //    $"�ֻ����� {random * 100}�� ���Խ��ϴ�.");

            if (chance > random)
            {
                // ���� ����
                return true;
            }

            // ���� ����
            return false;
        }

        // ���� ġ��Ÿ�� ����մϴ�.
        private bool AttackCriticalCalc(BattleStatus character, BattleStatus target)
        {
            // ���� ġ��Ÿ Ȯ���� ġ��Ÿ Ȯ�� * (1 - ��� ġ��Ÿ ȸ����)
            float criticalChance = character.status.CriticalChance * (1 - target.status.EvasionCritical);

            float random = Random.Range(0, 1f);

            //        Debug.Log($"{character.name}�� �����Ͽ� {target.name}�� Ÿ���߽��ϴ�.\n" +
            //$"{character.name}�� ġ��Ÿ ���߷� : {character.status.attackChance * 100}%\n" +
            //$"{target.name}�� ġ��Ÿ ȸ���� : {target.status.evasionPoint * 100}%\n" +
            //$"������ ġ��Ÿ�� �߻��� Ȯ���� {criticalChance * 100}% �Դϴ�.\n" +
            //$"ġ��Ÿ �������� {(int)(character.status.attackDamage * (1 + character.status.criticalDamage))} �Դϴ�.\n" +
            //$"�ֻ����� {random * 100}�� ���Խ��ϴ�.");

            if (criticalChance > random)
            {
                // ġ��Ÿ ���� ����
                return true;
            }

            // ġ��Ÿ ����
            return false;
        }

        // ���� �����̸�ŭ ����մϴ�.
        public IEnumerator WaitAttackDelay()
        {
            isAttack = true;
            yield return new WaitForSeconds(attackDelay);
            canAttack = true;
            isAttack = false;
        }
    }
}