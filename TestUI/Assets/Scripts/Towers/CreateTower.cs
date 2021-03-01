using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTower : MonoBehaviour
{
    // Update is called once per frame


    public GameObject towerA;
    public GameObject towerB;
    public GameObject towerC;
    public GameObject towerD;
    public GameObject towerE;
    public GameObject towerF;
    public GameObject towerG;
    public GameObject towerH;

    public List<GameObject> allTowers = new List<GameObject>();

    Tower selectedTower;

    public static CreateTower singleton;

    private void Awake()
    {
        singleton = this;
    }

    private void Update()
    {
        if (!HUDManager.singleton.tut)
        {
            if (!TowerPlacer.singleton)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Create(towerA);
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Create(towerB);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Create(towerC);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Create(towerD);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Create(towerE);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    Create(towerF);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    Create(towerG);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Create(towerH);
                }
            }
        }
    }

    public void Create(GameObject tower)
    {
        if (UpgradeMenu.singleton.tower)
        {
            selectedTower = UpgradeMenu.singleton.tower;
            selectedTower.setRangeVisible(false);
            selectedTower.isSelected = false;
        }
        if (HUDManager.singleton.tut)
        {
            TutorialText.singleton.ChangeText();
        }
        if (tower != null)
        {
            GameObject twr = Instantiate(tower, Input.mousePosition, Quaternion.identity);
            allTowers.Add(twr);
        }
    }

}
