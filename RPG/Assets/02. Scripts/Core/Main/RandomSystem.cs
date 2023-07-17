using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Character.Equipment;

/*
 * ���� ������ ������ ����� ���� Ŭ������ ��Ƴ��� static Ŭ���� �Դϴ�.
 */

namespace RPG.Core
{
    public static class RandomSystem
    {
        // ������ �������� �̽��ϴ�.
        public static bool TryGachaRandomData<T>(Dictionary<int, EquipmentData> dic ,EquipmentItemType type, out T data, int lowerTier = 0) where T : EquipmentData
        {
            // ������ ����� �����մϴ�.
            var tier = GetRandomTier(Random.Range(lowerTier, 101));
            var list = dic
                .Where(data => (data.Value.equipmentType == type && data.Value.equipmentTier == tier))
                .ToList();

            // �ش� ����� ������ ������ �ε����� �����ɴϴ�
            int getRandomIndex = Random.Range(0, list.Count);
            if (list.Count == 0)
            {
                Debug.Log($"{tier}����� ���� {type}�� �����ϴ�.");
                data = null;
                return false;
            }

            // �������� �������� T Ŭ������ ��ȯ�մϴ�.
            data = list[getRandomIndex].Value as T;
            return true;
        }

        // �����ۿ� ������ ��æƮ�� �ο��մϴ�.
        public static bool TryGachaIncant(EquipmentItemType type, Dictionary<int, Incant> dic, out Incant incant, int lowerTier = 0)
        {
            // ������ ����� �����մϴ�.
            var tier = GetRandomTier(Random.Range(lowerTier, 101));
            var IncantList = dic
                            .Where(item => item.Value.itemType == type && item.Value.incantTier == tier)
                            .ToList();

            if (IncantList.Count == 0)
            {
                Debug.Log($"{tier} ����� �˸´� ��æƮ�� �����ϴ�.");
                incant = null;
                return false;
            }

            // �ش� ����� ������ ��æƮ �ε����� �����ɴϴ�.
            int randomIndex = Random.Range(0, IncantList.Count);
            incant = IncantList[randomIndex].Value;

            if (incant.itemType != type)
            {
                Debug.LogError($"�߸��� ��æƮ ���� : {incant.incantName}�� {type}�� ��æƮ�� �� �����ϴ�!");
                incant = null;
                return false;
            }

            return true;
        }

        // ��ȭ Ȱ���� ����մϴ�.
        public static float ReinforceCalc(Equipment equipment)
            // ��ȭ Ȯ�� ����
            // 100f = 100%, 0 = 0%
        {
            // ���� ��ȭ ��ġ
            int currentReinforceCount = equipment.reinforceCount;
            // ��ȭ ����Ȯ��
            float reinforcementSuccessProbability = 100f - ((float)currentReinforceCount / Constant.maxReinforceCount * 100);

            return reinforcementSuccessProbability;
        }

        // ������ ����� �������ݴϴ�.
        private static TierType GetRandomTier(int tex)
        {
            // �� ��� Ȯ���� �°� ����� �����մϴ�.
            if (tex <= Constant.getNormalPercent)
            {
                return TierType.Normal;
            }
            else if (tex <= Constant.getNormalPercent + Constant.getRarelPercent)
            {
                return TierType.Rare;
            }
            else if (tex <= Constant.getNormalPercent + Constant.getRarelPercent + Constant.getUniquePercent)
            {
                return TierType.Unique;
            }
            else if (tex <= Constant.getNormalPercent + Constant.getRarelPercent + Constant.getUniquePercent + Constant.getLegendaryPercent)
            {
                return TierType.Legendary;
            }

            return TierType.Normal;
        }
    }

}