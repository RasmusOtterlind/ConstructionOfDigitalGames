using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    [SerializeField] private GameObject uiElement;
    [SerializeField] private GameObject levelSelectCanvas;

    private bool isTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        uiElement.SetActive(false);
        levelSelectCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTrigger)
        {
            Time.timeScale = 0.0f;
            levelSelectCanvas.SetActive(true);
            uiElement.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelSelectCanvas.SetActive(false);
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

    public void GoToNormalLevel()
    {
        Time.timeScale = 1.0f;
        levelSelectCanvas.SetActive(false);
        SceneManager.LoadScene(2);
    }

    public void GoToRandomLevel()
    {
        Time.timeScale = 1.0f;
        levelSelectCanvas.SetActive(false);
        SceneManager.LoadScene(3);
    }
    public void CloseWindow()
    {
        levelSelectCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
