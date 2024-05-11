using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PassiveManager : MonoBehaviour
{
    private static PassiveManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private PlayerManager playerManager;

    [SerializeField] private List<GameObject> gameObjectList = new List<GameObject>();

    private List<IPassive> passivePool;
    private List<IPassive> obtainedPassiveList = new List<IPassive>();
    private List<IPassive> selectionPassives = new List<IPassive>();

    private void FilterList()
    {
        passivePool = new List<IPassive>();
        foreach (GameObject go in gameObjectList)
        {
            IPassive component = go.GetComponent<IPassive>();
            if(component != null)
            {
                passivePool.Add(component);
            }
        }
    }

    private void Start()
    {
        FilterList();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void ExecutePassiveByType(IPassive.PassiveType type)
    {
        foreach (IPassive passive in obtainedPassiveList)
        {
            passive.Execute(type);
        }
    }

    public void GivePassiveSelection()
    {
        selectionPassives.Clear();
        if(passivePool.Count >= 3)
        {
            // Randomly pick three different index
            while (selectionPassives.Count < 3)
            {
                int randomIndex = Random.Range(0, passivePool.Count);
                IPassive randomPassive = passivePool[randomIndex];
                if (!selectionPassives.Contains(randomPassive))
                {
                    selectionPassives.Add(randomPassive);
                }
            }

        }
        else if(passivePool.Count == 2)
        {
            // pick two different index
            selectionPassives.Add(passivePool[0]);
            selectionPassives.Add(passivePool[1]);
        }
        else if(passivePool.Count == 1)
        {
            selectionPassives.Add(passivePool[0]);
        }

        SendSelectionDataToUI();
        
    }

    private void SendSelectionDataToUI()
    {
        UI_OverworldHUD UIScreen = FindObjectOfType<UI_OverworldHUD>();
        if (passivePool.Count >= 3)
        {
            UIScreen.EnablePassiveSelectionUI(3);
            UIScreen.passiveSelection1_Text.text = selectionPassives[0].ShowDescription();
            UIScreen.passiveSelection2_Text.text = selectionPassives[1].ShowDescription();
            UIScreen.passiveSelection3_Text.text = selectionPassives[2].ShowDescription();
        }
        else if(passivePool.Count == 2)
        {
            UIScreen.EnablePassiveSelectionUI(2);
            UIScreen.passiveSelection1_Text.text = selectionPassives[0].ShowDescription();
            UIScreen.passiveSelection2_Text.text = selectionPassives[1].ShowDescription();
            UIScreen.passiveSelection3_Text.text = "Obtain " + (Mathf.RoundToInt(10 * playerManager.coinMultiplier)) + " coins";
        }
        else if(passivePool.Count == 1)
        {
            UIScreen.EnablePassiveSelectionUI(1);
            UIScreen.passiveSelection1_Text.text = selectionPassives[0].ShowDescription();
            UIScreen.passiveSelection2_Text.text = "Obtain " + (Mathf.RoundToInt(10 * playerManager.coinMultiplier)) + " coins";
            UIScreen.passiveSelection3_Text.text = "Obtain " + (Mathf.RoundToInt(10 * playerManager.coinMultiplier)) + " coins";
        }
        else
        {
            UIScreen.EnablePassiveSelectionUI(0);
            UIScreen.passiveSelection1_Text.text = "Obtain " + (Mathf.RoundToInt(10 * playerManager.coinMultiplier)) + " coins";
        }
    }

    public void AddSelectionToList(int selectedIndex) // this is used by the selection UI to add the selected passive to the player
    {
        if(selectedIndex > selectionPassives.Count - 1) 
        {
            playerManager.ObtainCoin(10);
            return;
        }

        if (passivePool.Contains(selectionPassives[selectedIndex]))
        {
            obtainedPassiveList.Add(selectionPassives[selectedIndex]);
            passivePool.Remove(selectionPassives[selectedIndex]);
        }
        else
        {
            playerManager.ObtainCoin(10);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.LogWarning("OBTAINED LIST:");
            foreach (IPassive passive in obtainedPassiveList)
            {
                passive.DebugData();
            }
            Debug.LogWarning("PASSIVE POOL:");
            foreach (IPassive passive in passivePool)
            {
                passive.DebugData();
            }
        }
    }
    
}
