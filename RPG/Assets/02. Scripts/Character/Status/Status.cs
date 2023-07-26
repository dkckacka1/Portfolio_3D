using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��� ĳ������ �⺻ ���� Ŭ����
 */

namespace RPG.Character.Status 
{
    public class Status : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] int maxHp = 0;                         // �ִ� ü��

        [Header("Attack")]
        [SerializeField] float attackRange = 0f;                // ���� ����
        [SerializeField] int attackDamage = 0;                  // ���ݷ�
        [SerializeField] float attackSpeed = 0f;                // ���� �ӵ�
        [SerializeField] float criticalChance = 0f;             // ġ��Ÿ Ȯ��
        [SerializeField] float criticalDamage = 0f;             // ġ��Ÿ ���ط�
        [SerializeField] float attackChance = 0f;               // ���߷�

        [Header("Defence")]
        [SerializeField] int defencePoint = 0;                  // ��� ��ġ
        [SerializeField] float evasionPoint = 0f;               // ȸ����
        [SerializeField] float decreseCriticalDamage = 0f;      // ġ��Ÿ ������ ������
        [SerializeField] float evasionCritical = 0f;            // ġ��Ÿ ȸ����

        [Header("Movement")]
        [SerializeField] float movementSpeed = 0f;              // �̵� �ӵ�

        // Encapsulation
        public int MaxHp 
        {
            get => maxHp;
            set => maxHp = value; 
        }
        public float AttackRange { get => attackRange; set => attackRange = value; }
        public int AttackDamage { get => attackDamage; set => attackDamage = value; }
        // ���ݼӵ��� �ּ� ���ݼӵ��� �ֽ��ϴ�.
        public float AttackSpeed 
        {
            get
            {
                if (attackSpeed < Constant.minimumAttackSpeed)
                {
                    return Constant.minimumAttackSpeed;
                }
                else
                {
                    return attackSpeed;
                }
            }
            set => attackSpeed = value;
        }

        public float CriticalChance { get => criticalChance; set => criticalChance = value; }
        public float CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
        public float AttackChance { get => attackChance; set => attackChance = value; }
        public int DefencePoint { get => defencePoint; set => defencePoint = value; }
        public float EvasionPoint { get => evasionPoint; set => evasionPoint = value; }
        public float DecreseCriticalDamage { get => decreseCriticalDamage; set => decreseCriticalDamage = value; }
        public float EvasionCritical { get => evasionCritical; set => evasionCritical = value; }
        // �̵��ӵ��� �ּ� �̵��ӵ��� �ֽ��ϴ�.
        public float MovementSpeed 
        { 
            get
            {
                if (movementSpeed < Constant.minimumMovementSpeed)
                {
                    return Constant.minimumMovementSpeed;
                }
                else
                {
                    return movementSpeed;
                }
            }
            set => movementSpeed = value;
        }
    }
}

