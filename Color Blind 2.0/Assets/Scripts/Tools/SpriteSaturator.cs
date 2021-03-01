using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSaturator : MonoBehaviour
{
    public SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
      //  sr.material.SetFloat("_Saturation", HUDManager.singleton.saturation);
    }


    void Update()
    {

        sr.material.SetFloat("_Saturation", GameManager.singleton.saturation);

    }
}