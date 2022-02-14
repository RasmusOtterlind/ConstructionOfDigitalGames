using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float damage = 10;
    private bool destroy = false;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        HandleSound();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") || collision.collider.gameObject.layer == 6)
        {
            if (collision.transform.gameObject.GetComponent<PlayerController>())
            {
                collision.transform.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            }
            HideGameObject();
        }
    }

    private void HandleSound() { 
        if (!audioSource.isPlaying && destroy)
        {
            Destroy(gameObject);
        }
    }

    private void HideGameObject()
    {
        destroy = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<TrailRenderer>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }

}
