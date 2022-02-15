using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private float defaultDamage = 10;
    private float defaultHealth = 100;
    private int defaultGold = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("damange"))
        {
            PlayerPrefs.SetFloat("damage", defaultDamage);
        }

        if (!PlayerPrefs.HasKey("health"))
        {
            PlayerPrefs.SetFloat("health", defaultHealth);
        }

        if (!PlayerPrefs.HasKey("gold"))
        {
            PlayerPrefs.SetInt("gold", defaultGold);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
