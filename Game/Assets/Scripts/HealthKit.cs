using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    private HealthEntity healthEntity;

    private void Awake()
    {
        healthEntity = GameObject.Find("SimplePlayer").GetComponent<HealthEntity>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
        {
            return;
        }
        if (healthEntity != null)
        {
            healthEntity.health += 50;
        }
        Destroy(gameObject);
    }
}
