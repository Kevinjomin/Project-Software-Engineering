using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnableObjects = new List<GameObject>();

    public void SpawnObject()
    {
        GameObject objectToSpawn = GetRandomObjectFromList();
        GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
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

}
