using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    public CombatManager combatManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        combatManager = FindObjectOfType<CombatManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OverworldEnemy enemy = collision.gameObject.GetComponent<OverworldEnemy>();
            if(enemy != null)
            {
                combatManager.enemyName = enemy.enemyName;
                combatManager.enemyMaxHP = enemy.maxHP;
                combatManager.enemyCurrentHP = enemy.currentHP;
                // trigger combat now
            }
        }
    }
}
