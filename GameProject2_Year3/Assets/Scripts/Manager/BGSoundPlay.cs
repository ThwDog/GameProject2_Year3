using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundPlay : MonoBehaviour , Ipauseable
{
    [SerializeField] string bgSound;

    public void pause()
    {
        SoundManager.instance.PauseMusic();
    }

    public void resume()
    {
        SoundManager.instance.ResumeMusic();
    }

    private void Start() {
        SoundManager.instance.PlayMusic(bgSound);
    }

    public void changeSound(string sound){
        SoundManager.instance.StopMusic(bgSound);
        bgSound = sound;
        SoundManager.instance.PlayMusic(sound);
    }

    private void Update() {
        if(GameManager.instance.paused){
            pause();
        }
        else if(!SoundManager.instance.checkMusic()){
            resume();
        }
    }
}
