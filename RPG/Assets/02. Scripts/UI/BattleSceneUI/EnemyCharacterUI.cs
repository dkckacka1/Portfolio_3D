using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.UI;
using RPG.Character.Status;

/*
 * �� ĳ���� UI Ŭ����
 */

public class EnemyCharacterUI : CharacterUI
{
    public GameObject battleStatusUIPrefab;                     // ĳ������ UI ������
    public GameObject battleStatusUI;                           // ĳ���� UI ������Ʈ
    public Vector3 battleStatusOffset = new Vector3(0, 1.5f, 0);// ĳ���� UI�� ������ ��ġ ������

    // �ʱ�ȭ �մϴ�
    public override void SetUp()
    {
        base.SetUp();
        SetUpBattleStatusUI();
    }

    public override void Init()
    {
        base.Init();
        this.battleStatusUI.SetActive(true);
    }

    // UI�� ���ݴϴ�.
    public override void ReleaseUI()
    {
        if (battleStatusUI != null)
        {
            battleStatusUI.gameObject.SetActive(false);
        }
    }

    // UI��ġ�� ���� ĳ���� ��ġ�� ����ȭ�մϴ�.
    private void LateUpdate()
    {
        UpdateBattleStatusUI(transform.position + battleStatusOffset);
    }

    // ���� ���� ������ ���� ��ũ�� �����ͷ� �����մϴ�.
    public void UpdateBattleStatusUI(Vector3 position)
    {
        battleStatusUI.transform.transform.position = Camera.main.WorldToScreenPoint(position);
    }

    // ĳ���� UI�� �����մϴ�.
    public void SetUpBattleStatusUI()
    {
        if (battleCanvas == null)
        {
            return;
        }
        battleStatusUI = Instantiate(battleStatusUIPrefab, battleCanvas.transform);
        hpBar = battleStatusUI.GetComponentInChildren<HPBar>();
        debuffUI = battleStatusUI.GetComponentInChildren<DebuffUI>();
    }


}
