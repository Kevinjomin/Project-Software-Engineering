using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    public string playerName;
    public int playerMaxHP;
    public int playerCurrentHP;
    public float playerDamageMultiplier;

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

    public bool takeDamage(int damage)
    {
        playerCurrentHP -= damage;

        if (playerCurrentHP <= 0)
        {
            return true; //die
        }
        else
        {
            return false;
        }
    }
}
