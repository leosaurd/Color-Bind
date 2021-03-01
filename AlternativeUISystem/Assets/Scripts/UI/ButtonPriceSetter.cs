using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonPriceSetter : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI price = null;
    [HideInInspector]
    public GameObject ph = null;
    void Awake()
    {
        ph = GetComponentInParent<CreateTower>().Tower;
        price = GetComponent<TextMeshProUGUI>();
        price.text = "$" + ph.GetComponent<Tower>().stats.price;// ph.stats.price;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
