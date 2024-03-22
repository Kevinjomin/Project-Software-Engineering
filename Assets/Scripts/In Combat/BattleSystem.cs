using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum BattleState
{
    Start, PlayerTurn, EnemyTurn, Victory, Lost
}

public class BattleSystem : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerManager playerManager;
    private PassiveManager passiveManager;

    public BattleState battleState;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerLocation;
    public Transform enemyLocation;

    CombatHandler combatHandler;
    public UnitParameters playerUnit;
    public UnitParameters enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public GameObject questionPanel;
    public TMP_Text timeLimitDisplay;
    public QuestionManager questionManager;

    public int turn;
    public int pointThisTurn;
    public int bonusFinalDamage;
    public float bonusDamageMultiplier;

    public float timeLimit;
    public float timeLeft;

    private void Awake()
    {
        if (combatHandler == null)
        {
            combatHandler = FindObjectOfType<CombatHandler>();
            if (combatHandler == null)
            {
                Debug.Log("Combat Handler not found!");
            }
        }

        gameManager = FindObjectOfType<GameManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        passiveManager = FindObjectOfType<PassiveManager>();
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

        //import stat from combat manager to player unit
        playerUnit.unitName = combatHandler.playerName;
        playerUnit.damage = 0;
        playerUnit.maxHP = combatHandler.playerMaxHP;
        playerUnit.currentHP = combatHandler.playerCurrentHP;

        GameObject enemyGO = Instantiate(enemyPrefab, enemyLocation);
        enemyUnit = enemyGO.GetComponent<UnitParameters>();
        ImportDataToEnemyUnit();

        turn = 0;
        timeLimit = 10f;
        timeLimitDisplay.text = "";

        ExecutePassive(IPassive.PassiveType.BattleStart);

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
        bonusFinalDamage = 0;
        bonusDamageMultiplier = 1f;
        ExecutePassive(IPassive.PassiveType.PlayerTurnStart);

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
        int totalPlayerDamage = (int)(pointThisTurn * combatHandler.playerDamageMultiplier * bonusDamageMultiplier + bonusFinalDamage);
        bool isDead = enemyUnit.takeDamage(totalPlayerDamage);

        ExecutePassive(IPassive.PassiveType.PlayerTurnEnd);

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
        ExecutePassive(IPassive.PassiveType.EnemyTurnStart);
        timeLimitDisplay.text = "Enemy is attacking";

        bool isDead = playerUnit.takeDamage(enemyUnit.damage);

        ExecutePassive(IPassive.PassiveType.EnemyTurnEnd);

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
            playerManager.currentHP = playerUnit.currentHP;
            playerManager.ObtainCoin(combatHandler.enemyCoin);
            ReturnToOverworld();
        }
        else if(battleState == BattleState.Lost)
        {
            Debug.Log("Defeat");
            gameManager.EndRun();
        }
    }

    void ExecutePassive(IPassive.PassiveType type)
    {
        passiveManager.ExecutePassiveByType(type);
    }

    private void ReturnToOverworld()
    {
        FindObjectOfType<Camera>().GetComponent<AudioListener>().enabled = false;
        gameManager.UnloadScene("Battle Scene");
        gameManager.EnableGameObjectsInScene("Overworld Scene");
    }

    private void ImportDataToEnemyUnit()
    {
        enemyUnit.unitName = combatHandler.enemyName;
        enemyUnit.maxHP = combatHandler.enemyMaxHP;
        enemyUnit.currentHP = combatHandler.enemyCurrentHP;
        enemyUnit.damage = combatHandler.enemyDamage;

        enemyUnit.unitSprite.sprite = combatHandler.enemySprite;
        enemyUnit.unitSprite.color = combatHandler.enemySpriteColor;
        enemyUnit.spritePositionX = combatHandler.enemySpritePositionX;
        enemyUnit.spritePositionY = combatHandler.enemySpritePositionY;
        enemyUnit.RepositionSprite();
    }
}