using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    [SerializeField] float fadeDuration = 1.5f;

    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip menuMusic;

    AudioSource musicSource;
    Coroutine fadeRoutine;

    void Awake()
    {
        if (FindObjectsOfType<MusicScript>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        musicSource = GetComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0f;
    }

    public void PlayMusic()
    {
        StartFade(1f, true);
    }

    public void StopMusic()
    {
        StartFade(0f, false);
    }

    void StartFade(float targetVolume, bool playIfNeeded)
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeRoutine(targetVolume, playIfNeeded));
    }

    IEnumerator FadeRoutine(float targetVolume, bool playIfNeeded)
    {
        if (FindObjectOfType<SceneHandler>().GetCurrentSceneIndex() == 0)
            musicSource.clip = menuMusic;
        else
            musicSource.clip = gameMusic;

        if (playIfNeeded && !musicSource.isPlaying)
            musicSource.Play();

        float startVolume = musicSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeDuration);
            yield return null;
        }

        musicSource.volume = targetVolume;

        if (targetVolume == 0f)
            musicSource.Stop();

        fadeRoutine = null;
    }
}
