using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectColor : MonoBehaviour
{
    const int MAXVAL = 9;

    public Sprite[] coloredMap = new Sprite[MAXVAL];

    public GameObject[] maps = new GameObject[MAXVAL];

    public static bool[] clearedmap = new bool[MAXVAL];


    private void Start()
    {
        for(int i = 0; i < MAXVAL; i++)
        {
            if (clearedmap[i])
            {
                maps[i].GetComponent<Image>().sprite = coloredMap[i];
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
