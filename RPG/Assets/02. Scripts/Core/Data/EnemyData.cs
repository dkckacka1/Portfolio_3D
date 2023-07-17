using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �� ������ Ŭ����
 */

[CreateAssetMenu(fileName = "NewEnemy", menuName = "CreateScriptableObject/CreateEnemy", order = 6)]
public class EnemyData : Data
{
    public int apperenceNum;            // ���� ���� ID
    public string enemyName;            // �� �̸�

    [Header("Health")]
    public int maxHp = 0;   // �ִ� ü�� ��ġ

    [Header("Attack")]
    public int attackDamage = 0;        // ���ݷ�
    public float attackRange = 0f;      // ���ݹ���
    public float attackSpeed = 0f;      // ���ݼӵ�
    public float criticalChance = 0f;   // ġ��Ÿ ���߷�
    public float criticalDamage = 0f;   // ġ��Ÿ ������
    public float attackChance = 0f;     // ���߷�

    [Header("Defence")]
    public int defencePoint = 0;                // ��
    public float evasionPoint = 0f;             // ȸ����
    public float decreseCriticalDamage = 0f;    // ġ��Ÿ ���� ������
    public float evasionCritical = 0f;          // ȸ����

    [Header("Movement")]
    public float movementSpeed = 0f;    // �̵��ӵ�

    [Header("DropItem")]
    public int dropEnergy;              // ������ �����
    public List<DropTable> dropitems;   // ��� ���̺�

    [Header("Equipment")]
    public int weaponApparenceID;       // ���� ����
    public weaponHandleType handleType; // �Ѽ� �� ��� ����
}
