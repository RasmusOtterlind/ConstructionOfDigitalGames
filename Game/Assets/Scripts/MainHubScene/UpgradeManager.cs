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
        UpdateDamageText();
        UpdateGoldText();
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
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
