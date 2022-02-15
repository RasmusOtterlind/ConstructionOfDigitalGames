using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private float defaultDamage = 10;
    private float defaultHealth = 100;
    private int defaultGold = 0;

    private bool showMenu = false;
    private bool showDeadMenu = false;
    public GameObject menu = null;
    public GameObject deadMenu = null;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("damage"))
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

        menu.SetActive(showMenu);
        deadMenu.SetActive(showDeadMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !showDeadMenu)
        {
            showMenu = !showMenu;
            menu.SetActive(showMenu);
            Time.timeScale = showMenu ? 0.0f : 1.0f;
        }
    }

    public void setShowDeadMenu(bool value)
    {
        showDeadMenu = value;
    }

    public void OnMainMenuClicked()
    {
        showMenu = !showMenu;
        Time.timeScale = showMenu ? 0.0f : 1.0f;
        SceneManager.LoadScene(0);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
