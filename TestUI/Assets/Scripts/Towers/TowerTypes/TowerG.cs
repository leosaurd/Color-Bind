using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerG : Tower
{
    public GameObject mf;
    public override void Fire()
    {
        mf.transform.rotation = Quaternion.AngleAxis(GetAngle(Vector3.up + mf.transform.position, mf.transform.position, enemy.transform.position), Vector3.forward);
        StartCoroutine(FireRate());
        StartCoroutine(muzzleFlash());
        if (upgradelevel.unique >= 0)
        {
            enemy.GetComponent<Enemy>().TakeInstantDamage(stats.damage);
        }
        if (upgradelevel.unique >= 1)
        {
            FindTarget();
            if(enemy != null)
            enemy.GetComponent<Enemy>().TakeInstantDamage(stats.damage);
        }
        if (upgradelevel.unique >= 2)
        {
            FindTarget();
            if (enemy != null)
                enemy.GetComponent<Enemy>().TakeInstantDamage(stats.damage);
        }
        if (upgradelevel.unique >= 3)
        {
            FindTarget();
            if (enemy != null)
                enemy.GetComponent<Enemy>().TakeInstantDamage(stats.damage);
        }
        if (upgradelevel.unique >= 4)
        {
            FindTarget();
            if (enemy != null)
                enemy.GetComponent<Enemy>().TakeInstantDamage(stats.damage);
        }
        if (upgradelevel.unique >= 5)
        {
            FindTarget();
            if (enemy != null)
                enemy.GetComponent<Enemy>().TakeInstantDamage(stats.damage);
        }
        
    }

    IEnumerator muzzleFlash()
    {
        mf.SetActive(true);
        yield return new WaitForEndOfFrame();
        mf.SetActive(false);
    }
}
