using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour , Ipauseable
{
    [Header("Setting")]
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuUI;

    public void pauseMenu(){
        if(!GameManager.instance.paused) pause();
        else resume();
    }


    public void pause()
    {
        GameManager.instance.paused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void resume()
    {
        GameManager.instance.paused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
}
