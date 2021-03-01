using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDemo : Enemy
{
    public PathFollower pf;

    private void Awake()
    {
        pf = GetComponent<PathFollower>();
    }
    public new void Update()
    {
        float angle = Tower.GetAngle(transform.position + Vector3.left, transform.position, pf.nextNode.transform.position);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (HUDManager.singleton.getCash)
        {
            Destroy(gameObject);
        }
    }
    public override void DoDamage()
    {
        //recreate/reload object
        
        GameObject ph = Instantiate(gameObject);
        ph.name = "TestDummy";
        ph.GetComponent<PathFollower>().reachedEnd = false;
        Destroy(gameObject);
    }

    public override void OnDeath()
    {
        
    }
}
