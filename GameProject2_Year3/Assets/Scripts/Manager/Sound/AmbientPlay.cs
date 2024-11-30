using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientPlay : MonoBehaviour
{
    public AudioSource ambientSource;
    [SerializeField] private float maxVolume;
    [Header("Random Add sound")]
    [SerializeField] private AudioClip[] sound;
    private bool isPlay;
    private float timeDelay;

    public void play()
    {
        StartCoroutine(Fade(true,this.gameObject.GetComponent<AmbientPlay>(),2f,maxVolume));
        isPlay = true;
        ambientSource.Play();
    }

    public void stop()
    {
        StartCoroutine(Fade(true,this.gameObject.GetComponent<AmbientPlay>(),2f,0));
        isPlay = false;
        ambientSource.Stop();
    }  

    public void setBoolPlay(bool _bool){
        isPlay= _bool;
    }

    // set false to fade out and true to in
    public IEnumerator Fade(bool fadeIn, AmbientPlay Source, float duration, float targetVolume)
    {
        if (!fadeIn)
        {
            double lengthOfSource = (double)Source.ambientSource.clip.samples / Source.ambientSource.clip.frequency; 
            yield return new WaitForSecondsRealtime((float)(lengthOfSource - duration));
        }
        float time = 0f;
        float startVol = Source.ambientSource.volume;
        while (time < duration)
        {
            string fadeSituation = fadeIn ? "fadeIn" : "fadeOut";
            //Debug.Log(fadeSituation);
            time += Time.deltaTime;
            Source.ambientSource.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
            yield return null;
        }
        yield break;
    }

    private IEnumerator playSoundDelay(){
        if(!isPlay) yield break;
        int rnd = Random.Range(0,sound.Length);
        timeDelay = Random.Range(2,5);

        AudioClip _sound = sound[rnd];

        yield return new WaitForSecondsRealtime(timeDelay);
        ambientSource.clip = _sound;

        ambientSource.Play();
        StartCoroutine(playSoundDelay());
    }

    public void playRandomSound(){
        StartCoroutine(Fade(true,this.gameObject.GetComponent<AmbientPlay>(),2f,maxVolume));
        isPlay = true;
        StartCoroutine(playSoundDelay());
    }

}
