using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FireworksParticleSoundSystem : MonoBehaviour
{
    private ParticleSystem  _parentParticleSystem;

    private int _currentNumberOfParticles = 0;
    [SerializeField] float soundDelay;

    [SerializeField] private string BornSounds;
    [SerializeField] private string DieSounds;

    private void OnEnable()
    {
        _parentParticleSystem = this.GetComponent<ParticleSystem>();
        if(_parentParticleSystem == null)
            Debug.LogError("Missing ParticleSystem!", this);
    }

    void Update()
    {
        var amount = Mathf.Abs(_currentNumberOfParticles - _parentParticleSystem.particleCount);

        // Debug.Log("particleCount"+_parentParticleSystem.particleCount);
        // Debug.Log("amount"+amount);

        if (_parentParticleSystem.particleCount < _currentNumberOfParticles) 
        { 
            StartCoroutine(PlaySound(DieSounds, amount));
        } 

        if (_parentParticleSystem.particleCount > _currentNumberOfParticles) 
        { 
            StartCoroutine(PlaySound(BornSounds, amount));
        } 

        _currentNumberOfParticles = _parentParticleSystem.particleCount;
    }

    private IEnumerator PlaySound(string clip, int amount)
    {

        for (int i = 0; i < amount; i++)
        {

            StartCoroutine(PlaySound(clip, soundDelay));
            
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator PlaySound(string sound, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

        SoundManager.instance.PlaySfx(sound);
    }
}