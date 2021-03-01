using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public static UpgradeMenu singleton;
    public Tower tower;


    private void Awake()
    {
        singleton = this;
    }

}
