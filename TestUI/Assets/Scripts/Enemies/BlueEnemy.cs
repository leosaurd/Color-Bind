using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : Enemy
{
    private void Awake()
    {
        enemyColor = Tower.colorAim.Blue;
    }

    
}
