using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameObjectFactory : MonoBehaviour
{
    protected abstract GameObject CreateGameObject();

    public GameObject MakeGameObject()
    {
        return CreateGameObject();
    }

    protected Vector3 GetRandomInSpawnRange(float range, float height)
    {
        // range is for x, z, height is for y
        return new Vector3(Random.Range(-range, range), height, Random.Range(-range, range));
    }

}
