using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : GameObjectFactory
{
    [SerializeField] private GameObject playerPrefab;
    protected override GameObject CreateGameObject()
    {
        return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }
}
