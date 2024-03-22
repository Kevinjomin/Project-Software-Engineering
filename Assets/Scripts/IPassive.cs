using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPassive
{
    public enum PassiveType
    {
        BattleStart, PlayerTurnStart, PlayerTurnEnd, EnemyTurnStart, EnemyTurnEnd
    }

    void Execute(PassiveType passiveType);
    bool CheckCondition();
    void DebugData();
}
