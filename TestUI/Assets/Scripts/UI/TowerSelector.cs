﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSelector : MonoBehaviour
{

    public Tower tower;
    // Start is called before the first frame update
    void Awake()
    {
        tower = GetComponent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && gameObject.layer == 8 && TowerPlacer.singleton == null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, 2, 1<<8);

            if(hit.collider != null && hit.collider.gameObject == gameObject && tower.isSelected != true)
            {
                tower.isSelected = true;
                UpgradeMenu.singleton.tower = tower;
                GetTowerStats.singleton.getStats();
                ButtonStatUpgrade.singleton.UpdateText();
                tower.setRangeVisible(true);
                if (HUDManager.singleton.tut)
                {
                    TutorialText.singleton.ChangeText();
                }

            }
            else
            {
                if (IsPointerOverUIObject()) return;
                tower.isSelected = false;
                tower.setRangeVisible(false);
            }
            /*if (Vector3.Distance(mousePosition, transform.position) < (GetComponent<Tower>().stats.size * 2))
            {
                Debug.Log(mousePosition);
            }*/
        }
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}