using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEnemy : MonoBehaviour
{
    public string enemyName;
    public int damage;
    public int maxHP;
    public int currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }
}
