using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 3.0f;
    private Rigidbody enemyRigidbody;
    private Transform playerTransform;

    private void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        // The playerTransform is the transform of the game object that has the PlayerController script attached to it.
        playerTransform = FindObjectOfType<PlayerController>().gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 lookDirection = (playerTransform.position - transform.position).normalized;

        enemyRigidbody.AddForce(lookDirection * enemySpeed);

        DestroyOffScreen();
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<Powerup>() == null)
        {
            GameManager.Instance.DefeatedEnemy(this);
        }
    }
    private void DestroyOffScreen()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0) { Destroy(this.gameObject); }
    }
}
