using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public float dmg;
    [HideInInspector]
    public float pierce;
    [HideInInspector]
    public float pierced;
    public float lifespan;
    [HideInInspector]
    public float shotSpeed;

    // TODO: add colour value (store tower colour) and make dmg a multiplier(Check enemy)
    [HideInInspector]
    public Color colour;

    //private BoxCollider2D boxy;
    public Tower.colorAim canHit;
    public void Start()
    {
        //Math stuff to accommodate the box collider offset for projectiles?
        //problem is due to instantiation; if bullet speed is too big, it might "collide" with stuff behind its aim._.
        //Current workaround: Decrease Fixed Timestep in Project Settings>Time>Fixed Timestep(Topmost value)

        /*boxy = GetComponent<BoxCollider2D>();
        float xVal = (shotSpeed / 50f);
        boxy.size = new Vector2(xVal * 3, .16f);
        boxy.offset = new Vector2(xVal, 0);*/

        //Probably not needed, this just sets the color of the projectile sprite to the tower OR the configured color in the scripts
        GetComponent<SpriteRenderer>().color = colour;
        StartCoroutine(killProj());
    }


    public void FixedUpdate()
    {
        //cleanup if offscreen.
        if (!Utility.isVisible(GetComponent<Renderer>(), Camera.main))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator killProj()
    {
        //self explanatory; waits for lifespan to be over before destroying it.
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
