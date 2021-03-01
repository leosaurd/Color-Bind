using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GetTowerStats : MonoBehaviour
{
    public TextMeshProUGUI nametext;
    public static GetTowerStats singleton;
    // Start is called before the first frame update
    void Awake()
    {
        singleton = this;
    }

    public void getStats()
    {
        Tower twr = UpgradeMenu.singleton.tower;
        nametext.text = twr.s;
        nametext.color = twr.stats.colour;//Added some code to make the colour match the selected tower.
    }
}
