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

    public void updateHP(int currentHP, int maxHP)
    {
        hpText.text = currentHP + " / " + maxHP;
        hpSlider.value = (float)currentHP / (float)maxHP;
    }
}
