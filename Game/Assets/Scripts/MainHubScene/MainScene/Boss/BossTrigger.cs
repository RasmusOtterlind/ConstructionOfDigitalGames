using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{

    [SerializeField] private GameObject[] fences;
    [SerializeField] private GameObject boss;

    public float bombIntensity = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            OnBossDeath();
        }
    }

    private void OnBossDeath()
    {
        foreach (GameObject fence in fences)
        {
            fence.SetActive(false);
        }
        GameObject.Find("BombManager").GetComponent<BombManager>().intenisty = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("BombManager").GetComponent<BombManager>().intenisty = bombIntensity;
            Debug.Log("MADE IT");
            foreach (GameObject fence in fences) {
                fence.SetActive(true);
            }
            boss.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            //gameObject.SetActive(false);
        }
    }
}
