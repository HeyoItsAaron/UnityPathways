using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTarget : Target
{
    private void OnMouseDown()
    {
        HandleBadTargetDestroyed();
    }
    private void OnTriggerEnter(Collider other)
    {
        HandleBadTargetDestroyed();
    }

    private void HandleBadTargetDestroyed()
    {
        AudioManager.instance.PlayBoomClip();
        StartCoroutine(DestroyTarget());
        GameManager.instance.UpdateLives(pointValue);
    }
    
}
