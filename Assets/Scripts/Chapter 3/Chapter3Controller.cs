using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter3Controller : MonoBehaviour
{
    public AudioManager am;
    public int level = 0;
    public ParticleSystem ps;
    public GameObject player;
    public GameObject computer;
    public GameObject computerStand;
    private FillScreen fsPlayer;
    private bool levelInProgress = false;

    void Start()
    {
        StartCoroutine(footstepSounds());
        am.playOnLoop("C3_SFX_Void",0.7f);
        fsPlayer = player.GetComponent<FillScreen>();
    }

    void Update()
    {
        ps.transform.position = player.transform.position;
        handleEvents();
    }

    void handleEvents()
    {
        switch (level)
        {
            case 0:
                StartCoroutine(computerSpawnIn());
                levelInProgress = true;
                break;
            case 1:
                StartCoroutine(breakdown());
                levelInProgress = true;
                break;
            case 2:
                StartCoroutine(removeComputer());
                levelInProgress = true;
                break;
            default:
                break;
        }
    }
    IEnumerator footstepSounds()
    {
        float timer = 0;
        Vector3 prevPos = player.transform.position;
        bool lr = false;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 0.3f && Vector3.Distance(prevPos, player.transform.position) > 2)
            {
                if (!lr)
                {
                    am.play("C3_SFX_Step");
                    lr = true;
                }
                else
                {
                    am.play("C3_SFX_Step2");
                    lr = false;
                }
                prevPos = player.transform.position;
                timer -= 0.3f;
            }
            yield return null;
        }
    }

    IEnumerator computerSpawnIn()
    {
        if (!levelInProgress)
        {
            yield return new WaitForSeconds(am.play("C3_Narr_Intro"));
            yield return new WaitForSeconds(2);
            am.play("C3_SFX_Computer_On");
            yield return new WaitForSeconds(0.8f);
            computer.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForSeconds(am.play("C3_Narr_Instructions"));
            levelInProgress = false;
            level++;
        }
    }
    IEnumerator breakdown()
    {
        if (!levelInProgress)
        {
            yield return new WaitForSeconds(15f);
            yield return new WaitForSeconds(am.play("C3_Narr_Plead"));
            yield return new WaitForSeconds(15f);
            yield return new WaitForSeconds(am.play("C3_Narr_Last_Chance"));
            yield return new WaitForSeconds(10f);
            levelInProgress = false;
            level++;
        }
    }
    IEnumerator removeComputer()
    {
        if (!levelInProgress)
        {
            am.play("C3_SFX_Computer_Off");
            yield return new WaitForSeconds(2f);
            computer.SetActive(false);
            computerStand.SetActive(false);
            fsPlayer.inRangeOfComputer = false;
            yield return new WaitForSeconds(am.play("C3_Narr_Final_Monologue"));
            levelInProgress = false;
            level++;
        }
    }
        
}
