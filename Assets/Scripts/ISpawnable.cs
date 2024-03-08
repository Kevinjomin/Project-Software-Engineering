using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    void Spawn(Vector3 position, Quaternion rotation);
    void Despawn();
}
