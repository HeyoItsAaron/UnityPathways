using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodTarget : Target
{
    private void OnMouseDown()
    {
        HandleGoodTargetDestroyed();
    }
    private void OnTriggerEnter(Collider other)
    {
        HandleGoodTargetDestroyed();
    }

    private void HandleGoodTargetDestroyed()
    {
        AudioManager.instance.PlaySquishClip();
        StartCoroutine(DestroyTarget());
        GameManager.instance.UpdateScore(pointValue);
    }
}
