using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    private static CombatHandler instance;

    private GameManager gameManager;
    private PlayerManager playerManager;

    [Header("Player Info")]
    public string playerName;
    public int playerMaxHP;
    public int playerCurrentHP;
    public float playerDamageMultiplier;

    [Header("Enemy Info")]
    public string enemyName;
    public int enemyMaxHP;
    public int enemyCurrentHP;
    public int enemyDamage;
    public int enemyCoin;

    [Header("Enemy Sprite Info")]
    public Sprite enemySprite;
    public Color enemySpriteColor;
    public Vector2 enemySpritePosition;
    public Vector2 enemySpriteScale;
    public RuntimeAnimatorController enemyAnimatorController;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameManager = FindObjectOfType<GameManager>();
        playerManager = FindObjectOfType<PlayerManager>();
    }
    private void ReadPlayerData(string name, int maxHP, int currentHP, float damageMultiplier)
    {
        playerName = name;
        playerMaxHP = maxHP;
        playerCurrentHP = currentHP;
        playerDamageMultiplier = damageMultiplier;
    }

    public void ReadEnemyData(string name, int maxHP, int currentHP, int damage, int coinDropped, SpriteRenderer spriteRenderer, Animator animator)
    {
        enemyCoin = coinDropped;
        enemyName = name;
        enemyMaxHP = maxHP;
        enemyCurrentHP = currentHP;
        enemyDamage = damage;
        ReadSpriteRendererData(spriteRenderer);
        enemyAnimatorController = animator.runtimeAnimatorController;
    }

    private void ReadSpriteRendererData(SpriteRenderer spriteRenderer)
    {
        enemySprite = spriteRenderer.sprite;
        enemySpriteColor = spriteRenderer.color;
        enemySpritePosition = spriteRenderer.transform.localPosition;
        enemySpriteScale = spriteRenderer.transform.localScale;
    }

    public void TriggerCombat()
    {
        ReadPlayerData(playerManager.playerName, playerManager.maxHP, playerManager.currentHP, playerManager.damageMultiplier);
        gameManager.LoadSceneAdditive("Battle Scene");
        gameManager.DisableGameObjectsInScene("Overworld Scene");
    }
}
