using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class RunnerController : MonoBehaviour
{
    public GameObject player;
    public AudioManager am;
    RunnerPlayer rp;
    GroundAndObstacleSpawn gos;
    public bool explainedRandomDie = false;
    public bool explainedFirstFail = false;
    public bool diedForFirstTimeOnKey = false;
    bool justWon = false;
    int[] past3Scores = new int[3];

    void Start()
    {
        past3Scores[0] = 150;
        past3Scores[1] = 150;
        past3Scores[2] = 150;
        rp = player.GetComponent<RunnerPlayer>();
        gos = GetComponent<GroundAndObstacleSpawn>();
        StartCoroutine(intro());
    }

    void Update()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Space) && !gos.paused)
        {
            if (!explainedRandomDie)
            {
                diedForFirstTimeOnKey = true;
                rp.die();
                StartCoroutine(otherKeyDeath());
            }
            else
            {
                rp.die();
            }
        }
        if (rp.score >= 50 && !justWon)
        {
            justWon = true;
            StartCoroutine(win());
        }
    }

    public void addToPastScores(int score)
    {
        if (!explainedFirstFail && !diedForFirstTimeOnKey)
        {
            StartCoroutine(firstDeath());
        }
        past3Scores[0] = past3Scores[1];
        past3Scores[1] = past3Scores[2];
        past3Scores[2] = score;
        if (past3Scores[0] + past3Scores[1] + past3Scores[2] == 0)
        {
            StartCoroutine(lost3Times());
        }
    }

    IEnumerator intro()
    {
        rp.handleAnim(0);
        gos.paused = true;
        yield return new WaitForSeconds(am.play("Game_Narr_Intro"));
        gos.paused = false;
    }

    IEnumerator win()
    {
        rp.handleAnim(0);
        gos.paused = true;
        yield return new WaitForSeconds(am.play("Game_Narr_Win"));
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Chapter 1");
        gos.paused = false;
    }

    IEnumerator otherKeyDeath()
    {
        rp.handleAnim(4);
        gos.paused = true;
        yield return new WaitForSeconds(am.play("Game_Narr_Forbidden_Key_First_Death"));
        gos.paused = false;
        diedForFirstTimeOnKey = false;
    }

    IEnumerator firstDeath()
    {
        rp.handleAnim(4);
        gos.paused = true;
        yield return new WaitForSeconds(am.play("Game_Narr_First_Fail"));
        gos.paused = false;
    }

    IEnumerator lost3Times()
    {
        rp.handleAnim(4);
        gos.paused = true;
        yield return new WaitForSeconds(am.play("Game_Narr_Triple_Fail"));
        yield return new WaitForSeconds(1.5f);
        gos.paused = false;
        SceneManager.LoadScene("Chapter 2");
    }
}
