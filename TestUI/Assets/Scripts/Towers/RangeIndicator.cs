using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        refrange();
    }

    public void refrange()
    {
        transform.localScale = Vector2.one * GetComponentInParent<Tower>().stats.range;
    }
}
