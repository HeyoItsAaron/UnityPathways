using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Target : MonoBehaviour
{
    [SerializeField] protected int pointValue;

    [SerializeField] protected ParticleSystem destroyParticles;
    [SerializeField] protected ParticleSystem particles;
    protected Rigidbody targetRigidbody;

    protected float minSpeed = 12;
    protected float maxSpeed = 16;
    protected float maxTorque = 5;
    protected float xRange = 8;
    protected float ySpawnPos = -6;

    protected void Start()
    {
        targetRigidbody = GetComponent<Rigidbody>();
        targetRigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        targetRigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPosition();
    }
    protected Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    protected float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    protected Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    protected IEnumerator DestroyTarget()
    {
        particles = Instantiate(destroyParticles, transform.position, Quaternion.identity, null);
        particles.Play();
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
