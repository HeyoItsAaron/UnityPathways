using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private Quaternion startRotation;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private InputReader inputReader;

    private void Start()
    {
        startRotation = transform.rotation;
        GameManager.Instance.OnGameStart += ResetCameraPosition;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameStart -= ResetCameraPosition;
    }
    private void Update()
    {
        if (!GameManager.Instance.gamePlaying)
        {
            transform.Rotate(Vector3.up, 0.5f * rotationSpeed * Time.deltaTime);
        }
        float horizontalInput = inputReader.horizontalInput;
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }

    public void ResetCameraPosition()
    {
        transform.rotation = startRotation;
    }
}
