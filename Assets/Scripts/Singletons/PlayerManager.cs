using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    private GameManager gameManager;

    public string playerName;
    public int maxHP;
    public int currentHP;
    public float damageMultiplier;

    public int goldObtained;

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
    }

    public void ResetRun()
    {
        goldObtained = 0;
        currentHP = maxHP;
        //check upgrades again after the system is implemented
        Debug.LogWarning("Make sure to check upgrades stat again at playerManager");
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
        }
    }

    public void obtainGold(int gold)
    {
        goldObtained += gold;
    }
}
