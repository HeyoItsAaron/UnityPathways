using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float destructionDelay = 3f;

    void Start()
    {
        Destroy(gameObject, destructionDelay);
    }

}
