using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    public GameObject healthBar;
    private Transform transformHealthBar;
    private float health;
    private float startHealth;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.Find("HealthBar") == null)
        {
            GameObject objectHealthBar =
                Instantiate(healthBar, gameObject.transform.position + Vector3.up*2*gameObject.transform.localScale.y, Quaternion.Euler(0f, -180f, 0f));
            objectHealthBar.transform.parent = gameObject.transform;
            transformHealthBar = objectHealthBar.transform.Find("Bar");
        }
        else
        {
            transformHealthBar = gameObject.transform.Find("HealthBar").transform.Find("Bar");
        }

        transformHealthBar.localScale = new Vector3(1, 1);

        health = GetComponent<HealthEntity>().health;
        startHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateHealthBar()
    {
        health = GetComponent<HealthEntity>().health;
        transformHealthBar.localScale = new Vector3(health / startHealth, 1); 
    }

   
}
