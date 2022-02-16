using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{

    private float currentDamage;
    private float currentHealth;
    private int currentGold;

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private GameObject upgradeCanvas;
    [SerializeField] private GameObject upgradeText;

    // Start is called before the first frame update
    void Start()
    {
        currentDamage = PlayerPrefs.GetFloat("damage");
        currentHealth = PlayerPrefs.GetFloat("health");
        currentGold = PlayerPrefs.GetInt("gold");
        UpdateGoldText();
        UpdateDamageText();
        UpdateHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseWindow();
        }
    }

    public void UpgradeDamage()
    {
        float newDamage = currentDamage += 5;
        PlayerPrefs.SetFloat("damage", newDamage);
        UpdateDamageText();
        UpdateGoldText();
    }

    public void UpgradeHealth()
    {
        float newHealth = currentHealth += 50;
        PlayerPrefs.SetFloat("health", newHealth);
        UpdateHealthText();
        UpdateGoldText();
    }

    public void CloseWindow()
    {
        upgradeCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void UpdateDamageText()
    {
        damageText.SetText("Damage: " + currentDamage);
    }

    private void UpdateHealthText()
    {
        healthText.SetText("Health: " + currentHealth);
    }

    private void UpdateGoldText()
    {
        goldText.SetText("Gold: " + currentGold);
    }
}
