using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    private GameManager gameManager;

    private List<Spawner> spawners;
    private GameObject chosenStage;

    [SerializeField] private List<GameObject> stageList;

    public int currentLevel = 1;

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

        gameManager = FindObjectOfType<GameManager>();
        SetupLevel();
    }

    public void SetupLevel()
    {
        chosenStage = Instantiate(ChooseStageRandomly());

        UpdatePlayerPosition();

        spawners = new List<Spawner>();
        FindSpawners(spawners);
        foreach(Spawner spawner in spawners)
        {
            spawner.SpawnObject();
        }
        Debug.Log(spawners.Count);
    }

    public void EnterNextLevel()
    {
        currentLevel++;
        gameManager.ReloadCurrentScene();
    }


    private void RemoveCurrentLevel()
    {
        foreach (Spawner spawner in spawners)
        {
            spawner.DespawnObject();
        }
        spawners.Clear();
        //Destroy(chosenStage);
    }

    private void UpdatePlayerPosition()
    {
        Transform playerPosition = GameObject.Find("Player Spawner").transform;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (playerPosition == null)
        {
            Debug.LogError("Player spawn not found. Please add it in the stage and check the level manager again.");
            return;
        }
        if (player == null)
        {
            Debug.LogError("Level manager could not find the player");
            return;
        }
        player.transform.position = playerPosition.position;
    }

    private GameObject ChooseStageRandomly()
    {
        if (stageList.Count == 0)
        {
            Debug.Log("No stage in list");
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, stageList.Count);

        return stageList[randomIndex];
    }

    private void FindSpawners(List<Spawner> spawnerList)
    {
        Spawner[] foundSpawners = FindObjectsOfType<Spawner>();
        spawnerList.AddRange(foundSpawners);
    }
}
