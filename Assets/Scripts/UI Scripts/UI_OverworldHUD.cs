using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_OverworldHUD : MonoBehaviour
{
    private int selectedButtonIndex = -1;

    public Slider hpSlider;
    public GameObject passiveSelectionPanel;

    public Button passiveSelection1;
    public Button passiveSelection2;
    public Button passiveSelection3;

    public TMP_Text passiveSelection1_Text;
    public TMP_Text passiveSelection2_Text;
    public TMP_Text passiveSelection3_Text;

    private void Start()
    {
        UpdateHPSlider(1, 1);

    }
    public void UpdateHPSlider(int currentHP, int maxHP)
    {
        if (currentHP < 0)
        {
            currentHP = 0;
        }
        float hpPercentage = (float)currentHP / (float)maxHP;
        hpSlider.value = hpPercentage;
    }

    public void EnablePassiveSelectionUI()
    {
        selectedButtonIndex = -1;
        passiveSelectionPanel.SetActive(true);
    }

    public void SetSelectedButtonIndex(int index)
    {
        selectedButtonIndex = index;
        passiveSelectionPanel.SetActive(false);
        FindObjectOfType<PassiveManager>().AddSelectionToList(selectedButtonIndex);
    }
}
