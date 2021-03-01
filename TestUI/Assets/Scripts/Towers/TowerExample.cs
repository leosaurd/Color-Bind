using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Remember to extend from Tower class!
public class TowerExample : Tower
{
    private new void Start()
    {
        //Initialise the start from Tower
        base.Start();
        //Setting the name
        s = "Tower Example";
    }

/*    private void Update()
    {
        //Finds the target via Tower Script
        FindTarget();

        //if enemy exists and the tower can fire
        if(enemy != null && canFire)
        {
            //Call the Fire command
            Fire();
        }
    }*/

    //AoE circle
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
        //only damage those with matching target enums
        for(int i = 0; i < enemies.Length; i++)
        {
            Enemy comp = enemies[i].GetComponent<Enemy>();
            if(comp.enemyColor != stats.target)
            comp.TakeInstantDamage(stats.damage);
        }
    }

    //Raycast Example for firing AoE Cone
    /* public override void Fire()
    {
        Vector3 relativeVector = enemy.transform.position - transform.position;
        float dist = relativeVector.magnitude;
        Vector3 normalVector = relativeVector / dist;

        Vector3 middleRayDir = normalVector;
        Vector3 direction = middleRayDir;

        float a = 1f;
        float b = a;
        float c = 0;
        StartCoroutine(FireRate());
        List<GameObject> enemies = new List<GameObject>();
        while (c <= 45)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, stats.range, 1 << 11);
            for(int i = 0; i < hit.Length; i++)
            {
                if (!enemies.Contains(hit[i].collider.gameObject))
                {
                    enemies.Add(hit[i].collider.gameObject);
                    hit[i].collider.gameObject.GetComponent<Enemy>().TakeInstantDamage(stats.damage);
                }
            }
            Debug.DrawRay(transform.position, direction * stats.range, Color.green, stats.fireRate);

            Quaternion rotation = Quaternion.AngleAxis(b, Vector3.forward);
            direction = rotation * middleRayDir;

            b *= -1;
            b += (b > 0) ? a : 0;
            c++;
        }


    }*/

    //Raycast pierce fire mode
    /*public override void Fire()
    {
       if (Time.timeScale != 0)
       {
           Vector3 relativeVector = enemy.transform.position - transform.position;
           float distance = relativeVector.magnitude;
           Vector3 normalVector = relativeVector / distance;

           RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, normalVector, stats.range + 1, 1 << 11);
           StartCoroutine(FireRate());
           Debug.Log(hit.Length);
           Debug.DrawLine(transform.position, normalVector * (stats.range + 1), Color.green, stats.fireRate);
           for(int i = 0; i < stats.pierce && i < hit.Length; i++)
           {

               if (hit[i].collider)
               {
                   hit[i].collider.gameObject.GetComponent<Enemy>().TakeInstantDamage(stats.damage); 
               }

           }
       }
    }*/


    //Instant Hit Example for firing
    /*    public override void Fire()
        {
            //override for instant firing(kinda like a sniper)
            if (gameObject.layer == 8)
            {
                enemy.GetComponent<Enemy>().TakeInstantDamage(stats.damage);
                StartCoroutine(FireRate());
            }
        }*/
}
