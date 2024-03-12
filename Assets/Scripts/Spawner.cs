using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnableObjects = new List<GameObject>();

    private GameObject objectToSpawn;
    private GameObject spawnedObject;

    public void SpawnObject()
    {
        objectToSpawn = GetRandomObjectFromList();
        spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
    }

    private GameObject GetRandomObjectFromList()
    {

        if (spawnableObjects.Count == 0)
        {
            Debug.Log("No spawnable object in list");
            return null;
        }
        int randomIndex = UnityEngine.Random.Range(0, spawnableObjects.Count);

        return spawnableObjects[randomIndex];
    }

    public void DespawnObject()
    {
        Destroy(spawnedObject);
    }
}
