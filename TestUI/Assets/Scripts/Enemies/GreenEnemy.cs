using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : Enemy
{
    private void Awake()
    {

        enemyColor = Tower.colorAim.Green;
    }
}
