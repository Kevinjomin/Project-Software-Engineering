using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnemy : MonoBehaviour
{
    public string enemyName;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int coinDrop;

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        damage += Mathf.RoundToInt(damage * levelManager.multiplierDamage);
        maxHP += Mathf.RoundToInt(maxHP * levelManager.multiplierHP);
        currentHP = maxHP;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
