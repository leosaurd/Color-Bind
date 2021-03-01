using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonStatUpgrade : MonoBehaviour
{
    Button b;
    TextMeshProUGUI textMesh;
    public string price;
    public string stat;
    public bool isEnabled = true;
    private void Awake()
    {
        b = GetComponent<Button>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        
        if (UpgradeMenu.singleton.tower)
        {
            if (!isEnabled) textMesh.text = "Max Level Reached";
            else textMesh.text = $"Upgrade: ${price}";
            if (!isEnabled || price == null || price == "" || HUDManager.singleton.money < int.Parse(price))
            {
                b.interactable = false;
            }
            else
            {
                b.interactable = true;
            }
        }
        
    }

    public void DoUpgrade()
    {
        typeof(Stat).GetMethod("Upgrade").Invoke(typeof(Tower.TowerVars).GetField(stat).GetValue(UpgradeMenu.singleton.tower.stats), null);
        UpgradeMenu.singleton.UpdateButtonText();
        UpgradeMenu.singleton.GenerateUpgradeIndicator(this);
    }

}
