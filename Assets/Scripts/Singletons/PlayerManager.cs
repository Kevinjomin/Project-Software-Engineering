using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    private GameManager gameManager;
    private UpgradeManager upgradeManager;
    private UI_OverworldHUD UI_Overworld;

    public string playerName;
    public int maxHP;
    public int currentHP;
    public float damageMultiplier;
    public float coinMultiplier;

    public int totalCoin;
    public int coinObtainedThisRun;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameManager = FindObjectOfType<GameManager>();
        upgradeManager = FindObjectOfType<UpgradeManager>();
    }

    public void ResetRun()
    {
        AddToTotalCoin();

        maxHP = upgradeManager.baseHealth;
        damageMultiplier = upgradeManager.damageMultiplier;
        coinMultiplier = upgradeManager.coinMultiplier * CalculateDifficultyMultiplier();

        coinObtainedThisRun = 0;
        currentHP = maxHP;
    }

    private float CalculateDifficultyMultiplier()
    {
        switch (gameManager.tempDifficulty)
        {
            case 3: //hard
                return 4f;
            case 2: //medium
                return 2f;
            default: //easy
                return 1f;
        }
    }

    public bool isDead()
    {
        if(currentHP <= 0)
        {
            return true;
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (isDead())
        {
            Debug.LogError("Player died, return to main menu");
            gameManager.EndRun();
        }
    }

    public void ObtainCoin(int coin)
    {
        coinObtainedThisRun += CalculateCoin(coin);
        //UpdateCoinCounter(coinObtainedThisRun);
    }

    private void UpdateCoinCounter(int coin)
    {
        if(UI_Overworld == null)
        {
            UI_Overworld = FindObjectOfType<UI_OverworldHUD>();
        }
        UI_Overworld.UpdateCoinCounter(coin);
    }

    public int CalculateCoin(int coin)
    {
        return (Mathf.RoundToInt(coin * coinMultiplier));
    }

    public void AddToTotalCoin() //this is when run ends
    {
        totalCoin += coinObtainedThisRun;
    }
}
