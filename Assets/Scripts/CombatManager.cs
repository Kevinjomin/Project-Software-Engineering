using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private static CombatManager instance;

    public string playerName;
    public int playerMaxHP;
    public int playerCurrentHP;
    public float playerDamageMultiplier;

    public string enemyName;
    public int enemyMaxHP;
    public int enemyCurrentHP;

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
    }
}
