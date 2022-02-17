using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionForce = 500;
    public float explosionRange = 5;
    public float damagePercentage = 0.10f;

    private bool explosionStarted = false;

    public AudioClip whistlingClip;
    public AudioClip explostionClip;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 1000, out hit, 1000);
        if(!(hit.transform.gameObject.tag == "Player"))
        {
            Destroy(gameObject);
        }
     
        if (whistlingClip != null)
        {
            GetComponent<AudioSource>().clip = whistlingClip;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(explosionStarted && !GetComponent<AudioSource>().isPlaying) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
    }

    private void OnTriggerEnter(Collider other)
    {
        explosionStarted = true;
        ExplosionParticles();
        ExplosionForce();
        ExplosionSound();
        Destroy(transform.GetChild(0).gameObject, 0.1f);
        Destroy(transform.GetChild(2).gameObject, 0.1f);
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
        GetComponent<AudioSource>().clip = explostionClip;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Play();
    }

}
