using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private enum SpawnDirection
    {
        left, right
    }
    [SerializeField] private SpawnDirection spawnDirection = SpawnDirection.left;

    [SerializeField] private List<GameObject> spawnableObjects = new List<GameObject>();

    private GameObject objectToSpawn;
    private GameObject spawnedObject;

    public void SpawnObject()
    {
        objectToSpawn = GetRandomObjectFromList();
        if(spawnDirection == SpawnDirection.left)
        {
            spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
        else
        {
            spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
            Vector3 currentScale = spawnedObject.transform.localScale;
            currentScale.x *= -1;
            spawnedObject.transform.localScale = currentScale;
        }     
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
