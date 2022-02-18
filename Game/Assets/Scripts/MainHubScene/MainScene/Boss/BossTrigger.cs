using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{

    private GameObject[] fences;
    private GameObject boss;

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
        if (other.CompareTag("Player"))
        {
            Debug.Log("MADE IT");
            fences = GameObject.FindGameObjectsWithTag("Fence");
            foreach (GameObject fence in fences) {
                fence.SetActive(true);
            }
        }
        //boss.SetActive(true);
    }
}
