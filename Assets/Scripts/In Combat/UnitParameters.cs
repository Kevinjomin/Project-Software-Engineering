using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitParameters : MonoBehaviour
{
    [Header("Floating Text Displays")]
    public GameObject floatingDamageDisplay;
    public GameObject floatingHealDisplay;
    private Transform floatingTextPosition;

    [Header("Unit Sprite Data")]
    public SpriteRenderer unitSprite;
    public Vector2 spritePosition;
    public Vector2 spriteScale;
    public Animator animator;

    [Header("Unit Stats")]
    public string unitName;
    public int damage;
    public int maxHP;
    public int currentHP;

    private void Awake()
    {
        unitSprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        floatingTextPosition = transform.GetChild(1); // this is index 1

        // Ensure current HP does not exceed max HP
        currentHP = Mathf.Min(currentHP, maxHP);
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

        currentHP = Mathf.Min(currentHP + healingAmount, maxHP);
    }

    public void RepositionSprite()
    {
        unitSprite.transform.localPosition = new Vector3(spritePosition.x, spritePosition.y, unitSprite.transform.localPosition.z);
        unitSprite.transform.localScale = new Vector3(spriteScale.x, spriteScale.y, unitSprite.transform.localScale.z);
    }
}
