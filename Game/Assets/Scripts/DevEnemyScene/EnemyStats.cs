using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    public GameObject healthBar;
    private Transform transformHealthBar;
    public float health = 100;

    private float damage = 10;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (gameObject.transform.Find("HealthBar") == null)
        {
            GameObject objectHealthBar =
                Instantiate(healthBar, gameObject.transform.position + Vector3.up*2, Quaternion.Euler(0f, -180f, 0f));
            objectHealthBar.transform.parent = gameObject.transform;
            transformHealthBar = objectHealthBar.transform.Find("Bar");
        }

        transformHealthBar.localScale = new Vector3(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnHitByBullet(float damageTaken)
    {
        health -= damageTaken;
        transformHealthBar.localScale = new Vector3(health / 100f, 1);
    }

    public float getHealth()
    {
        return health;
    }
}
