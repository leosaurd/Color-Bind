using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerB : Tower
{
    public SpriteRenderer sr;
    private bool flip = false;
    private new void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    private new void Update()
    {
        base.Update();
        //Pulsating effect for the tower
        if (flip)
        {
            sr.color += new Color(0, 0, 0, 1f / 255);
        }
        else
        {
            sr.color -= new Color(0, 0, 0, 1f / 255);
        }
        if(sr.color.a <= 0 || sr.color.a >= 1)
        {
            flip = !flip;
        }
    }

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
                comp.TakeInstantDamage(stats.damage);
        }
    }

    //override to enable the background component to change as well.
    public override void ColorChange()
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        switch (stats.target)
        {
            case colorAim.Blue:
                s.color = Color.blue;
                break;
            case colorAim.Red:
                s.color = Color.red;
                break;
            case colorAim.Green:
                s.color = Color.green;
                break;
            default:
                break;
        }
        stats.colour = s.color;
        sr.color = s.color;
    }

}
