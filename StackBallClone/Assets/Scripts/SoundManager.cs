using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;
    public bool soundOn { get; private set; } = true;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SoundOnOff()
    {
        soundOn = !soundOn;
    }
    public void PlaySoundFX(AudioClip clip,float volume)
    {
        if (soundOn)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}
