using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEntity : MonoBehaviour
{
    public float health;
    public float startHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = PlayerPrefs.GetFloat("health", 100);
        startHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }
}
