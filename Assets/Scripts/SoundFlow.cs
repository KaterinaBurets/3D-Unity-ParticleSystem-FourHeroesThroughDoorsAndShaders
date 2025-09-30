using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFlow : MonoBehaviour
{
    [SerializeField] AudioSource _audioSourse;
    [SerializeField] float _duration;
    [SerializeField] float _targetVolume;

    private void Start()
    {
        _audioSourse.Play();
        StartCoroutine(StartFade(_audioSourse, _duration, _targetVolume));
    }
    private static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
