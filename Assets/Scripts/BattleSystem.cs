using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState
{
    Start, PlayerTurn, EnemyTurn, Victory, Lost
}

public class BattleSystem : MonoBehaviour
{
    public BattleState battleState;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerLocation;
    public Transform enemyLocation;

    PlayerManager playerUnit;
    UnitParameters enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    private void Awake()
    {
        playerUnit = GameObject.Find("Player Manager").GetComponent<PlayerManager>();
    }

    void Start()
    {
        battleState = BattleState.Start;
        SetupBattle();
    }

    void SetupBattle()
    {
        GameObject playerGO =  Instantiate(playerPrefab, playerLocation);


        GameObject enemyGO = Instantiate(enemyPrefab, enemyLocation);
        enemyUnit = enemyGO.GetComponent<UnitParameters>();

        //setup HUD
        playerHUD.nameText.text = playerUnit.playerName;
        playerHUD.updateHP(playerUnit.playerCurrentHP, playerUnit.playerMaxHP);

        enemyHUD.nameText.text = enemyUnit.unitName;
        enemyHUD.updateHP(enemyUnit.currentHP, enemyUnit.maxHP);
        
    }

}
