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
    public AudioClip[] sfxClips;

    [SerializeField] private AudioSource narrationSource;
    [SerializeField] private AudioSource sfxSource;
    private AudioClip currentClip;

    bool isPlayingReal = false;
    AudioClip lastRealClip = null;

    Coroutine playingRealCoroutine;
    Coroutine interruptCoroutine;

    private void Awake()
    {
        if (narrationSource != null)
        {
            narrationSource.playOnAwake = false;
            narrationSource.loop = false;
        }
    }

    public float PlayClip(string name, bool isInterrupt)
    {
        AudioClip clip = Array.Find(audioClips, sound => sound.name == name);

        if (isInterrupt)
        {
            if (interruptCoroutine != null) StopCoroutine(interruptCoroutine);
            interruptCoroutine = StartCoroutine(InterruptThenResume(clip));
            return clip.length;
        }
        else
        {
            currentClip = clip;
            narrationSource.clip = currentClip;
            narrationSource.Play();

            lastRealClip = clip;
            playingRealCoroutine = StartCoroutine(PlayingReal(clip));
            return clip.length;
        }
    }

    public void PlaySFX(string name)
    {
        Debug.Log("gg4tg4g");
        AudioClip clip = Array.Find(sfxClips, sound => sound.name == name);


        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public bool CurrentlyPlaying()
    {
        if (interruptCoroutine != null || playingRealCoroutine != null) return true;
        else return false;
    }

    IEnumerator InterruptThenResume(AudioClip interruptClip)
    {
        StopCoroutine(playingRealCoroutine);
        narrationSource.Stop();
        narrationSource.clip = interruptClip;
        narrationSource.Play();

        yield return new WaitForSeconds(narrationSource.clip.length + 0.5f);

        narrationSource.clip = lastRealClip;
        narrationSource.Play();
        StartCoroutine(PlayingReal(lastRealClip));

        interruptCoroutine = null;
    }
    IEnumerator PlayingReal(AudioClip realClip)
    {
        isPlayingReal = true;

        yield return new WaitForSeconds(narrationSource.clip.length + 0.5f);

        isPlayingReal = false;
        playingRealCoroutine = null;
    }
}