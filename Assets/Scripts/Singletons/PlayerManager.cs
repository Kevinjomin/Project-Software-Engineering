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

    public void obtainGold(int gold)
    {
        goldObtained += gold;
    }
}
