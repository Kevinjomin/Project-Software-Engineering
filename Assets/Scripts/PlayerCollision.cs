using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Rigidbody2D rb;
    public CombatHandler combatHandler;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        combatHandler = FindObjectOfType<CombatHandler>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OverworldEnemy enemy = collision.gameObject.GetComponent<OverworldEnemy>();
            if(enemy != null)
            {
                combatHandler.ReadEnemyData(enemy.enemyName, enemy.maxHP, enemy.currentHP, enemy.damage, enemy.goldDrop, enemy.GetComponentInChildren<SpriteRenderer>());
                enemy.RemoveSelf();
                combatHandler.TriggerCombat();
            }
        }
    }
}
