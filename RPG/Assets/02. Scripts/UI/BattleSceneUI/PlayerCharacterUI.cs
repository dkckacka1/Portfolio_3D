using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.UI;
using RPG.Character.Status;

/*
 * �÷��̾� ĳ���� UI Ŭ����
 */

public class PlayerCharacterUI : CharacterUI
{
    // UI�� �����մϴ�.
    public override void SetUp()
    {
        base.SetUp();
        hpBar = BattleManager.BattleUI.playerHPBarUI;
        debuffUI = BattleManager.BattleUI.playerDebuffUI;
    }
}
