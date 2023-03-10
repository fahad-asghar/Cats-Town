using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip sound4;
    public AudioClip sound5;
    public AudioClip sound6;
    public AudioClip sound7;
    public AudioClip sound8;
    public AudioClip sound9;


    void Start()
    {
        instance = this;
    }

    public void playSound(AudioClip audio)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = audio;
        source.Play();
        source.loop = false;

        Destroy(source, 3);
        
    }

    public void playLoop(AudioClip audio)
    {
        gameObject.GetComponent<AudioSource>().clip = audio;
        gameObject.GetComponent<AudioSource>().Play();
        gameObject.GetComponent<AudioSource>().loop = true;
    }
}
