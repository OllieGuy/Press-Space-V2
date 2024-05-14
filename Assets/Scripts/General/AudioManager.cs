using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    List<Sound> currentlyPlaying = new List<Sound>();
    private IEnumerator coroutineLoop;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public float play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        /*foreach (Sound playingNow in currentlyPlaying)
        {
            Debug.Log("iter");
            if (!playingNow.name.Contains("SFX"))
            {
                playingNow.source.Stop();
                currentlyPlaying.Remove(playingNow);
            }
        }*/
        s.source.Play();
        //StartCoroutine(currentlyPlayingElement(s));
        Debug.Log(name);
        //Debug.Log(currentlyPlaying.Count);
        return s.source.clip.length;
    }

    public float returnLength(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.clip.length;
    }

    public void playOnLoop(string name, float offset)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        coroutineLoop = looper(s, offset);
        StartCoroutine(coroutineLoop);
    }

    public void breakLoop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
        StopCoroutine(coroutineLoop);
    }
    IEnumerator looper(Sound s, float offset)
    {
        float timer = 0;
        float lenthOfClip = s.source.clip.length;
        while (true)
        {
            if (timer > (lenthOfClip - offset))
            {
                s.source.Play();
                timer = 0;
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator currentlyPlayingElement(Sound s)
    {
        currentlyPlaying.Add(s);
        yield return new WaitForSeconds(s.source.clip.length);
        if (currentlyPlaying.Contains(s))
        {
            currentlyPlaying.Remove(s);
        }
    }
}