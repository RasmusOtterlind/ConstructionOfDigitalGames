using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{

    [SerializeField] private GameObject uiElement;
    
    private bool isTrigger = false;

    private void Start()
    {
        uiElement.SetActive(false);
    }
    
    private void Update(){
        if (Input.GetKeyDown(KeyCode.E) && isTrigger)
        {
            SceneManager.LoadScene(2);
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
