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

    [SerializeField] private List<GameObject> GameObjectList = new List<GameObject>();

    private List<IPassive> PassiveList;

    private void FilterList()
    {
        PassiveList = new List<IPassive>();
        foreach (GameObject go in GameObjectList)
        {
            IPassive component = go.GetComponent<IPassive>();
            if(component != null)
            {
                PassiveList.Add(component);
            }
        }
    }

    private void Start()
    {
        FilterList();
    }

    public void ExecutePassiveByType(IPassive.PassiveType type)
    {
        foreach (IPassive passive in PassiveList)
        {
            passive.Execute(type);
        }
    }


    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            foreach(IPassive passive in PassiveList)
            {
                passive.DebugData();
            }
        }
    }
    
}
