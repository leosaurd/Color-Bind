using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF : Tower
{
    public override void Update()
    {
        if (towerbufficon != null)
        {
            towerbufficon.SetActive(stats.buffed);
        }
        if (gameObject.layer == 8)
        {
            if (canFire)
            {
                Fire();
            }
        }
    }
    public override void Fire()
    {
        if(upgradelevel.unique == 1)
        {
            stats.bulletspeed = 3f;
            stats.pierce = 5;
        }
        if (upgradelevel.unique == 2)
        {
            stats.bulletspeed = 3.5f;
            stats.pierce = 8;
        }
        if (upgradelevel.unique == 3)
        {
            stats.bulletspeed = 4f;
            stats.pierce = 11;
        }
        if (upgradelevel.unique == 4)
        {
            stats.bulletspeed = 4.5f;
            stats.pierce = 14;
        }
        if (upgradelevel.unique == 5)
        {
            stats.bulletspeed = 5f;
            stats.pierce = 20;
        }
        createShot(transform.up, 0, 0);
        createShot(transform.right, 0, 90);
        createShot(-transform.up, 0, 180);
        createShot(-transform.right, 0, 270);
        StartCoroutine(FireRate());
    }
}
