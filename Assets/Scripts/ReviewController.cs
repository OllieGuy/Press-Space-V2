using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReviewController : MonoBehaviour
{
    public GameObject Para1;
    public GameObject Para2;
    public GameObject NameInsult;
    public GameObject Para3;
    public GameObject Rev1;
    public GameObject Rev2;
    public GameObject Rev3;
    public GameObject SliderCanvas;
    public GameObject ReleaseCanvas;
    public AudioManagerNew am;
    public NameSlider ns;

    void Start()
    {
        StartCoroutine(SliderSection());
    }

    IEnumerator SliderSection()
    {
        yield return new WaitForSeconds(am.PlayClip("Rvew_Narr_Intro", false) + 0.75f);
        SliderCanvas.SetActive(true);
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(TextShow());
                SliderCanvas.SetActive(false);
                break;
            }
            yield return null;
        }
    }

    IEnumerator TextShow()
    {
        ReleaseCanvas.SetActive(true);
        yield return new WaitForSeconds(am.PlayClip("Rvew_Narr_Thank", false) + 0.75f);
        StartCoroutine(FadeInText(Para1));
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInText(Para2));
        yield return new WaitForSeconds(2f);
        if (ns.nameSelected)
        {
            StartCoroutine(FadeInText(NameInsult));
            yield return new WaitForSeconds(2f);
        }
        StartCoroutine(FadeInText(Para3));
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeInText(Rev1));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeInText(Rev2));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeInText(Rev3));
        yield return new WaitForSeconds(0.5f);
        bool beenFrus = false;
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("Chapter 1");
            else if (Input.anyKeyDown && !beenFrus)
            {
                am.PlayClip("Rvew_Narr_Frustrated", false); //add the name here
                beenFrus = true;
            }
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
