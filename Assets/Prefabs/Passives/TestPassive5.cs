using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPassive5 : MonoBehaviour, IPassive
{
    private BattleSystem battleSystem;

    private IPassive.PassiveType type = IPassive.PassiveType.PlayerTurnStart;

    private string description = "Has a 10% chance to increase damage multiplier by 100%";

    public void Execute(IPassive.PassiveType passiveType)
    {
        if (passiveType == type && CheckCondition() == true)
        {
            if (battleSystem == null)
            {
                battleSystem = FindObjectOfType<BattleSystem>();
            }
            Debug.Log(description );
            battleSystem.bonusDamageMultiplier += 1f;
        }
    }

    public bool CheckCondition()
    {
        int randomInt = Random.Range(0, 100);
        if (randomInt < 10)
        {
            return true;
        }
        return false;
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
