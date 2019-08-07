using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ocarina
{
    public class FadeSound : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private float fadingTime;

        public void Play()
        {
            StopAllCoroutines();
            audioSource.volume = 1f;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void Stop()
        {
            StartCoroutine(FadeAudioSourceSoundCoroutine());
        }

        private IEnumerator FadeAudioSourceSoundCoroutine()
        {
            var startingTime = Time.realtimeSinceStartup;
            while(Time.realtimeSinceStartup < startingTime + fadingTime)
            {
                var step = Time.deltaTime / fadingTime;
                audioSource.volume -= step;
                yield return null;
            }
            audioSource.loop = false;
            audioSource.Stop();
            audioSource.volume = 1f;
        }
    }
}
