using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{

    [SerializeField] private GameObject uiElement;
    [SerializeField] private GameObject upgradeCanvas;

    private bool isTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        uiElement.SetActive(false);
        upgradeCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTrigger)
        {
            uiElement.SetActive(false);
            upgradeCanvas.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
