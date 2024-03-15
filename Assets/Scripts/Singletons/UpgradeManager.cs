using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager instance;

    private PlayerManager playerManager;

    public int healthLevel;
    public int baseHealth;
    public int healthUpgradeCost;

    public int damageLevel;
    public float damageMultiplier;
    public int damageUpgradeCost;

    public int coinLevel;
    public float coinMultiplier;
    public int coinUpgradeCost;


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

        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Start()
    {
        healthUpgradeCost = UpdateCost(healthLevel);
        damageUpgradeCost = UpdateCost(damageLevel);
        coinUpgradeCost = UpdateCost(damageLevel);

        baseHealth = UpdateBaseHealth(healthLevel);
        damageMultiplier = UpdateMultiplier(damageLevel);
        coinMultiplier = UpdateMultiplier(coinLevel);
    }

    private int UpdateCost(int currentLevel)
    {
        switch (currentLevel)
        {
            case 1:
                return 100;
            case 2:
                return 200;
            case 3:
                return 500;
            case 4:
                return 1000;
            case 5:
                return 2000;
            default:
                return (currentLevel - 1) * 1000;
        }
    }

    private float UpdateMultiplier(int currentLevel)
    {
        return 1f + (currentLevel - 1) * 0.1f;
    }

    private int UpdateBaseHealth(int currentLevel)
    {
        return 100 + (currentLevel - 1) * 50;
    }

    public void UpgradeHealth()
    {
        if(playerManager.totalCoin < healthUpgradeCost)
        {
            Debug.Log("Not enough coins");
            return;
        }
        playerManager.totalCoin -= healthUpgradeCost;
        healthLevel++;
        baseHealth = UpdateBaseHealth(healthLevel);
        healthUpgradeCost = UpdateCost(healthLevel);
    }

    public void UpgradeDamage()
    {
        if (playerManager.totalCoin < damageUpgradeCost)
        {
            Debug.Log("Not enough coins");
            return;
        }
        playerManager.totalCoin -= damageUpgradeCost;
        damageLevel++;
        damageMultiplier = UpdateMultiplier(damageLevel);
        damageUpgradeCost = UpdateCost(damageLevel);
    }

    public void UpgradeCoin()
    {
        if (playerManager.totalCoin < coinUpgradeCost)
        {
            Debug.Log("Not enough coins");
            return;
        }
        playerManager.totalCoin -= coinUpgradeCost;
        coinLevel++;
        coinMultiplier = UpdateMultiplier(coinLevel);
        coinUpgradeCost = UpdateCost(coinLevel);
    }
}