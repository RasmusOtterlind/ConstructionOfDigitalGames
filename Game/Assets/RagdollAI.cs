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
        root.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10);
        
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddForce(Vector3.right*10, ForceMode.Impulse);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
