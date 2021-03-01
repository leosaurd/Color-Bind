using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlue : Tower
{
    private new void Awake()
    {
        base.Awake();
        // Initialize Stats (Value, max level, increment by, base price)
        stats.fireRate = new Stat(0.5f, 10, -0.025f, 50, this);
        stats.range = new Stat(5f, 5, 1, 75, this);
        stats.damage = new Stat(2f, 5, 2, 100, this);
        stats.pierce = new Stat(1f, 5, 1, 100, this);
    }
    private new void Start()
    {
        base.Start();
        s = "Blue Tower";
    }

    public override void Fire()
    {
        if (gameObject.layer == 8 && Time.timeScale != 0)
        {
            Vector3 relativeVector = enemy.transform.position - transform.position;
            float distance = relativeVector.magnitude;
            Vector3 normalVector = relativeVector / distance;
            float hyp = normalVector.magnitude;
            float adj = new Vector3(normalVector.x, 0, normalVector.z).magnitude;
            float angle = Mathf.Acos(adj / hyp) * 180 / Mathf.PI;



            if (relativeVector.y < 0) angle *= -1;
            if (relativeVector.x < 0) angle *= -1;
            GameObject localshot = Instantiate(shot, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            localshot.GetComponent<SpeedProjectile>().dmg = stats.damage.value;
            localshot.GetComponent<SpeedProjectile>().pierce = stats.pierce.value;
            Rigidbody2D rb = localshot.GetComponent<Rigidbody2D>();
            rb.velocity = normalVector * stats.bulletspeed;
            StartCoroutine(FireRate());
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        if (enemy != null && canFire)
        {
            //Debug.Log(enemy);
            Fire();
        }
    }


}
