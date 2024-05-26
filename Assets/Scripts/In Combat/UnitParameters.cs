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

    public GameObject hitEffect;

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

        HitEffectAnimation();
        StartCoroutine(FlashRed());

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

    void HitEffectAnimation()
    {
        float positionY  = (transform.position.y + floatingTextPosition.position.y) / 2;
        Vector2 position = new Vector2(transform.position.x, positionY);
        Instantiate(hitEffect, position, Quaternion.identity);
    }

    IEnumerator FlashRed() //turn the sprite red momentarily
    {
        Color originalColor = unitSprite.color;
        unitSprite.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        unitSprite.color = originalColor;
    }

    public void RepositionSprite()
    {
        unitSprite.transform.localPosition = new Vector3(spritePosition.x, spritePosition.y, unitSprite.transform.localPosition.z);
        unitSprite.transform.localScale = new Vector3(spriteScale.x, spriteScale.y, unitSprite.transform.localScale.z);
    }

    public void PlayEnemyDeathAnimation()
    {
        StartCoroutine(TransformSprite());
    }

    private IEnumerator TransformSprite() //this is for the enemy death animation
    {
        float duration = 0.2f;
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;
        Color originalColor = unitSprite.color;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * 1.2f, elapsedTime / duration);

            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(1f, 0f, elapsedTime / duration); //change opacity (alpha)
            unitSprite.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the final transform is set
        transform.localScale = originalScale * 1.2f; 
        Color finalColor = originalColor;
        finalColor.a = 0f;
        unitSprite.color = finalColor;
    }
}
