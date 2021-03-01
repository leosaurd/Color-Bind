using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonPriceSetter : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI price = null;
    [HideInInspector]
    public GameObject ph = null;
    Button b;
    float p;
    void Start()
    {
        //ph = CreateTower thing

        if (b == null)
            b = GetComponentInParent<Button>();

        if (b.name.Contains("1"))
            ph = CreateTower.singleton.towerA;
        else if (b.name.Contains("2"))
            ph = CreateTower.singleton.towerB;
        else if (b.name.Contains("3"))
            ph = CreateTower.singleton.towerC;
        else if (b.name.Contains("4"))
            ph = CreateTower.singleton.towerD;
        else if (b.name.Contains("5"))
            ph = CreateTower.singleton.towerE;
        else if (b.name.Contains("6"))
            ph = CreateTower.singleton.towerF;
        else if (b.name.Contains("7"))
            ph = CreateTower.singleton.towerG;
        else if (b.name.Contains("8"))
            ph = CreateTower.singleton.towerH;

        price = GetComponent<TextMeshProUGUI>();
        p = ph.GetComponent<Tower>().stats.price;
        price.text = " " + p;// ph.stats.price;
    }

    // Update is called once per frame
    public void Update()
    {
        if(p > HUDManager.singleton.money)
        {
            b.interactable = false;
        } else
        {
            b.interactable = true;
        }
    }
}
