using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    static public bool GameIsPaused = true;
    private void Start()
    {
        Time.timeScale = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    void Pausing()
    {
        Time.timeScale = 0;
        GameIsPaused = true;
    }

}
