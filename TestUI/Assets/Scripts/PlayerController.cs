using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 100;

    public static PlayerController singleton;
    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
