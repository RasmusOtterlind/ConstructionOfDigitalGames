using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliTrigger : MonoBehaviour
{

    private GameObject helicopter;

    // Start is called before the first frame update
    void Start()
    {
        helicopter = GameObject.FindWithTag("Helicopter");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            helicopter.GetComponent<FlyAway>().ToggleEvent();
            gameObject.SetActive(false);
        }
    }
}
