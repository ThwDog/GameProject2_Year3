using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour , Ipauseable
{
    [Header("Setting")]
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuUI;
    [Header("Sound")]
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    void Start()
    {
        defaultVolume();
    }

    public void pauseMenu(){
        if(!GameManager.instance.paused) {
            pause();
        }
        else resume();
    }


    public void pause(){
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

    public void updateVolume(){
        music.volume = musicVolumeSlider.value ;
        sfx.volume = sfxVolumeSlider.value;
    }

    private void defaultVolume(){
        musicVolumeSlider.value = music.volume;
        sfxVolumeSlider.value = sfx.volume;
    }
}
