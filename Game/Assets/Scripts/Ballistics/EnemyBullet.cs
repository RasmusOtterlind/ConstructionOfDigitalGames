using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") || collision.collider.gameObject.layer == 6)
        {
            if (collision.transform.gameObject.GetComponent<PlayerController>())
            {
                collision.transform.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
