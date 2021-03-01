using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGreen : Tower
{
    private new void Awake()
    {
        base.Awake();
        // Initialize Stats (Value, max level, increment by, base price)
        stats.fireRate = new Stat(1f, 5, -0.05f, 50, this);
        stats.range = new Stat(5000f, 0, 1, 75, this);
        stats.damage = new Stat(5f, 5, 2.5f, 100, this);
    }
    private new void Start()
    {
        base.Start();
        s = "Green Tower";
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
            RaycastHit2D hit = Physics2D.Raycast(transform.position, normalVector, Mathf.Infinity, 1 << 11);
            if (hit.collider)
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeInstantDamage(stats.damage.value);
            }
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
