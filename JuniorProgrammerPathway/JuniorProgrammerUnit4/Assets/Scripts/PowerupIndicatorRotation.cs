using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupIndicatorRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
