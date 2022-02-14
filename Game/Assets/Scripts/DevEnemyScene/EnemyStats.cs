using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public float health = 100;

    private float damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnHitByBullet(float damageTaken)
    {
        health -= damageTaken;
    }

    public float getHealth()
    {
        return health;
    }
}
