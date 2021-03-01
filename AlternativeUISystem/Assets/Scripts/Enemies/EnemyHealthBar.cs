using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    Enemy s;
    Transform child;
    // Start is called before the first frame update
    void Start()
    {
        
        s = GetComponentInParent<Enemy>();
        child = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        child.localScale = new Vector3(0.25f * s.health / s.maxhealth, child.localScale.y, child.localScale.z);
    }
}
