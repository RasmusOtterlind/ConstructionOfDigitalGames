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
            if (healthEntity.health < healthEntity.startHealth - 50f)
            {
                healthEntity.health += 50;
            }
            else if (healthEntity.startHealth - healthEntity.health < 50)
            {
                healthEntity.health = healthEntity.startHealth;
            }
        }
        Destroy(gameObject);
    }
}
