using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    private GameManager gameManager;
    private UpgradeManager upgradeManager;

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
        coinMultiplier = upgradeManager.coinMultiplier;

        coinObtainedThisRun = 0;
        currentHP = maxHP;
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
        coinObtainedThisRun += (Mathf.RoundToInt(coin * coinMultiplier));
    }

    public void AddToTotalCoin()
    {
        totalCoin += coinObtainedThisRun;
    }
}
