using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPassive2 : MonoBehaviour, IPassive
{
    private BattleSystem battleSystem;

    private IPassive.PassiveType type = IPassive.PassiveType.PlayerTurnStart;

    private string description = "Increase damage multiplier by 10%";

    public void Execute(IPassive.PassiveType passiveType)
    {
        if (passiveType == type && CheckCondition() == true)
        {
            if (battleSystem == null)
            {
                battleSystem = FindObjectOfType<BattleSystem>();
            }
            Debug.Log(description);
            battleSystem.bonusDamageMultiplier += 0.1f;
        }
    }

    public bool CheckCondition()
    {
        return true;
    }

    public void DebugData()
    {
        Debug.Log(type);
        Debug.Log(description);
    }

    public string ShowDescription()
    {
        return description;
    }
}
