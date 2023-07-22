using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodTarget : Target // INHERITANCE
{
    protected override void Start() // POLYMORPHISM
    {
        base.Start();

        if(GameManager.instance.Difficulty == 1f)
        {
            pointValue = 1;
        }
        else if(GameManager.instance.Difficulty == 0.5f)
        {
            pointValue = 2;
        }
        else if(GameManager.instance.Difficulty == 0.25f)
        {
            pointValue = 4;
        }
    }
    private void Update()
    {
        if (transform.position.y < -6)
        {
            GameManager.instance.UpdateLives(1);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hit) return;
        HandleGoodTargetDestroyed(); // ABSTRACTION
    }

    private void HandleGoodTargetDestroyed()
    {
        hit = true;
        AudioManager.instance.PlaySquishClip();
        StartCoroutine(DestroyTarget());
        GameManager.instance.UpdateScore(pointValue);
    }
}
