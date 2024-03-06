using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    private static CombatHandler instance;

    private GameManager gameManager;

    public string playerName;
    public int playerMaxHP;
    public int playerCurrentHP;
    public float playerDamageMultiplier;

    public string enemyName;
    public int enemyMaxHP;
    public int enemyCurrentHP;
    public int enemyDamage;

    // handle enemy sprite between scenes
    private SpriteRenderer enemySpriteRenderer;
    public Sprite enemySprite;
    public Color enemySpriteColor;
    public float enemySpritePositionX;
    public float enemySpritePositionY;

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
    }

    public void ReadEnemyData(string name, int maxHP, int currentHP, int damage, SpriteRenderer spriteRenderer)
    {
        enemyName = name;
        enemyMaxHP = maxHP;
        enemyCurrentHP = currentHP;
        enemyDamage = damage;
        enemySpriteRenderer = spriteRenderer;
        ReadSpriteRendererData(spriteRenderer);
    }

    private void ReadSpriteRendererData(SpriteRenderer spriteRenderer)
    {
        GetTransformPosition(spriteRenderer.transform);
        enemySprite = spriteRenderer.sprite;
        enemySpriteColor = spriteRenderer.color;
    }

    private void GetTransformPosition(Transform transform)
    {
        enemySpritePositionX = transform.localPosition.x;
        enemySpritePositionY = transform.localPosition.y;
    }

    public void TriggerCombat()
    {
        gameManager.LoadScene("Battle Scene");
    }
}
