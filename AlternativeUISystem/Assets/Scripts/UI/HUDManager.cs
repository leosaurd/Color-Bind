using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{

    // References to UI elements on the canvas

    [SerializeField] TextMeshProUGUI hudHealth;
    [SerializeField] TextMeshProUGUI hudMoney;
    [SerializeField] TextMeshProUGUI currentWave;
    [SerializeField] TextMeshProUGUI gameOverTxt;
    // Health value currently displayed
    public float health = 100;
    public float money = 20;

    public int initwave = 0;

    public static HUDManager singleton;

    // Use this for initialization
    void Awake()
    {
        singleton = this;

        gameOverTxt.text = "Press 'S' to start!";


        //health = currentPlayerHP;
        //money = startingMoney;
        // Set the starting health value for display
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameOverTxt.gameObject.SetActive(false);
            initwave = 1;
        }
        // Display the score
        hudHealth.text = "Lives: " + health;
        hudMoney.text = "Money: " + money;
        currentWave.text = "Wave: " + (Waves.singleton.currentWave + initwave) + "/" + Waves.singleton.waves.Length;

        if (Waves.singleton.currentWave == Waves.singleton.waves.Length)
        {
            initwave = 0;
        }

        if (health <= 0)
        {
            gameOverTxt.text = "Game Over! Press S to restart!";
            gameOverTxt.gameObject.SetActive(true);
            Time.timeScale = 0;

            if (Input.GetKeyDown(KeyCode.S))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1;
            }
        }

    }
}