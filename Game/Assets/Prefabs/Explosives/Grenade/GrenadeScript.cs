using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    public float explosionForce = 300;
    public float explosionRange = 10;
    public float damagePercentage = 0.25f;

    private float timeToLive = 3.0f;
    private float timeStarted;

    private bool explosionStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        timeStarted = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeStarted + timeToLive && !explosionStarted)
        {
            explosionStarted = true;
            ExplosionParticles();
            ExplosionForce();
            ExplosionSound();
            Destroy(transform.GetChild(0).gameObject, 0.1f);
            Destroy(gameObject, 5.0f);
        }
    }

    private void ExplosionParticles()
    {
        GameObject explosion = transform.GetChild(1).gameObject;
        explosion.GetComponent<ParticleSystem>().Play();
    }


    private void ExplosionForce()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (Collider collider in colliders)
        {
            HealthEntity healthEntity = collider.GetComponent<HealthEntity>();
            if (healthEntity != null)
            {
                healthEntity.takeDamage(healthEntity.startHealth * damagePercentage);
            }
        }
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRange);

    }

    private void ExplosionSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
