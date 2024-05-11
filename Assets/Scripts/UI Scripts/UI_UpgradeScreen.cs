using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_UpgradeScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text healthLevelText;
    [SerializeField] private TMP_Text baseHealthText;
    [SerializeField] private Button healthUpgradeButton;

    [SerializeField] private TMP_Text damageLevelText;
    [SerializeField] private TMP_Text damageMultiplierText;
    [SerializeField] private Button damageUpgradeButton;

    [SerializeField] private TMP_Text coinLevelText;
    [SerializeField] private TMP_Text coinMultiplierText;
    [SerializeField] private Button coinUpgradeButton;

    [SerializeField] private TMP_Text coinCounter;

    private UpgradeManager upgradeManager;
    private PlayerManager playerManager;

    private void Awake()
    {
        upgradeManager = FindObjectOfType<UpgradeManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        if(upgradeManager == null)
        {
            Debug.LogError("Upgrade UI cannot find upgrade manager");
        }
    }

    private void Start()
    {
        healthUpgradeButton.onClick.AddListener(UpgradeHealth);
        damageUpgradeButton.onClick.AddListener(UpgradeDamage);
        coinUpgradeButton.onClick.AddListener(UpgradeCoin);
    }

    public void UpdateCoinCounter(int coin)
    {
        coinCounter.text = coin.ToString();
    }

    private void UpdateValuesDisplayed()
    {
        UpdateCoinCounter(playerManager.totalCoin);

        healthLevelText.text = "Level " + upgradeManager.healthLevel;
        baseHealthText.text = "Max health : " + upgradeManager.baseHealth.ToString();
        healthUpgradeButton.GetComponentInChildren<TMP_Text>().text = upgradeManager.healthUpgradeCost + " coins";

        damageLevelText.text = "Level " + upgradeManager.damageLevel;
        damageMultiplierText.text = "Damage multiplier: " + upgradeManager.damageMultiplier.ToString("F1") + "x";
        damageUpgradeButton.GetComponentInChildren<TMP_Text>().text = upgradeManager.damageUpgradeCost + " coins";

        coinLevelText.text = "Level " + upgradeManager.coinLevel;
        coinMultiplierText.text = "Coin multiplier: " + upgradeManager.coinMultiplier.ToString("F1") + "x";
        coinUpgradeButton.GetComponentInChildren<TMP_Text>().text = upgradeManager.coinUpgradeCost + " coins";
    }

    private void UpgradeHealth()
    {
        upgradeManager.UpgradeHealth();
        UpdateValuesDisplayed();
    }

    private void UpgradeDamage()
    {
        upgradeManager.UpgradeDamage();
        UpdateValuesDisplayed();
    }

    private void UpgradeCoin()
    {
        upgradeManager.UpgradeCoin();
        UpdateValuesDisplayed();
    }

    private void OnEnable()
    {
        UpdateValuesDisplayed();
    }
}
