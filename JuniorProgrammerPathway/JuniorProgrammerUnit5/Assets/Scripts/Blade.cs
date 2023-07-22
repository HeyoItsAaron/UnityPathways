using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class Blade : MonoBehaviour
{
    private Camera mainCamera;
    private Collider bladeCollider;
    [SerializeField] private GameObject bladeTrailPrefab;
    private TrailRenderer bladeTrail;
    private UnityEngine.Color savedColor;
    private Vector2 previousPosition;

    private bool isSlicing;
    private readonly float minSlideVelocity = 0.01f;

    public Vector3 SliceDirection { get; private set; }
    

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider = GetComponent<Collider>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (isSlicing)
        {
            UpdateSlicing();
        }
    }

    private void StartSlicing()
    {
        isSlicing = true;

        bladeTrail = Instantiate(bladeTrailPrefab, transform).GetComponent<TrailRenderer>();
        previousPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        bladeTrail.Clear();
        bladeTrail.startColor = savedColor;
        bladeTrail.endColor = savedColor;

        bladeCollider.enabled = false;
    }
    private void UpdateSlicing()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        Vector2 newPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        transform.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSlideVelocity;

        previousPosition = newPosition;
    }
    private void StopSlicing()
    {
        isSlicing = false;

        bladeTrail.transform.SetParent(null);
        Destroy(bladeTrail.gameObject,2f);

        bladeCollider.enabled = false;
    }
    

    public void ChangeBladeColor(UnityEngine.Color color)
    {
        savedColor = color;
    }
}
