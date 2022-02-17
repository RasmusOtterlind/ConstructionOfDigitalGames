using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEntity : MonoBehaviour
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = PlayerPrefs.GetFloat("health", 100);
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
