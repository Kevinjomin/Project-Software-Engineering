using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPassive3 : MonoBehaviour, IPassive
{
    private BattleSystem battleSystem;

    private IPassive.PassiveType type = IPassive.PassiveType.BattleStart;

    private string description = "On battle start, heal by 20% of max HP";

    public void Execute(IPassive.PassiveType passiveType)
    {
        if (passiveType == type && CheckCondition() == true)
        {
            if (battleSystem == null)
            {
                battleSystem = FindObjectOfType<BattleSystem>();
            }
            Debug.Log(description);
            battleSystem.playerUnit.heal((int)(battleSystem.playerUnit.maxHP * 0.2f));
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
