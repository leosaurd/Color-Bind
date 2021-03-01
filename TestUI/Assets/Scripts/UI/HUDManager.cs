using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class HUDManager : MonoBehaviour
{

    // References to UI elements on the canvas

    [SerializeField] TextMeshProUGUI hudHealth;
    [SerializeField] TextMeshProUGUI hudMoney;
    [SerializeField] TextMeshProUGUI currentWave;
    [SerializeField] public TextMeshProUGUI gameOverTxt;
    [SerializeField] TextMeshProUGUI StartingTxt;
    [SerializeField] TextMeshProUGUI EndPtTxt;
    // Health value currently displayed
    public float health = 100;
    public float money = 20;
    public int beacons; //number of beacons - should only change upon new scene load
    public int litBeacons; //number of lit beacons - should reset upon replay of level
    private int initwave = 0;
    private float timeBuffer = 0;
    public int routeStartIndex; //stores index of start of current route, determined by wave
    public bool tut;
    public bool wavesRunning = false;
    public bool getCash;

    public Button but;
    public Button but2;

    public GameObject winMenu;
    public GameObject towerMenu;
    public GameObject upgradeMenu;

    public static HUDManager singleton;

    public float saturation = 0;
    public float incBy;

    // Use this for initialization
    void Awake()
    {
        Time.timeScale = 1;
        singleton = this;
        //health = currentPlayerHP;
        //money = startingMoney;
        // Set the starting health value for display
    }

    private void Start()
    { 
        winMenu.SetActive(false);
        Pause.singleton.canActivate = true;
        incBy = 1f / Waves.singleton.totalEnems;
    }
    // Update is called once per frame
    void Update()
    {
        if (!Waves.singleton.canStart && Waves.singleton.currentWave < Waves.singleton.waves.Length)
        {
            initwave = 1;
            //money on wave "complete"
            getCash = true;
        } else
        {
            initwave = 0;
            
        }

        if (Waves.singleton.allEnemies.Count == 0) {
            wavesRunning = false;
        }

        if (health <= 0)
        {
            gameOverTxt.text = "Game Over!";
            but.gameObject.SetActive(false);
            but2.gameObject.SetActive(true);
            Waves.singleton.b.gameObject.SetActive(false);
            winMenu.SetActive(true);
            towerMenu.SetActive(false);
            upgradeMenu.SetActive(false);
            Time.timeScale = 0;
            health = 0;
        }
        else if ((Waves.singleton.allEnemies.Count == 0 && Waves.singleton.currentWave >= Waves.singleton.waves.Length) || litBeacons == beacons)

        {
            Time.timeScale = 0;
            but.gameObject.SetActive(true);
            but2.gameObject.SetActive(false);
            Pause.singleton.canActivate = false;
            //Can set continue menu here to be active? Under Canvas - so we can click continue to go to next scene.
            winMenu.SetActive(true);
            //if the level scene is cleared, set it to true
            LevelSelectColor.clearedmap[SceneManager.GetActiveScene().buildIndex - 1] = true;
            towerMenu.SetActive(false);
            upgradeMenu.SetActive(false);
        }
        //Money on wave complete
        if(Waves.singleton.allEnemies.Count == 0 && getCash)
        {
            if (timeBuffer >= 1f) {
                money += 500f;
                getCash = false;
                timeBuffer = 0;
            } else {
                timeBuffer += Time.deltaTime;
            }
        }


        // Display the score
        hudHealth.text = "Light: " + health;
        hudMoney.text = ""+ money;
        currentWave.text = "Wave: " + (Waves.singleton.currentWave + initwave) + "/" + Waves.singleton.waves.Length;

        //If there are no enemies remaining and current wave is more than or equal to max waves OR all beacons have been lit
        

    }
}