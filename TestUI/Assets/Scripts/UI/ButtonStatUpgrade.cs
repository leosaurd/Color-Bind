using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ButtonStatUpgrade : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI Target;
    [SerializeField]
    TextMeshProUGUI AimMode;
    [SerializeField]
    TextMeshProUGUI DMG;
    [SerializeField]
    TextMeshProUGUI RNG;
    [SerializeField]
    TextMeshProUGUI FR;
    [SerializeField]
    TextMeshProUGUI UNIQUE;
    [SerializeField]
    TextMeshProUGUI SellPrice;
    public Tower twr;
    Button b;
    public static ButtonStatUpgrade singleton;
    private string maxed = "MAX";
    public bool tut;
    private void Awake()
    {
        singleton = this;
    }

    public void Update()
    {
        //introduce the hotkeys
        if (CreateTower.singleton.allTowers.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                shiftAllTowers();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                shiftAllTowersRed();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                shiftAllTowersGreen();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                shiftAllTowersBlue();
            }
        }

        if (!HUDManager.singleton.tut)
        {
            if (twr && TowerUpgradePopout.singleton.isOut)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        ReverseChangeTarget();
                    }
                    if (Input.GetKeyDown(KeyCode.Tab))
                    {
                        ReverseChangeAim();
                    }
                }


                else if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    ChangeTarget();
                }
                else if (Input.GetKeyDown(KeyCode.Tab))
                {
                    ChangeAim();
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    AddDmg();
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    AddRng();
                }
                else if (Input.GetKeyDown(KeyCode.C))
                {
                    AddFR();
                }
                else if (Input.GetKeyDown(KeyCode.V))
                {
                    UpgradeUNIQUE();
                }
                else if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    twr.Sell();
                }
            }
        }
    }

    public void UpdateText()
    {
        twr = UpgradeMenu.singleton.tower;
        Target.text = twr.stats.target.ToString();
        AimMode.text = twr.currentAimMode.ToString();
        updateTextBetter(ref twr.mxlvl.damage, ref twr.upgradelevel.damage, ref twr.bP.damage, DMG);
        updateTextBetter(ref twr.mxlvl.range, ref twr.upgradelevel.range, ref twr.bP.range, RNG);
        updateTextBetter(ref twr.mxlvl.fireRate, ref twr.upgradelevel.fireRate, ref twr.bP.fireRate, FR);
        updateTextBetter(ref twr.mxlvl.unique, ref twr.upgradelevel.unique, ref twr.bP.unique, UNIQUE);
    }

    private void updateTextBetter(ref int mxlvl, ref int ul, ref float bp, TextMeshProUGUI txt)
    {
        b = txt.GetComponentInParent<Button>();
        Image img = b.GetComponent<Image>();
        Image butimg = b.transform.GetChild(0).GetComponentInChildren<Image>();

        if (mxlvl > ul) 
        {
            txt.text = "LVL: " + ul + "/" + mxlvl + " Price:   " + bp;
        }
        else
        {
            txt.text = maxed;
            bp = 0;
            butimg.enabled = false;
        }
        //ternary operator to set button color.
        img.color = (HUDManager.singleton.money < bp) ? Color.red : Color.white;
        b.interactable = (b && HUDManager.singleton.money >= bp && !string.Equals(txt.text, maxed)) ? true : false;
        SellPrice.text = "Sell:   " + Mathf.Round(twr.stats.price / 2);
    }

    public void Sell()
    {
        twr.Sell();
    }

    // Update is called once per frame
    public void AddDmg()
    {
        twr.Upgrade(ref twr.stats.damage, ref twr.bP.damage,  twr.mxlvl.damage, ref twr.upgradelevel.damage,  twr.incb.damage); 
        if (HUDManager.singleton.tut)
        {
            TutorialText.singleton.ChangeText();
        }
    }

    public void AddRng()
    {
        twr.Upgrade(ref twr.stats.range, ref twr.bP.range,  twr.mxlvl.range, ref twr.upgradelevel.range,  twr.incb.range);
        twr.GetComponentInChildren<RangeIndicator>().refrange(); 
        if (HUDManager.singleton.tut)
        {
            TutorialText.singleton.ChangeText();
        }
    }
    public void AddFR()
    {
        twr.Upgrade(ref twr.stats.fireRate, ref twr.bP.fireRate,  twr.mxlvl.fireRate, ref twr.upgradelevel.fireRate,  twr.incb.fireRate);
        if (HUDManager.singleton.tut)
        {
            TutorialText.singleton.ChangeText();
        }
    }

    public void UpgradeUNIQUE()
    {
        twr.Upgrade(ref twr.stats.unique, ref twr.bP.unique, twr.mxlvl.unique, ref twr.upgradelevel.unique, 0);
        if (HUDManager.singleton.tut)
        {
            TutorialText.singleton.ChangeText();
        }
    }


    //code for rightclick etc
    public void ChangeAim()
    {
        if (twr.GetComponent<TowerB>() != null || twr.GetComponent<TowerF>() != null || twr.GetComponent<TowerH>() != null || twr.GetComponent<TowerE>() != null)
        {
            return;
        }
        twr.currentAimMode++;
        if ((int)twr.currentAimMode >= Enum.GetValues(typeof(Tower.aimMode)).Length)
            twr.currentAimMode = 0;
        AimMode.text = twr.currentAimMode.ToString();
    }

    public void ReverseChangeAim()
    {
        if (twr.GetComponent<TowerB>() != null || twr.GetComponent<TowerF>() != null || twr.GetComponent<TowerH>() != null || twr.GetComponent<TowerE>() != null)
        {
            return;
        }
            twr.currentAimMode--;
        if(twr.currentAimMode < 0)
        {
            twr.currentAimMode = (Tower.aimMode)Enum.GetValues(typeof(Tower.aimMode)).Length-1;
        }
        AimMode.text = twr.currentAimMode.ToString();
    }

    public void ChangeTarget()
    {
        twr.stats.target++;
        if ((int)twr.stats.target >= Enum.GetValues(typeof(Tower.colorAim)).Length)
        {
            twr.stats.target = 0;
        }
        Target.text = twr.stats.target.ToString();
        //Part of extra
        twr.ColorChange();

        if (HUDManager.singleton.tut)
        {
            TutorialText.singleton.ChangeText();
        }
    }

    public void ReverseChangeTarget()
    {
        twr.stats.target--;
        if (twr.stats.target < 0)
        {
            twr.stats.target = (Tower.colorAim)Enum.GetValues(typeof(Tower.colorAim)).Length-1;
        }
        Target.text = twr.stats.target.ToString();
        //Part of extra
        twr.ColorChange();
    }


    public void shiftAllTowers()
    {
        GameObject[] towers = CreateTower.singleton.allTowers.ToArray();
        Tower tower;
        //Color change mechanic for all towers bound to B(probably should change it later)
        for(int i = 0; i < towers.Length; i++)
        {
            tower = towers[i].GetComponent<Tower>();
            tower.stats.target--;
            if (tower.stats.target < 0)
            {
                tower.stats.target = (Tower.colorAim)Enum.GetValues(typeof(Tower.colorAim)).Length - 1;
            }
            Target.text = tower.stats.target.ToString();
            tower.ColorChange();
        }
    }

    public void shiftAllTowersRed()
    {
        GameObject[] towers = CreateTower.singleton.allTowers.ToArray();
        Tower tower;
        //Color change mechanic for all towers bound to B(probably should change it later)
        for (int i = 0; i < towers.Length; i++)
        {
            tower = towers[i].GetComponent<Tower>();
            tower.stats.target = Tower.colorAim.Red;
            Target.text = tower.stats.target.ToString();
            tower.ColorChange();
        }
    }

    public void shiftAllTowersGreen()
    {
        GameObject[] towers = CreateTower.singleton.allTowers.ToArray();
        Tower tower;
        //Color change mechanic for all towers bound to B(probably should change it later)
        for (int i = 0; i < towers.Length; i++)
        {
            tower = towers[i].GetComponent<Tower>();
            tower.stats.target = Tower.colorAim.Green;
            Target.text = tower.stats.target.ToString();
            tower.ColorChange();
        }
    }

    public void shiftAllTowersBlue()
    {
        GameObject[] towers = CreateTower.singleton.allTowers.ToArray();
        Tower tower;
        //Color change mechanic for all towers bound to B(probably should change it later)
        for (int i = 0; i < towers.Length; i++)
        {
            tower = towers[i].GetComponent<Tower>();
            tower.stats.target = Tower.colorAim.Blue;
            Target.text = tower.stats.target.ToString();
            tower.ColorChange();
        }
    }
}


