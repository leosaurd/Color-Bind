using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    public Slider saturation;
    // Start is called before the first frame update
    public Image fill;
    private void Awake()
    {
        saturation = GetComponent<Slider>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        saturation.value = HUDManager.singleton.saturation;
        fill.color = Color.Lerp(Color.red, Color.green, saturation.value);
    }
}
