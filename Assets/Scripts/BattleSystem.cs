using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    PlayerManager playerManager;
    UnitParameters playerUnit;
    UnitParameters enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public GameObject questionPanel;
    public TMP_Text timeLimitDisplay;
    public QuestionManager questionManager;

    public int turn;
    public int pointThisTurn;
    public float timeLimit;
    public float timeLeft;

    private void Awake()
    {
        if (playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManager>();
            if (playerManager == null)
            {
                Debug.Log("Player Manager not found!");
            }
        }
    }

    void Start()
    {
        battleState = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO =  Instantiate(playerPrefab, playerLocation);
        playerUnit = playerGO.GetComponent<UnitParameters>();

        //import stat from player manager to player unit
        playerUnit.unitName = playerManager.playerName;
        playerUnit.damage = 0;
        playerUnit.maxHP = playerManager.playerMaxHP;
        playerUnit.currentHP = playerManager.playerCurrentHP;

        GameObject enemyGO = Instantiate(enemyPrefab, enemyLocation);
        enemyUnit = enemyGO.GetComponent<UnitParameters>();

        turn = 0;
        timeLimit = 10f;
        timeLimitDisplay.text = "";

        //setup HUD
        playerHUD.nameText.text = playerUnit.unitName;
        playerHUD.updateHP(playerUnit.currentHP, playerUnit.maxHP);

        enemyHUD.nameText.text = enemyUnit.unitName;
        enemyHUD.updateHP(enemyUnit.currentHP, enemyUnit.maxHP);

        yield return new WaitForSeconds(1f);

        battleState = BattleState.PlayerTurn;
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        turn++;
        pointThisTurn = 0;
        questionPanel.SetActive(true);
        questionManager.StartQuestionManager();

        timeLeft = timeLimit;
        while (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            timeLimitDisplay.text = "Time left : " + timeLeft.ToString("F0") + " s";
            yield return null; //wait until next frame
        }

        questionPanel.SetActive(false);
        timeLimitDisplay.text = "";
        yield return new WaitForSeconds(1f);
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        int totalPlayerDamage = (int)(pointThisTurn * playerManager.playerDamageMultiplier);
        bool isDead = enemyUnit.takeDamage(totalPlayerDamage);

        enemyHUD.updateHP(enemyUnit.currentHP, enemyUnit.maxHP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            battleState = BattleState.Victory;
            EndBattle();
        }
        else
        {
            battleState = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        timeLimitDisplay.text = "Enemy is attacking";

        bool isDead = playerUnit.takeDamage(enemyUnit.damage);
        playerHUD.updateHP(playerUnit.currentHP, playerUnit.maxHP);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            battleState = BattleState.Lost;
            EndBattle();
        }
        else
        {
            battleState = BattleState.PlayerTurn;
            StartCoroutine(PlayerTurn());
        }
    }


    void EndBattle()
    {
        if (battleState == BattleState.Victory)
        {
            Debug.Log("Victory");
            playerManager.playerCurrentHP = playerUnit.currentHP;
        }
        else if(battleState == BattleState.Lost)
        {
            Debug.Log("Defeat");
        }
    }

}
