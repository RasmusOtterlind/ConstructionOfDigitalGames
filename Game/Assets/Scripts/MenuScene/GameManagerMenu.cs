using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManagerMenu : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        PlayerPrefs.GetFloat("volume", 1.0f);
    }

    private void Awake()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
