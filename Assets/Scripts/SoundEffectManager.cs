using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class SoundEffectManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> splashSoundEffects;
    [SerializeField] private List<AudioClip> completeSoundEffects;
    private AudioSource managerAudioSource;

    private void Start() 
    {
        managerAudioSource = GetComponent<AudioSource>();
    }
    
    private AudioClip getRandomSoundEffect(List<AudioClip> sounds)
    {   
        if(sounds.Count > 0 )
        {
            return sounds.Random();
        }
        else
        {
            return null;
        } 
    }

    public AudioClip getrandomSplashSoundEffect(){
        return getRandomSoundEffect(splashSoundEffects);
    }

    public void getrandomCompleteSoundEffect(){
        managerAudioSource.clip = getRandomSoundEffect(completeSoundEffects);
        managerAudioSource.Play();
    }
}
