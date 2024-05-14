using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrisonController : MonoBehaviour
{
    public Camera cellCamera;
    public Camera solitaryCamera;
    public GameObject player;
    public AudioManager am;
    private IEnumerator currentCoroutine;
    private bool currentlyInRoutine = false;
    private bool bedLie = false;
    private Vector3 bedPos = new Vector3(-2.43f, 1.38f, -2.03f);

    void Start()
    {
        cellCamera.enabled = true;
        solitaryCamera.enabled = false;
        solitaryCamera.GetComponent<AudioListener>().enabled = false;
        currentCoroutine = prison();
        StartCoroutine(currentCoroutine);
        currentlyInRoutine = true;
        am.playOnLoop("Pris_SFX_Ambient",0.5f);
    }

    void Update()
    {
        if (player.transform.position == bedPos && !bedLie)
        {
            bedLie = true;
            StopCoroutine(currentCoroutine);
            StartCoroutine(inBed());
        }
        if (player.transform.position.z < -8f)
        {
            currentlyInRoutine = false;
            player.transform.position = new Vector3(9.5f, 1.5f, -3.5f);
            StopCoroutine(currentCoroutine);
            StartCoroutine(goToSolitary());
            currentlyInRoutine = true;
        }
    }

    IEnumerator prison()
    {
        if(!currentlyInRoutine)
        {
            yield return new WaitForSeconds(am.play("Pris_Narr_Intro"));
            yield return new WaitForSeconds(15);
            yield return new WaitForSeconds(am.play("Pris_Narr_Idle"));
            currentlyInRoutine = false;
        }
    }

    IEnumerator inBed()
    {
        yield return new WaitForSeconds(am.play("Pris_Narr_Enter_Bed"));
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Chapter 1");
    }

    IEnumerator goToSolitary()
    {
        if (!currentlyInRoutine)
        {
            yield return new WaitForSeconds(am.play("Pris_Narr_Leave_Cell"));
            yield return new WaitForSeconds(2.5f);
            cellCamera.enabled = false;
            cellCamera.GetComponent<AudioListener>().enabled = false;
            solitaryCamera.enabled = true;
            solitaryCamera.GetComponent<AudioListener>().enabled = true;
            yield return new WaitForSeconds(am.play("Pris_Narr_Conf_Intro"));
            yield return new WaitForSeconds(3);
            yield return new WaitForSeconds(am.play("Pris_Narr_Conf_Monologue"));
            yield return new WaitForSeconds(7); 
            yield return new WaitForSeconds(am.play("Pris_Narr_Conf_Idle"));
            yield return new WaitForSeconds(5);
            yield return new WaitForSeconds(am.play("Pris_Narr_Conf_End"));
            yield return new WaitForSeconds(3);
            currentlyInRoutine = false;
            SceneManager.LoadScene("Chapter 1");
        }
    }
}
