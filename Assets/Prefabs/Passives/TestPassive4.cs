using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPassive4 : MonoBehaviour, IPassive
{
    private BattleSystem battleSystem;
    private IPassive.PassiveType type = IPassive.PassiveType.PlayerTurnStart;

    private string description = "Increase final damage by 5% of current HP";

    public void Execute(IPassive.PassiveType passiveType)
    {
        if (passiveType == type && CheckCondition() == true)
        {
            if (battleSystem == null)
            {
                battleSystem = FindObjectOfType<BattleSystem>();
            }
            Debug.Log(description);
            battleSystem.bonusFinalDamage += Mathf.RoundToInt(0.05f * battleSystem.playerUnit.currentHP);
        }
    }

    public bool CheckCondition()
    {
        return true;
    }

    public void DebugData()
    {
        Debug.Log(description);
    }

    public string ShowDescription()
    {
        return description;
    }
}
