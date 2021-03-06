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
    private int currentCost;

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI[] costTexts;

    [SerializeField] private GameObject upgradeCanvas;
    //[SerializeField] private GameObject upgradeText;

    // Start is called before the first frame update
    void Start()
    {
        currentDamage = PlayerPrefs.GetFloat("damage", 10);
        currentHealth = PlayerPrefs.GetFloat("health", 100);
        currentGold = PlayerPrefs.GetInt("gold", 0);
        currentCost = PlayerPrefs.GetInt("upgradeCost", 100);
        UpdateCostText();
        UpdateGoldText();
        UpdateDamageText();
        UpdateHealthText();
    }

    private void Awake()
    {
        currentDamage = PlayerPrefs.GetFloat("damage", 10);
        currentHealth = PlayerPrefs.GetFloat("health", 100);
        currentGold = PlayerPrefs.GetInt("gold", 0);
        currentCost = PlayerPrefs.GetInt("upgradeCost", 100);
        UpdateCostText();
        UpdateGoldText();
        UpdateDamageText();
        UpdateHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            upgradeCanvas.SetActive(false);
        }
    }

    public void UpgradeDamage()
    {
        if (currentGold >= currentCost)
        {
            currentGold -= currentCost;
            PlayerPrefs.SetInt("gold", currentGold);
            currentCost = Mathf.RoundToInt(currentCost * 1.1f);
            PlayerPrefs.SetInt("upgradeCost", currentCost);
            float newDamage = currentDamage += 5;
            PlayerPrefs.SetFloat("damage", newDamage);
            UpdateUI();
        }
    }

    public void UpgradeHealth()
    {
        if (currentGold >= currentCost)
        {
            currentGold -= currentCost;
            PlayerPrefs.SetInt("gold", currentGold);
            currentCost = Mathf.RoundToInt(currentCost * 1.1f);
            PlayerPrefs.SetInt("upgradeCost", currentCost);
            float newHealth = currentHealth += 50;
            PlayerPrefs.SetFloat("health", newHealth);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        UpdateDamageText();
        UpdateCostText();
        UpdateGoldText();
        UpdateHealthText();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().FetchStatsAfterUpgrade();
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
        goldText.SetText("SEK: " + currentGold);
    }
    private void UpdateCostText()
    {
        foreach (TextMeshProUGUI costText in costTexts)
        {
            costText.SetText("Upgrade " + currentCost + ":-");
        }
    }

    public void BuyAk()
    {
        if (currentGold >= 200)
        {
            PlayerPrefs.SetInt("BoughtAK", 1);
            PlayerPrefs.SetInt("AK", 1);
            currentGold -= 200;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerController>().updateWeapon();
        }
    }
}
