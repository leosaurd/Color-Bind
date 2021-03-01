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
    public void Start()
    {
        StartCoroutine(killProj());
    }


    public void FixedUpdate()
    {
        if (!Utility.isVisible(GetComponent<Renderer>(), Camera.main))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator killProj()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
