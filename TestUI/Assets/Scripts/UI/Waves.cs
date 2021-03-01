using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Waves : MonoBehaviour
{
    public Wave[] waves;
    public List<GameObject> allEnemies = new List<GameObject>();
    public int currentWave = 0;
    public bool canStart = true;
    public static Waves singleton;
    public GameObject waveController;
    public GameObject shotHolder;

    public Button b;

    public int totalEnems;
    private void Awake()
    {
        singleton = this;
        for(int i = 0; i < waves.Length; i++)
        {
            for(int j = 0; j < waves[i].enemies.Length; j++)
            {
                totalEnems += waves[i].enemies[j].numEnemy;
            }
        }
        Debug.Log(totalEnems);
    }

    public void SendWave()
    {
        if (Pause.singleton.GameIsPaused)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
        //If canStart is true
        if (canStart)
        {
            canStart = false;
            HUDManager.singleton.wavesRunning = true;
            //Ensure that the text is disabled
            b.gameObject.SetActive(false);
            HUDManager.singleton.routeStartIndex = currentWave % Path.instance.routes; // update index in array of node of next start point by wave
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
                if(Time.timeScale == 0)
                {
                    yield return new WaitForFixedUpdate();
                }
                GameObject ph = Instantiate(waves[currentWave].enemies[i].enemy, Path.instance.path[Path.instance.routeStart[HUDManager.singleton.routeStartIndex]].transform.position, Quaternion.identity);
                //should start at node of next start point with each consecutive wave
                ph.transform.parent = waveController.transform;
                allEnemies.Add(ph);
                yield return new WaitForSeconds(waves[currentWave].enemies[i].delayCreation);
            }
            yield return new WaitForSeconds(waves[currentWave].enemies[i].delayWave);
        }
        currentWave++;
        if (waves.Length != currentWave && HUDManager.singleton.health > 0)
        {
            canStart = true;
            b.gameObject.SetActive(true);
        }
        else
            b.gameObject.SetActive(false);

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
        public float delayCreation;
        public float delayWave;
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