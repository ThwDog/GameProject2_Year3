using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour , Ipauseable
{
    [Header("Setting")]
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuUI;
    private bool paused = false;

    public void pauseMenu(){
        if(!paused) pause();
        else resume();
    }


    public void pause()
    {
        paused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void resume()
    {
        paused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
}
