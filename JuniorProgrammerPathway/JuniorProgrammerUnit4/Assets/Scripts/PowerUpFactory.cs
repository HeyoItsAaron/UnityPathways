using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFactory : GameObjectFactory
{
    [SerializeField] private GameObject powerupPrefab;
    private const float spawnRange = 9.0f;
    protected override GameObject CreateGameObject()
    {
        Vector3 spawnPos = GetRandomInSpawnRange(spawnRange, 0f);
        return Instantiate(powerupPrefab, spawnPos, Quaternion.identity);
    }
}
