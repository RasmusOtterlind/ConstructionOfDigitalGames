using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float explosionForce = 700;
    public float explosionRange = 50;

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
    private void OnTriggerEnter(Collider other)
    {
        explosionStarted = true;
        ExplosionParticles();
        ExplosionForce();
        ExplosionSound();
        Destroy(transform.GetChild(0).gameObject, 0.1f);
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
           Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(explosionForce, transform.position + new Vector3(0, -10, 0), explosionRange);
            }
        }
    }

    private void ExplosionSound()
    {
        GetComponent<AudioSource>().Play();
    }

}
