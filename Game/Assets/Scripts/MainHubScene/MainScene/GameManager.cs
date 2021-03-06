using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float defaultDamage = 10;
    private float defaultHealth = 100;
    private int defaultGold = 0;
    private int defaultCost = 100;

    private bool showMenu = false;
    private bool showDeadMenu = false;
    public GameObject menu = null;
    public GameObject deadMenu = null;
    public Slider volumeSlider;

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

        if (!PlayerPrefs.HasKey("upgradeCost"))
        {
            PlayerPrefs.SetInt("upgradeCost", defaultCost);
        }

        menu.SetActive(showMenu);
        deadMenu.SetActive(showDeadMenu);
    }

    // Update is called once per frame
    void Update()
    {
        if (showDeadMenu)
        {
            deadMenu.SetActive(true);
            showMenu = false;
            menu.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !showDeadMenu)
        {
            showMenu = !showMenu;
            menu.SetActive(showMenu);
            Time.timeScale = showMenu ? 0.0f : 1.0f;
        }
       
    }

    private void Awake()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

    public void setShowDeadMenu(bool value)
    {
        showDeadMenu = value;
    }

    public void OnToBaseClicked()
    {
        resetGame();
        SceneManager.LoadScene(1);
    }

    public void OnMainMenuClicked()
    {
        resetGame();
        SceneManager.LoadScene(0);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void VolumeChanged()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    private void resetGame()
    {
        showDeadMenu = false;
        deadMenu.SetActive(false);
        showMenu = false;
        menu.SetActive(false);
        Time.timeScale = 1.0f;
    }
    
}
