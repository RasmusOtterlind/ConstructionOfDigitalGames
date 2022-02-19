using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{

    [SerializeField] private GameObject uiElement;
    public int sceneToSwitchTo = 2;
    
    private bool isTrigger = false;

    private void Start()
    {
        uiElement.SetActive(false);
    }
    
    private void Update(){
        if (Input.GetKeyDown(KeyCode.E) && isTrigger)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerPrefs.SetInt("gold", player.GetComponent<PlayerController>().gold);
            player.GetComponent<HealthEntity>().health = player.GetComponent<HealthEntity>().startHealth;
            player.GetComponent<PlayerController>().damage = PlayerPrefs.GetFloat("damage");
            SceneManager.LoadScene(sceneToSwitchTo);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isTrigger = true;
            uiElement.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = false;
            uiElement.SetActive(false);
        }
    }

}
