using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerD : Tower
{
    //Ray Tower/Star
    public SpriteRenderer sr;
    public float FoV;
    Vector3 relativeVector;
    float dist;
    Vector3 normalVector;
    private new void Start()
    {
        base.Start();
        sr.gameObject.SetActive(false);
    }
    public override void Update()
    {
        base.Update();
        if (enemy != null)
        {
            //Rotates it based on the angle between 3 positions, above the tower, the tower, and the enemy.
            sr.transform.rotation = Quaternion.AngleAxis(GetAngle(Vector3.up + sr.transform.position, sr.transform.position, enemy.transform.position), Vector3.forward);
            sr.gameObject.SetActive(true);
        }
        else
        {
            sr.gameObject.SetActive(false);
        }
    }
    public override void Fire()
    {
        relativeVector = enemy.transform.position - transform.position;
        dist = relativeVector.magnitude;
        normalVector = relativeVector / dist;
        Vector3 middleRayDir = normalVector;
        Vector3 direction = middleRayDir;

        float a = 1f;
        float b = a;
        float c = 0;


        StartCoroutine(FireRate());
        List<GameObject> enemies = new List<GameObject>();
        while (c <= FoV)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, stats.range, 1 << 11);
            for (int i = 0; i < hit.Length; i++)
            {
                if (!enemies.Contains(hit[i].collider.gameObject))
                {
                    enemies.Add(hit[i].collider.gameObject);
                    //If the target enemy has the same "target" value as the tower.
                    if (hit[i].collider.gameObject.GetComponent<Enemy>().enemyColor == stats.target)
                    {
                        hit[i].collider.gameObject.GetComponent<Enemy>().TakeInstantDamage(stats.damage);
                    }
                }
            }
            Debug.DrawRay(transform.position, direction * stats.range, Color.green, stats.fireRate);

            Quaternion rotation = Quaternion.AngleAxis(b, Vector3.forward);
            direction = rotation * middleRayDir;

            b *= -1;
            b += (b > 0) ? a : 0;
            c++;
        }
    }
}


