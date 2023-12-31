using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRigidbody;
    private float moveSpeed = 5.0f;
    private GameObject focalPoint;
    private bool hasPowerup;
    private float powerupStrength = 15.0f;
    private GameObject powerupIndicator;
    private InputReader inputReader;
    
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        
        // The focal point is the game object that has the RotateCamera script attached to it.
        focalPoint = FindObjectOfType<RotateCamera>().gameObject;

        hasPowerup = false;
        inputReader = FindObjectOfType<InputReader>();
    }

    private void Update()
    {
        if(GameManager.Instance.gamePlaying == true)
        {
            float forwardInput = inputReader.verticalInput;
            playerRigidbody.AddForce(focalPoint.transform.forward * moveSpeed * forwardInput);
        }
        if (hasPowerup == true)
        {
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.05f, 0);
        }
        DestroyOffScreen();
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.Instance.EndGame();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Powerup>() != null)
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            if (powerupIndicator != null) Destroy(powerupIndicator);
            powerupIndicator = GameManager.Instance.PowerupIndicatorFactory.MakeGameObject();
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null && hasPowerup)
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position); //.normalized
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        Destroy(powerupIndicator);
        powerupIndicator = null;
    }
    private void OnDestroy()
    {
        Destroy(powerupIndicator);
    }

    private void DestroyOffScreen()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0) { Destroy(this.gameObject); }
    }
}
