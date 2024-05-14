using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EvilController : MonoBehaviour
{
    public GameObject PSEvil;
    public Image image;
    public AudioManager am;
    public int acceptableKeyCounter = 0;
    KeyController KC = new KeyController();
    int unacceptableKeyCounter = 0;
    int level = 0;
    bool enabledInput = true;
    int levelReps = 0;


    void Start()
    {
        am.playOnLoop("Evil_SFX_Ambience", 0.3f);
    }
    void Update()
    {
        handleInput();
    }

    void handleInput()
    {
        if (Input.anyKeyDown && enabledInput)
        {
            for (int i = 0; i < KC.acceptableKeys.Length; i++)
            {
                if (!Input.GetKeyDown(KC.acceptableKeys[i]))
                {
                    unacceptableKeyCounter++;
                    acceptableKeyCounter = 0;
                    Debug.Log(unacceptableKeyCounter);
                }
                else
                {
                    acceptableKeyCounter++;
                }
            }
            handleEvents();
        }
    }

    void handleEvents()
    {

        if (acceptableKeyCounter == 250)
        {
            StartCoroutine(beatenEvil());
        }
        switch (level)
        {
            case 0:
                levelUp(am.play("Evil_Narr_Intro"));
                break;
            case 1:
                Instantiate(PSEvil, image.transform);
                levelUp(am.play("Evil_SFX_Ominous_Thud"));
                break;
            case 2:
                if (unacceptableKeyCounter % 10 == 1 && acceptableKeyCounter == 0)
                {
                    System.Random rand = new System.Random();
                    int choice = rand.Next(0, 3);
                    switch (choice)
                    {
                        case 0:
                            Instantiate(PSEvil, image.transform);
                            am.play("Evil_SFX_Ominous_Thud");
                            StartCoroutine(disableInput(2.5f));
                            levelReps++;
                            break;
                        case 1:
                            StartCoroutine(disableInput(am.play("Evil_SFX_Crowd_Boo")));
                            break;
                        case 2:
                            int voiceLine = rand.Next(1, 11);
                            string voiceLineToPlay = "Evil_Narr_Quip_" + voiceLine.ToString();
                            am.play(voiceLineToPlay);
                            break;
                        default:
                            break;
                    }
                }
                break;
            default:
                break;
        }
    }
    void levelUp(float timeToLockInput)
    {
        StartCoroutine(disableInput(timeToLockInput));
        unacceptableKeyCounter = 0;
        level++;
    }
    IEnumerator disableInput(float time)
    {
        enabledInput = false;
        yield return new WaitForSeconds(time);
        enabledInput = true;
    }

    IEnumerator beatenEvil()
    {
        float time = am.play("Evil_Narr_Reach250");
        enabledInput = false;
        yield return new WaitForSeconds(am.play("Evil_Narr_Reach250"));
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Chapter 1");
    }
}