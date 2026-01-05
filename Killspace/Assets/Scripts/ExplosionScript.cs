using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    AudioSource source;

    [SerializeField] AudioClip[] sounds;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        PlayRandomSFX();
    }

    void PlayRandomSFX()
    {
        source.clip = sounds[Random.Range(0, sounds.Length)];
        source.Play();
    }
}
