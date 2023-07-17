using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;

/*
 * �������� ������ Ŭ����
 */

namespace RPG.Battle.Core
{
    [CreateAssetMenu(fileName = "NewStage", menuName = "CreateScriptableObject/CreateStage", order = 0)]
    public class StageData : Data
    {
        public Vector3 playerSpawnPosition = new Vector3(8.0f, 0f); // ���� ĳ���� ���� ��ġ
        public int[] enemyDatas;                                    // ���������� �� ID
        public int ConsumEnergy;                                    // ������ �Һ��
    }
}