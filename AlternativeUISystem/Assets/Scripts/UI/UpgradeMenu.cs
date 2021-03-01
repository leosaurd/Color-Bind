using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public static UpgradeMenu singleton;
    public Tower tower;
    public ButtonStatUpgrade[] buttonUpgradeBtns;
    public GameObject NoUpgrade;
    public GameObject HasUpgrade;


    private void Awake()
    {
        singleton = this;
    }

    public void OnDeselect()
    {
        tower = null;
    }

    public void OnSelect()
    {
        UpdateButtonText();
        for(int i = 0; i < buttonUpgradeBtns.Length; i++)
        {
            GenerateUpgradeIndicator(buttonUpgradeBtns[i]);
        }
    }

    public void GenerateUpgradeIndicator(ButtonStatUpgrade btn)
    {
        Stat stat = typeof(Tower.TowerVars).GetField(btn.stat).GetValue(UpgradeMenu.singleton.tower.stats) as Stat;
        GameObject container = btn.transform.parent.GetChild(1).gameObject;
        for (int j = 0; j < container.transform.childCount; j++)
        {
            Destroy(container.transform.GetChild(j).gameObject);
        }
        if(stat.maxLevel > 9)
        {
            btn.transform.localPosition = new Vector3(btn.transform.localPosition.x, -20 * (Mathf.CeilToInt(stat.maxLevel / 9f) - 1));
        }
        for(int i = 0; i < stat.maxLevel; i++)
        {
            float x = 20 * (i - ((Mathf.CeilToInt((i + 1) / 9f) - 1) * 9)) - 80;
            float y = -20 * (Mathf.CeilToInt((i + 1) / 9f) - 1);
            GenerateIndicator(new Vector2(x, y), i < stat.level, container);
        }
        if(stat.level >= stat.maxLevel)
        {
            btn.isEnabled = false;
        }
        else
        {
            btn.isEnabled = true;
        }
    }

    void GenerateIndicator(Vector2 pos, bool has, GameObject parent)
    {
        GameObject indicator = Instantiate(has ? HasUpgrade : NoUpgrade, Vector3.zero, Quaternion.Euler(Vector3.zero), parent.transform);
        indicator.GetComponent<RectTransform>().localPosition = pos;
    }

    public void UpdateButtonText()
    {
        for(int i = 0; i < buttonUpgradeBtns.Length; i++)
        {
            Stat stat = typeof(Tower.TowerVars).GetField(buttonUpgradeBtns[i].stat).GetValue(UpgradeMenu.singleton.tower.stats) as Stat;
            buttonUpgradeBtns[i].price  = stat.upgradeCost.ToString();
        }
    }

}
