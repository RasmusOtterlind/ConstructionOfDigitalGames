using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public float power;
    public int bounces;
    public Vector3 hitPoint;
    public float life = 3;
    private float lifeTimer;

    private float damage = 10;

    public bool spawnedByPlayer = false;

    private AudioSource audioSource;

    private bool destroy = false;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = Time.time;
        this.GetComponent<Rigidbody>().AddForce((hitPoint - this.transform.position).normalized * power);
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.90f, 1.10f);
        audioSource.Play();
    }

    void Awake()
    {
        damage = PlayerPrefs.GetFloat("damage");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > lifeTimer + life)
        {
            Destroy(gameObject);
        }
        //HandleSound();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !spawnedByPlayer)
        {
            HealthEntity healthEntity = collision.collider.GetComponent<HealthEntity>();
            healthEntity.takeDamage(damage);
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
            HealthEntity healthEntity = collision.collider.GetComponent<HealthEntity>();
            Debug.Log(healthEntity == null);
            healthEntity.takeDamage(damage);
        }
        Destroy(gameObject);

        /*GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * power);
        bounces--;
        if (bounces < 1)
        {
            HideGameObject();
        }
        */
    }

    private void HandleSound()
    {
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
