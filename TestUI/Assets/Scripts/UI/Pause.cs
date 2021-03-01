using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public bool canActivate = true;
    static public Pause singleton;
    //this is the damn PauseMenuButton at the top of the canvas
    public Button ResumeButton;
    public Button TowerMenuButton;

    private void Awake()
    {
        singleton = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canActivate)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pausing();
            }
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);

        ResumeButton.gameObject.SetActive(true);
        TowerMenuButton.gameObject.SetActive(true);
        HUDManager.singleton.towerMenu.SetActive(true);
        HUDManager.singleton.upgradeMenu.SetActive(true);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Pausing()
    {
        ResumeButton.gameObject.SetActive(false);
        TowerMenuButton.gameObject.SetActive(false);
        HUDManager.singleton.towerMenu.SetActive(false);
        HUDManager.singleton.upgradeMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        //Dont forget to set timescale back to normal on scene change.
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
