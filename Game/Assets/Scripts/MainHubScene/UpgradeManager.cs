using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        goldText.SetText("Gold: " + PlayerPrefs.GetInt("gold"));
        damageText.SetText("Damage: " + PlayerPrefs.GetFloat("damage"));
        healthText.SetText("Health: " + PlayerPrefs.GetFloat("health"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
