using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : GameObjectFactory
{
    [SerializeField] private GameObject enemyPrefab;
    private const float spawnRange = 9.0f;
    protected override GameObject CreateGameObject()
    {
        Vector3 spawnPos = GetRandomInSpawnRange(spawnRange, 0.15f);
        return Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
