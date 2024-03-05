using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text hpText;
    public Slider hpSlider;

    [SerializeField] private Color hpHighColor;
    [SerializeField] private Color hpLowColor;
    [SerializeField] private GameObject hpBarFill;

    public void updateHP(int currentHP, int maxHP)
    {
        Image fillImage = hpBarFill.GetComponent<Image>();
        if(currentHP < 0)
        {
            currentHP = 0;
        }

        float hpPercentage = (float)currentHP / (float)maxHP;
        if(hpPercentage <= 0.3f)
        {
            fillImage.color = hpLowColor;
        }
        else
        {
            fillImage.color = hpHighColor;
        }

        hpText.text = currentHP + " / " + maxHP;
        hpSlider.value = hpPercentage;
    }
}
