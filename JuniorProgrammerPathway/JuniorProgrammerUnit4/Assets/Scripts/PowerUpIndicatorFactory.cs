using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpIndicatorFactory : GameObjectFactory
{
    [SerializeField] private GameObject powerupPrefab;
    protected override GameObject CreateGameObject()
    {
        Vector3 spawnPos = FindObjectOfType<PlayerController>().transform.position;
        return Instantiate(powerupPrefab, spawnPos, Quaternion.identity);
    }
}
