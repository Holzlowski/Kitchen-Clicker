using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Singletons
{
    public class SoundEffectManager : Singleton<SoundEffectManager>
    {
        [SerializeField] private List<AudioClip> splashSoundEffects;
        [SerializeField] private List<AudioClip> completeSoundEffects;
        private AudioSource managerAudioSource;

        private void Start()
        {
            Instance.managerAudioSource = GetComponent<AudioSource>();
        }

        private AudioClip getRandomSoundEffect(List<AudioClip> sounds)
        {
            if (sounds.Count > 0)
            {
                return sounds.Random();
            }
            else
            {
                return null;
            }
        }

        public static void getrandomSplashSoundEffect()
        {
            Instance.managerAudioSource.clip = Instance.getRandomSoundEffect(Instance.splashSoundEffects);
            Instance.managerAudioSource.Play();
        }

        public static void getrandomCompleteSoundEffect()
        {
            Instance.managerAudioSource.clip = Instance.getRandomSoundEffect(Instance.completeSoundEffects);
            Instance.managerAudioSource.Play();
        }
    }
}

