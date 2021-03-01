using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [HideInInspector]
    public bool fix = true;
    public static TowerPlacer singleton;
    // Start is called before the first frame update
    // Update is called once per frame
    public Tower tower;

    private void Awake()
    {
        singleton = this;
        tower = GetComponent<Tower>();
    }
    void Update()
    {
        if (fix)
        {
            Pause.singleton.ResumeButton.gameObject.SetActive(false);
            Pause.singleton.TowerMenuButton.gameObject.SetActive(false);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
        if (!canPlaceTower())
        {
            //Color newColor = new Color(1, 1, 1, 1);
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (Input.GetMouseButtonUp(0) && canPlaceTower())
        {
            
            HUDManager.singleton.money -= tower.stats.price;
            fix = false;
            this.gameObject.layer = 8;
            tower.isSelected = false;
            tower.setRangeVisible(false);
            Destroy(this);
            if (HUDManager.singleton.tut)
            {
                TutorialText.singleton.ChangeText();
            }
            Pause.singleton.ResumeButton.gameObject.SetActive(true);
            Pause.singleton.TowerMenuButton.gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonUp(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            CreateTower.singleton.allTowers.RemoveAt(CreateTower.singleton.allTowers.Count - 1);
            Destroy(gameObject);
            Pause.singleton.ResumeButton.gameObject.SetActive(true);
            Pause.singleton.TowerMenuButton.gameObject.SetActive(true);
        }


    }

    private bool canPlaceTower()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.1f, Vector3.forward, 0.1f, 1<<8);
        RaycastHit2D hit2 = Physics2D.CircleCast(transform.position, 0.1f, Vector3.forward, 0.1f, 1<<9);
        //Debug.Log(hit2.collider);
        if (hit.collider != null || hit2.collider != null)
        {
            return false;
        }
        else
        {
            if (HUDManager.singleton.money >= tower.stats.price)
            {
                return true;
            } else
            {
                return false;
            }

        }
    }
}
