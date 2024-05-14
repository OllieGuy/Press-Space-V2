using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class AudioManagerNew : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private AudioClip currentClip;

    bool isPlayingReal = false;
    AudioClip lastRealClip = null;

    Coroutine playingRealCoroutine;
    Coroutine interruptCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    public float PlayClip(string name, bool isInterrupt)
    {
        //name = "C1_SFX_Bark";
        AudioClip clip = Array.Find(audioClips, sound => sound.name == name);

        if (isInterrupt)
        {
            if (interruptCoroutine != null) StopCoroutine(interruptCoroutine);
            interruptCoroutine = StartCoroutine(InterruptThenResume(clip));
            return -1;
        }
        else
        {
            currentClip = clip;
            audioSource.clip = currentClip;
            audioSource.Play();

            lastRealClip = clip;
            playingRealCoroutine = StartCoroutine(PlayingReal(clip));
            return clip.length;
        }
    }

    public bool CurrentlyPlaying()
    {
        if (interruptCoroutine != null || playingRealCoroutine != null) return true;
        else return false;
    }

    IEnumerator InterruptThenResume(AudioClip interruptClip)
    {
        StopCoroutine(playingRealCoroutine);
        audioSource.Stop();
        audioSource.clip = interruptClip;
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length + 0.5f);

        audioSource.clip = lastRealClip;
        audioSource.Play();
        StartCoroutine(PlayingReal(lastRealClip));

        interruptCoroutine = null;
    }
    IEnumerator PlayingReal(AudioClip realClip)
    {
        isPlayingReal = true;

        yield return new WaitForSeconds(audioSource.clip.length + 0.5f);

        isPlayingReal = false;
        playingRealCoroutine = null;
    }
}