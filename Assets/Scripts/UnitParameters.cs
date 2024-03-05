using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitParameters : MonoBehaviour
{
    public GameObject floatingDamageDisplay;
    private Transform floatingDamagePosition;

    public string unitName;
    public int damage;
    public int maxHP;
    public int currentHP;

    private void Awake()
    {
        floatingDamagePosition = transform.GetChild(1); // this is index 1
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public bool takeDamage(int damage)
    {
        GameObject damageDisplay = Instantiate(floatingDamageDisplay, floatingDamagePosition);
        damageDisplay.transform.GetChild(0).GetComponent<TMP_Text>().text = damage.ToString();

        currentHP -= damage;

        if(currentHP <= 0)
        {
            return true; //die
        }
        else
        {
            return false;
        }
    }
}
