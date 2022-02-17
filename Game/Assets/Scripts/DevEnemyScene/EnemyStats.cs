using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    public GameObject healthBar;
    private Transform transformHealthBar;
    public float maxHealth = 100f;
    private float health;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        audioSource = GetComponent<AudioSource>();
        if (gameObject.transform.Find("HealthBar") == null)
        {
            GameObject objectHealthBar =
                Instantiate(healthBar, gameObject.transform.position + Vector3.up*2*gameObject.transform.localScale.y, Quaternion.Euler(0f, -180f, 0f));
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
        transformHealthBar.localScale = new Vector3(health / maxHealth, 1);
        Debug.Log(health);
    }

    public float getHealth()
    {
        return health;
    }
}
