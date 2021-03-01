using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerC : Tower
{
    private new void Start()
    {
        base.Start();
    }


    //Example for unique upgrades(in this case based on unique stat)
    public override void Fire()
    {
        Enemy s = enemy.GetComponent<Enemy>();

        //Proof of concept for relative upgrades?
        if (upgradelevel.unique >= 0)
        {
            Vector3 relativeVector = enemy.transform.position - transform.position;
            float distance = relativeVector.magnitude;
            Vector3 normalVector = relativeVector / distance;
            float hyp = normalVector.magnitude;
            float adj = new Vector3(normalVector.x, 0, normalVector.z).magnitude;
            float angle = Mathf.Acos(adj / hyp) * 180 / Mathf.PI;
            if (relativeVector.y < 0) angle *= -1;
            if (relativeVector.x < 0) angle *= -1;
            createShot(normalVector, angle, 0);

            if (upgradelevel.unique >= 1)
            {
                createShot(normalVector, angle, 45);
                createShot(normalVector, angle, -45);
                if (upgradelevel.unique >= 2)
                {
                    createShot(normalVector, angle, 25);
                    createShot(normalVector, angle, -25);

                    if (upgradelevel.unique >= 3)
                    {
                        stats.bulletspeed = 30;
                    }
                }
            }

            StartCoroutine(FireRate());



        }
    }


}
