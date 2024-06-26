using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Chapter1Controller : MonoBehaviour
{
    public GameObject PSBlack;
    public GameObject counter;
    public Transform psTrans;
    public AudioManagerNew am;
    public int acceptableKeyCounter = 0;
    int unacceptableKeyCounter = 0;
    int level = 0;
    bool enabledInput = true;
    bool buzzerEnabled = false;
    bool clicksEnabled = false;

    string[] interruptAudios = { "C1v2_Narr_Interrupt_1", "C1v2_Narr_Interrupt_2", "C1v2_Narr_Interrupt_3","C1v2_Narr_Interrupt_3"};
    int currentInterruptions = 0;
    Coroutine currentNarration = null;

    void Start()
    {
        handleEvents(false);
    }

    void Update()
    {
        handleInput();
    }

    void handleInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (clicksEnabled) am.PlaySFX("C1_SFX_Click");
            handleEvents(true);
        }
        else if(Input.anyKeyDown && enabledInput)
        {
            if (buzzerEnabled)
            {
                Instantiate(PSBlack, psTrans);
                am.PlaySFX("C1_SFX_Buzzer");
            }
            checkInterrupt();
            handleEvents(false);
        }
    }

    void checkInterrupt()
    {
        if (am.CurrentlyPlaying())
        {
            if (currentInterruptions == 4)
            {
                StartCoroutine(interruptToChapter2());
            }
            else if (currentInterruptions < 4)
            {
                am.PlayClip(interruptAudios[currentInterruptions], true);
                StartCoroutine(disableInput(1.5f));
            }
            currentInterruptions++;
        }
    }

    void narratorLine(string name)
    {
        if (currentNarration == null)
        {
            currentNarration = StartCoroutine(levelWhenAudioFinished(name));
            StartCoroutine(disableInput(1.5f));
        }
    }

    void handleEvents(bool space)
    {
        switch (level)
        {
            case 0:
                narratorLine("C1v2_Narr_Intro");
                break;
            case 1:
                if (space)
                {
                    narratorLine("C1v2_Narr_Pressed");
                }
                else 
                {
                    narratorLine("C1v2_Narr_Correction_1");
                }
                break;
            case 2:
                if (space)
                {
                    acceptableKeyCounter++;
                    if (acceptableKeyCounter >= 25) SceneManager.LoadScene("Release");
                }
                else
                {
                    unacceptableKeyCounter++;
                    if (unacceptableKeyCounter >= 5)
                    {
                        clicksEnabled = true;
                        narratorLine("C1v2_Narr_Sounds");
                    }
                }
                break;
            case 3:
                if (space)
                {
                    acceptableKeyCounter++;
                    if (acceptableKeyCounter >= 20) SceneManager.LoadScene("Review");
                }
                else
                {
                    unacceptableKeyCounter++;
                    acceptableKeyCounter = 0;
                    if (unacceptableKeyCounter >= 5)
                    {
                        counter.SetActive(true);
                        narratorLine("C1v2_Narr_Counter");
                    }
                }
                break;
            case 4:
                if (space)
                {
                    acceptableKeyCounter++;
                    if (acceptableKeyCounter >= 20) SceneManager.LoadScene("Review");
                }
                else
                {
                    unacceptableKeyCounter++;
                    acceptableKeyCounter = 0;
                    if (unacceptableKeyCounter >= 5) narratorLine("C1v2_Narr_Correction_2");
                }
                break;
            case 5:
                if (space)
                {
                    acceptableKeyCounter++;
                    if (acceptableKeyCounter >= 20) SceneManager.LoadScene("Review");
                }
                else
                {
                    unacceptableKeyCounter++;
                    acceptableKeyCounter = 0;
                    if (unacceptableKeyCounter >= 5)
                    { 
                        buzzerEnabled = true;
                        narratorLine("C1v2_Narr_Buzzer"); 
                    }
                }
                break;
            case 6:
                if (space)
                {
                    acceptableKeyCounter++;
                    if (acceptableKeyCounter >= 20) SceneManager.LoadScene("Review");
                }
                else
                {
                    unacceptableKeyCounter++;
                    acceptableKeyCounter = 0;
                    if (unacceptableKeyCounter >= 10) narratorLine("C1v2_Narr_Game_Ask");
                }
                break;
            case 7:
                if (space)
                {
                    level++;
                    StartCoroutine(endScene(true));
                }
                else
                {
                    level++;
                    StartCoroutine(endScene(false));
                }
                break;
            case 8:
                break;
        }
    }

    IEnumerator endScene(bool choseGame)
    {
        if (choseGame)
        {
            am.PlayClip("C1_Narr_Game_Ask_Accept", false);
            while (true)
            {
                if (!am.CurrentlyPlaying()) break;

                else yield return new WaitForSeconds(0.5f);
            }
            SceneManager.LoadScene("Runner");
        }
        else
        {
            am.PlayClip("C1_Narr_Game_Ask_Reject", false);
            while (true)
            {
                if (!am.CurrentlyPlaying()) break;

                else yield return new WaitForSeconds(0.5f);
            }
            SceneManager.LoadScene("Chapter 2");
        }
    }
    IEnumerator interruptToChapter2()
    {
        enabledInput = false;
        yield return new WaitForSeconds(am.PlayClip("C1v2_Narr_Interrupt_4", true));
        SceneManager.LoadScene("Chapter 2");
    }

    IEnumerator levelWhenAudioFinished(string name)
    {
        am.PlayClip(name, false);
        while (true)
        {
            if (!am.CurrentlyPlaying()) break;

            else yield return new WaitForSeconds(0.5f);
        }
        if (level == 0) Instantiate(PSBlack, psTrans); //super janky
        level++;
        acceptableKeyCounter = 0;
        unacceptableKeyCounter = 0;
        currentNarration = null;
    }

    IEnumerator disableInput(float time)
    {
        enabledInput = false;
        yield return new WaitForSeconds(time);
        enabledInput = true;
    }
}
