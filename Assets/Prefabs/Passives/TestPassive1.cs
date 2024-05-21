using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPassive1 : MonoBehaviour, IPassive
{
    private BattleSystem battleSystem;

    private IPassive.PassiveType type = IPassive.PassiveType.PlayerTurnStart;

    private string description = "Increase final damage by 20";

    public void Execute(IPassive.PassiveType passiveType)
    {
        if(passiveType == type && CheckCondition() == true)
        {
            if(battleSystem == null)
            {
                battleSystem = FindObjectOfType<BattleSystem>();
            }
            Debug.Log(description);
            battleSystem.bonusFinalDamage += 20;
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
