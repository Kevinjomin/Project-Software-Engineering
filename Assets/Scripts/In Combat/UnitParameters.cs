using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitParameters : MonoBehaviour
{
    public GameObject floatingDamageDisplay;
    private Transform floatingDamagePosition;

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

    public void RepositionSprite()
    {
        unitSprite.transform.localPosition = new Vector3(spritePositionX, spritePositionY, transform.localPosition.z);
    }
}
