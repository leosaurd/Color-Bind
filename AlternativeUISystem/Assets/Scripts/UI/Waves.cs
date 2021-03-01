using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public Wave[] waves;
    public List<GameObject> allEnemies = new List<GameObject>();
    public int currentWave = 0;
    private bool canStart = true;
    public static Waves singleton;
    private void Awake()
    {
        singleton = this;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && canStart)
        {
            currentWave = 0;
            canStart = false;
            StartCoroutine(SpawnWave());
        }
    }


    IEnumerator SpawnWave()
    {
       // SpawnEnemy(GameObject);
       for(int i = 0; i < waves[currentWave].enemies.Length; i++)
        {
            for(int j = 0; j < waves[currentWave].enemies[i].numEnemy; j++)
            {
                GameObject ph = Instantiate(waves[currentWave].enemies[i].enemy, Path.instance.path[0].transform.position, Quaternion.identity);
                allEnemies.Add(ph);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
        }
        currentWave++;
        if (waves.Length != currentWave)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(SpawnWave());
        } else
        {
            canStart = true;
        }
    }


}

[System.Serializable]
public class Wave
{
    public EnemyWaves[] enemies;

    [System.Serializable]
    public struct EnemyWaves
    {
        public GameObject enemy;
        public int numEnemy;
    }
}

/*    void FixedUpdate()
    {
        StartCoroutine(SpawnWave());
    }*/

/*    private void SpawnEnemy(int i)
    {

    }*/


//public int blueEnemies;

//public int greenEnemies;

//2D array benefits - heavily customizable on every level, easily separated
//Non 2D array benefits(Wave by wave) - Consistency? 