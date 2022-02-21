using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollAI : MonoBehaviour
{

    public GameObject root;
    public float life = 3;
    // Start is called before the first frame update

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    void Start()
    {
        Vector3 dir = Vector3.right * 10;
        if ((GameObject.FindWithTag("Player").transform.position - transform.position).x < 0)
        {
            dir = Vector3.right * 50;
        }
        else
        {
            dir = Vector3.left * 50;
        }
        
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddForce(dir, ForceMode.Impulse);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
