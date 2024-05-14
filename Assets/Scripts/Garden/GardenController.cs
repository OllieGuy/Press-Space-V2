using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class GardenController : MonoBehaviour
{
    public AudioManager am;
    public GameObject player;
    private IEnumerator currentCoroutine;
    private bool benchSitActive = false;

    void Start()
    {
        StartCoroutine(footstepSounds());
        am.playOnLoop("Gard_SFX_Ambient", 0.1f);
        currentCoroutine = gardenTalk();
        StartCoroutine(currentCoroutine);
    }

    void Update()
    {
        handleEvents();
    }

    void handleEvents()
    {
        if(player.GetComponent<BenchSit>().sat)
        {
            StopCoroutine(currentCoroutine);
            StartCoroutine(benchSatOn());
            benchSitActive = true;
        }
    }

    IEnumerator gardenTalk()
    {
        yield return new WaitForSeconds(am.play("Gard_Narr_Intro"));
        yield return new WaitForSeconds(15f);
        yield return new WaitForSeconds(am.play("Gard_Narr_Idle"));
        yield return new WaitForSeconds(15f);
        yield return new WaitForSeconds(am.play("Gard_Narr_Idle_2"));
        yield return new WaitForSeconds(15f);
        yield return new WaitForSeconds(am.play("Gard_Narr_Idle_3"));
    }

    IEnumerator benchSatOn()
    {
        if (!benchSitActive)
        {
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForSeconds(am.play("Gard_Narr_Monologue"));
            yield return new WaitForSeconds(5f);
            yield return new WaitForSeconds(am.play("Gard_Narr_Restarting"));
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("Chapter 1");
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
                    am.play("Gard_SFX_Grass_Walk");
                    lr = true;
                }
                else
                {
                    am.play("Gard_SFX_Grass_Walk_2");
                    lr = false;
                }
                prevPos = player.transform.position;
                timer -= 0.3f;
            }
            yield return null;
        }
    }
}
