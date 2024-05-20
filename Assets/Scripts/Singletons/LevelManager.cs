using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    private GameManager gameManager;
    private PlayerManager playerManager;

    private List<Spawner> spawners;
    private GameObject chosenStage;

    public enum Tileset
    {
        Grassland, Desert, Snow, Volcano
    }
    public Tileset tileset = Tileset.Grassland;

    [SerializeField] private List<GameObject> grasslandStageList;

    public enum Difficulty
    {
        Easy, Medium, Hard
    }

    public Difficulty difficulty = Difficulty.Easy;
    public int currentLevel = 1;

    public float multiplierHP;
    public float multiplierDamage;

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
        playerManager = FindObjectOfType<PlayerManager>();
        SetupLevel();
    }

    public void ResetRun()
    {
        currentLevel = 1;
        multiplierDamage = 1f;
        multiplierHP = 1f;
        if(gameManager.tempDifficulty == 1)
        {
            difficulty = Difficulty.Easy;
        }
        else if(gameManager.tempDifficulty == 2)
        {
            difficulty = Difficulty.Medium;
        }
        else if(gameManager.tempDifficulty == 3)
        {
            difficulty = Difficulty.Hard;
        }
        else
        {
            Debug.LogError("Level manager cannot find selected difficulty from gameManager");
        }
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
    }

    public void EnterNextLevel()
    {
        currentLevel++;
        multiplierHP = 1f + (0.2f * currentLevel - 1);
        multiplierDamage = 1f + (0.1f * currentLevel - 1);
        IncreaseCoinMultiplierByLevel();
        gameManager.ReloadCurrentScene();
    }

    private void IncreaseCoinMultiplierByLevel()
    {
        float multiplier;
        switch (difficulty)
        {
            case Difficulty.Hard:
                multiplier = 4f;
                break;
            case Difficulty.Medium:
                multiplier = 2f;
                break;
            default:
                multiplier = 1f;
                break;
        }
        playerManager.coinMultiplier += 0.01f * multiplier;
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
        if (grasslandStageList.Count == 0)
        {
            Debug.Log("No stage in list");
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, grasslandStageList.Count);

        return grasslandStageList[randomIndex];
    }

    private void FindSpawners(List<Spawner> spawnerList)
    {
        Spawner[] foundSpawners = FindObjectsOfType<Spawner>();
        spawnerList.AddRange(foundSpawners);
    }
}
