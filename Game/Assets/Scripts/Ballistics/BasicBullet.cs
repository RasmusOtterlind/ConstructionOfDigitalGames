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

    public float damage = 10;

    public bool spawnedByPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = Time.time;
        this.GetComponent<Rigidbody>().AddForce((hitPoint - this.transform.position).normalized * power);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > lifeTimer + life)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && !spawnedByPlayer)
        {
            collision.transform.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
        else if (collision.collider.CompareTag("Enemy"))
        {
            collision.transform.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
        }
        GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * power);
        bounces--;
        if (bounces < 1)
        {
            Destroy(gameObject);
        }
    }
}
