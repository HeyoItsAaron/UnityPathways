using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTarget : Target // INHERITANCE
{
    private void OnTriggerEnter(Collider other)
    {
        if(hit) return;
        HandleBadTargetDestroyed();
    }

    private void HandleBadTargetDestroyed()
    {
        hit = true;
        AudioManager.instance.PlayBoomClip();
        StartCoroutine(DestroyTarget());
        GameManager.instance.UpdateLives(pointValue);
    }
    
}
