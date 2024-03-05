using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextHandler : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 1f;
    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
