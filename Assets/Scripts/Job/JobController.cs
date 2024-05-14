using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JobController : MonoBehaviour
{
    public AudioManager am;
    public bool doneTyping = false;

    void Start()
    {
        am.playOnLoop("Job_SFX_Type",0.1f);
        StartCoroutine(clicks());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && doneTyping)
        {
            SceneManager.LoadScene("Chapter 1");
        }
    }
    IEnumerator clicks()
    {
        yield return new WaitForSeconds(48);
        Debug.Log("try break");
        am.breakLoop("Job_SFX_Type");
        doneTyping = true;
    }
}
