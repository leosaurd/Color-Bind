using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerH : Tower
{
    public override void Update()
    {
        if (gameObject.layer == 8)
        {
            Fire();
        }
    }
    public override void Fire()
    {
        List<GameObject> inRange = new List<GameObject>();
        GameObject[] towers = CreateTower.singleton.allTowers.ToArray();


        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i] != null)
            {
                Tower comp = towers[i].GetComponent<Tower>();
                if (comp.stats.buffed && comp.GetComponent<TowerH>() == null)
                {
                    comp.stats.damage /= stats.damage;
                    comp.stats.buffed = false;
                }
            }
        }

        for (int i = 0; i < towers.Length; i++)
        {

            if (towers[i] != null)
            {
                float distance = Vector3.Distance(transform.position, towers[i].transform.position) - 0.1f;
                if (distance < stats.range)
                {
                    inRange.Add(towers[i]);
                }
                
            }

        }



        towers = inRange.ToArray();
        for (int i = 0; i < towers.Length; i++)
        {
            Tower comp = towers[i].GetComponent<Tower>();
            if (comp.stats.target == stats.target && !comp.stats.buffed && comp.GetComponent<TowerH>() == null && stats.target == comp.stats.target)
            {
                comp.stats.damage *= stats.damage;
                comp.stats.buffed = true;
            }
        }
    }

    public void OnDestroy()
    {
        GameObject[] towers = CreateTower.singleton.allTowers.ToArray();
        for (int i = 0; i < towers.Length; i++)
        {
            if (towers[i] != null)
            {
                Tower comp = towers[i].GetComponent<Tower>();
                if (comp.stats.buffed && comp.GetComponent<TowerH>() == null)
                {
                    comp.stats.damage /= stats.damage;
                    comp.stats.buffed = false;
                }
            }
        }

    }
}
