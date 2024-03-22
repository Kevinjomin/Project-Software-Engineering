using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitParameters : MonoBehaviour
{
    public GameObject floatingDamageDisplay;
    public GameObject floatingHealDisplay;
    private Transform floatingTextPosition;

    public SpriteRenderer unitSprite;
    public float spritePositionX;
    public float spritePositionY;

    public string unitName;
    public int damage;
    public int maxHP;
    public int currentHP;

    private void Awake()
    {
        unitSprite = GetComponentInChildren<SpriteRenderer>();
        floatingTextPosition = transform.GetChild(1); // this is index 1
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public bool takeDamage(int damage)
    {
        GameObject damageDisplay = Instantiate(floatingDamageDisplay, floatingTextPosition);
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

    public void heal(int healingAmount)
    {
        GameObject healDisplay = Instantiate(floatingHealDisplay, floatingTextPosition);
        healDisplay.transform.GetChild(0).GetComponent<TMP_Text>().text = healingAmount.ToString();

        if(currentHP + healingAmount > maxHP)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += healingAmount;
        }
    }

    public void RepositionSprite()
    {
        unitSprite.transform.localPosition = new Vector3(spritePositionX, spritePositionY, transform.localPosition.z);
    }
}
