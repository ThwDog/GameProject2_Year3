using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientPlay : MonoBehaviour
{
    public AudioSource ambientSource;
    [SerializeField] private float maxVolume;

    public void play()
    {
        StartCoroutine(Fade(true,this.gameObject.GetComponent<AmbientPlay>(),2f,maxVolume));
        ambientSource.Play();
    }

    public void stop()
    {
        StartCoroutine(Fade(true,this.gameObject.GetComponent<AmbientPlay>(),2f,0));
        ambientSource.Stop();
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

}
