using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPulsing : MonoBehaviour
{
    public float pulsingScale = 1.1f;
    public float pulsingSpeed = 1.5f;

    private TextMeshProUGUI textMesh;
    private Vector3 originalScale;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        float pulsingScaleFactor = 1f + Mathf.Sin(Time.time * pulsingSpeed) * (pulsingScale - 1f);
        transform.localScale = originalScale * pulsingScaleFactor;
    }
}
