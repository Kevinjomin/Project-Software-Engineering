using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    private GameManager gameManager;

    public List<Spawner> spawners;

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
        spawners = new List<Spawner>();
        FindSpawners(spawners);
        foreach(Spawner spawner in spawners)
        {
            spawner.SpawnObject();
        }
    }

    private void FindSpawners(List<Spawner> spawnerList)
    {
        Spawner[] foundSpawners = FindObjectsOfType<Spawner>();
        spawnerList.AddRange(foundSpawners);
    }
}
