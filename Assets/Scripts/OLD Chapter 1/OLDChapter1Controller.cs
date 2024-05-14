using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OLDChapter1Controller : MonoBehaviour
{
    public GameObject PSBlack;
    public GameObject PSRed;
    public GameObject PFPuppyPlane;
    public GameObject isntWorking;
    public GameObject letsTry;
    public GameObject lessUplifting;
    public GameObject counter;
    public Image image;
    public AudioManager am;
    public int acceptableKeyCounter = 0;
    KeyController KC = new KeyController();
    PuppyExplode PE;
    int unacceptableKeyCounter = 0;
    int level = 0;
    GameObject PS;
    bool enabledInput = true;
    bool buzzerEnabled = false;
    int levelReps = 0;

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
                    if (buzzerEnabled)
                    {
                        am.play("C1_SFX_Buzzer");
                    }
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
        if (acceptableKeyCounter >= 25 && level <= 7)
        {
            SceneManager.LoadScene("Job");
        }
        if (acceptableKeyCounter >= 25 && level >= 8 && level <= 9)
        {
            SceneManager.LoadScene("Evil");
        }
        switch (level)
        {
            case 0:
                if (unacceptableKeyCounter % 5 == 1 && unacceptableKeyCounter / 5 >= levelReps)
                {
                    if (levelReps < 1)
                    {
                        Instantiate(PSBlack, image.transform);
                        StartCoroutine(disableInput(2.5f));
                        levelReps++;
                        break;
                    }
                    else
                    {
                        Instantiate(PSBlack, image.transform);
                        levelReps = 0;
                        levelUp(2.5f);
                    }
                }
                break;
            case 1:
                if (unacceptableKeyCounter == 10)
                {
                    Instantiate(PSRed, image.transform);
                    levelUp(2.5f);
                }
                break;
            case 2:
                if (unacceptableKeyCounter == 10)
                {
                    StartCoroutine(disableInput(5f));
                    StartCoroutine(counterIntro());
                }
                break;
            case 3:
                if (unacceptableKeyCounter == 10)
                {
                    StartCoroutine(disableInput(2.5f));
                    StartCoroutine(buzzerIntro());
                }
                break;
            case 4:
                if (unacceptableKeyCounter == 5)
                {
                    levelUp(am.play("C1_Narr_Intro"));
                }
                break;
            case 5:
                if (unacceptableKeyCounter == 5)
                {
                    levelUp(am.play("C1_Narr_Correction_1"));
                }
                break;
            case 6:
                if (unacceptableKeyCounter == 5)
                {
                    levelUp(am.play("C1_Narr_Correction_2"));
                }
                break;
            case 7:
                if (unacceptableKeyCounter == 5)
                {
                    levelUp(am.play("C1_Narr_Correction_3"));
                }
                break;
            case 8:
                if (unacceptableKeyCounter == 10)
                {
                    StartCoroutine(disableInput(am.play("C1_Narr_Puppies_Intro")));
                    counter.SetActive(false);
                    buzzerEnabled = false;
                    am.play("C1_SFX_Bark");
                    PS = Instantiate(PFPuppyPlane, image.transform);
                    PE = PS.GetComponent<PuppyExplode>();
                    levelUp(2.5f);
                }
                break;
            case 9:
                if (unacceptableKeyCounter == 1)
                {
                    if (levelReps < 4)
                    {
                        StartCoroutine(disableInput(2.5f));
                        PE.boom();
                        levelReps++;
                        unacceptableKeyCounter = 0;
                        break;
                    }
                    else
                    {
                        PE.boom();
                        StartCoroutine(puppyCleanUp());
                        levelUp(am.play("C1_Narr_Puppies_Fail"));
                    }
                }
                break;
            case 10:
                levelUp(am.play("C1_Narr_Game_Ask"));
                acceptableKeyCounter = 0;
                break;
            case 11:
                if (acceptableKeyCounter == 1)
                {
                    SceneManager.LoadScene("Runner");
                }
                else
                {
                    SceneManager.LoadScene("Chapter 2");
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
    IEnumerator puppyCleanUp()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(PS);
        Destroy(GameObject.Find("PuppyBoom(Clone)"));
    }
    IEnumerator counterIntro()
    {
        Instantiate(isntWorking, image.transform);
        yield return new WaitForSeconds(2.5f);
        Instantiate(letsTry, image.transform);
        yield return new WaitForSeconds(2.5f);
        counter.SetActive(true);
        levelUp(0f);
    }
    IEnumerator buzzerIntro()
    {
        Instantiate(lessUplifting, image.transform);
        yield return new WaitForSeconds(2.5f);
        buzzerEnabled = true;
        levelUp(0f);
    }
    IEnumerator disableInput(float time)
    {
        enabledInput = false;
        yield return new WaitForSeconds(time);
        enabledInput = true;
    }
}
