using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRed : Tower
{
    private new void Awake()
    {
        base.Awake();
        // Initialize Stats (Value, max level, increment by, base price)
        stats.fireRate = new Stat(0.5f, 10, -0.025f, 50, this);
        stats.range = new Stat(5f, 5, 1, 75, this);
        stats.damage = new Stat(2f, 5, 2, 100, this);
    }
    private new void Start()
    {
        base.Start();
        s = "Red Tower";
    }
    // Update is called once per frame
    void Update()
    {
        FindTarget();
        if(enemy != null && canFire)
        {
            //Debug.Log(enemy);
            Fire();
        }
    }

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
