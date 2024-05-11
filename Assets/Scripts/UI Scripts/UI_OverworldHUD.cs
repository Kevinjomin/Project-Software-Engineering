using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_OverworldHUD : MonoBehaviour
{
    private PlayerManager playerManager;

    private int selectedButtonIndex = -1;

    public Slider hpSlider;
    public TMP_Text coinCounter;
    public GameObject passiveSelectionPanel;

    public Button passiveSelection1;
    public Button passiveSelection2;
    public Button passiveSelection3;

    public TMP_Text passiveSelection1_Text;
    public TMP_Text passiveSelection2_Text;
    public TMP_Text passiveSelection3_Text;

    private RectTransform passiveSelection1_RectTransform;
    private RectTransform passiveSelection2_RectTransform;
    private RectTransform passiveSelection3_RectTransform;

    private Vector2 passiveSelection1_InitialPosition;
    private Vector2 passiveSelection2_InitialPosition;
    private Vector2 passiveSelection3_InitialPosition;

    private void Start()
    {
        if (playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }

        UpdateHPSlider(playerManager.currentHP, playerManager.maxHP);
        UpdateCoinCounter(playerManager.coinObtainedThisRun);

        passiveSelection1_RectTransform = passiveSelection1.GetComponent<RectTransform>();
        passiveSelection2_RectTransform = passiveSelection2.GetComponent<RectTransform>();
        passiveSelection3_RectTransform = passiveSelection3.GetComponent<RectTransform>();

        passiveSelection1_InitialPosition = passiveSelection1_RectTransform.anchoredPosition;
        passiveSelection2_InitialPosition = passiveSelection2_RectTransform.anchoredPosition;
        passiveSelection3_InitialPosition = passiveSelection3_RectTransform.anchoredPosition;
    }

    private void FixedUpdate() //this is inefficient, find better alternatives if possible
    {
        UpdateCoinCounter(playerManager.coinObtainedThisRun);
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

    public void UpdateCoinCounter(int coin)
    {
        coinCounter.text = coin.ToString();
    }

    public void EnablePassiveSelectionUI(int selectionAmount)
    {
        selectedButtonIndex = -1;
        passiveSelectionPanel.SetActive(true);

        if (selectionAmount == 3 || selectionAmount == 2)
        {
            passiveSelection1.gameObject.SetActive(true);
            passiveSelection2.gameObject.SetActive(true);
            passiveSelection3.gameObject.SetActive(true);

            passiveSelection1_RectTransform.anchoredPosition = passiveSelection1_InitialPosition;
            passiveSelection2_RectTransform.anchoredPosition = passiveSelection2_InitialPosition;
            passiveSelection3_RectTransform.anchoredPosition = passiveSelection3_InitialPosition;
        }
        else if (selectionAmount == 1)
        {
            passiveSelection1.gameObject.SetActive(true);
            passiveSelection2.gameObject.SetActive(true);
            passiveSelection3.gameObject.SetActive(false);

            passiveSelection1_RectTransform.anchoredPosition = passiveSelection1_InitialPosition + new Vector2(100f, 0f);
            passiveSelection2_RectTransform.anchoredPosition = passiveSelection2_InitialPosition + new Vector2(100f, 0f);
        }
        else if (selectionAmount == 0)
        {
            passiveSelection1.gameObject.SetActive(true);
            passiveSelection2.gameObject.SetActive(false);
            passiveSelection3.gameObject.SetActive(false);

            passiveSelection1_RectTransform.anchoredPosition += new Vector2(200f, 0f);
        }
    }

    public void SetSelectedButtonIndex(int index)
    {
        selectedButtonIndex = index;
        passiveSelectionPanel.SetActive(false);
        FindObjectOfType<PassiveManager>().AddSelectionToList(selectedButtonIndex);
    }
}
