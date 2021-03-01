using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public static float vol = 1;
    Slider slidingthing;
    // Start is called before the first frame update
    void Start()
    {
        slidingthing = gameObject.GetComponent<Slider>();
        slidingthing.value = vol;
    }

    // Update is called once per frame
    void Update()
    {
        vol = slidingthing.value;
    }
}
