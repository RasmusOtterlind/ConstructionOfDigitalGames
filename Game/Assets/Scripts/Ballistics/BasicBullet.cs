using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public float power;
    public int bounces;
    public Vector3 hitPoint;
    // Start is called before the first frame update
    void Start()
    {
       
        this.GetComponent<Rigidbody>().AddForce((hitPoint - this.transform.position).normalized * power);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * power);
        bounces--;
        if (bounces < 1)
        {
            Destroy(this.gameObject);
        }

    }
}
