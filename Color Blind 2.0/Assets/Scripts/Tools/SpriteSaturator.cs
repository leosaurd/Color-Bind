using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSaturator : MonoBehaviour
{
	// Mostly self explanatory, this script saturated the attached spriteRenderer
	// Based on the saturation value in GameManager
	// The spriteRenderer must have the SaturationShader Material
    public SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

        sr.material.SetFloat("_Saturation", GameManager.singleton.saturation);

    }
}