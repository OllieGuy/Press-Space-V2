using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ReleaseController : MonoBehaviour
{
    public GameObject Para1;
    public GameObject Para2;
    public GameObject Para3;
    public GameObject Rev1;
    public GameObject Rev2;
    public GameObject Rev3;
    public AudioManagerNew am;

    void Start()
    {
        StartCoroutine(TextShow());
    }

    IEnumerator TextShow()
    {
        //yield return new WaitForSeconds(am.PlayClip("RLSE_Narr_Thank", false) + 0.75f);
        yield return new WaitForSeconds(am.PlayClip("C1_Narr_Intro", false) + 0.75f);
        StartCoroutine(FadeInText(Para1));
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInText(Para2));
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInText(Para3));
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInText(Rev1));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeInText(Rev2));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeInText(Rev3));
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (Input.anyKeyDown) SceneManager.LoadScene("Chapter 1");
            yield return null;
        }    
    }

    IEnumerator FadeInText(GameObject bodyOfText)
    {
        bodyOfText.SetActive(true);

        float fadeInDuration = 1f;
        TMP_Text textMesh = bodyOfText.GetComponent<TMP_Text>();
        Color originalColor = textMesh.color;
        float elapsedTime = 0;

        while (elapsedTime < fadeInDuration)
        {
            float t = Mathf.Clamp01(elapsedTime / fadeInDuration);
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(0f, 1f, t);
            textMesh.color = newColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
