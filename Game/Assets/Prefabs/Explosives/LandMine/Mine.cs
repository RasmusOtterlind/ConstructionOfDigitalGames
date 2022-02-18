using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float explosionForce = 500;
    public float explosionRange = 10;
    public float damagePercentage = 0.1f;

    private bool explosionStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(explosionStarted && !GetComponent<AudioSource>().isPlaying) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<HealthEntity>() != null || collision.collider.gameObject.layer == 8)
        {
            explosionStarted = true;
            ExplosionParticles();
            ExplosionForce();
            ExplosionSound();
            Destroy(transform.GetChild(0).gameObject, 0.1f);
        }
    }

    private void ExplosionParticles() {
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
