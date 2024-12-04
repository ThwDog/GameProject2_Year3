using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundPlay : MonoBehaviour , Ipauseable
{
    [SerializeField] string bgSound;
    [SerializeField] bool playOnStart = true;

    public void pause()
    {
        SoundManager.instance.PauseMusic();
    }

    public void resume()
    {
        SoundManager.instance.ResumeMusic();
    }

    private void Start() {
        SoundManager.instance.StopAllMusic();
        if(playOnStart) {
            // Debug.Log("Play Sound");
            SoundManager.instance.PlayMusic(bgSound);
        }
    }

    public void playNormal(){
        SoundManager.instance.StopAllMusic();
        SoundManager.instance.PlayMusic(bgSound);
    }

    public void changeSound(string sound){
        SoundManager.instance.StopMusic(bgSound);
        bgSound = sound;
        SoundManager.instance.PlayMusic(sound);
    }

    // private void Update() {
    //     if(GameManager.instance.paused){
    //         pause();
    //     }
    //     else if(!SoundManager.instance.checkMusic()){
    //         resume();
    //     }
    // }
}
