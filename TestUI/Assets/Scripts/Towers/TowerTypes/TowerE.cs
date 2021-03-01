using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerE : Tower
{
    //Permanently slows enemies based on damage.
    //Should make it temporary but strapped for time, TODO.

    public override void Fire()
    {
        List<GameObject> inRange = new List<GameObject>();
        GameObject[] enemies = Waves.singleton.allEnemies.ToArray();
        StartCoroutine(FireRate());
        for (int i = 0; i < enemies.Length; i++)
        {

            float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distance < stats.range)
            {
                inRange.Add(enemies[i]);
            }

        }
        enemies = inRange.ToArray();
        //only damage enemies who aren't green.
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy comp = enemies[i].GetComponent<Enemy>();
            if (comp.enemyColor == stats.target)
            {
                if (!comp.debuffed)
                {
                    comp.speed *= stats.damage;
                    comp.debuffed = true;
                    StartCoroutine(comp.slowDur(1f));
                }
            }
        }
    }
}

