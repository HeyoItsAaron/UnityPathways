using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private float spawnRange = 9.0f;
    [SerializeField] private GameObject enemyPrefab;

    public GameObject MakeEnemy()
    {
        Vector3 spawnPos = GetRandomInSpawnRange(spawnRange);
        return Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    private Vector3 GetRandomInSpawnRange(float range)
    {
        return new Vector3(Random.Range(-range, range), 0.15f, Random.Range(-range, range));
    }
}
