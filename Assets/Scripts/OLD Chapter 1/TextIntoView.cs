using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class TextIntoView : MonoBehaviour
{
    public float fadeInDuration = 1f;
    public float holdDuration = 1f;
    public float fadeOutDuration = 0.5f;
    public GameObject thisText;
    private TMP_Text textMesh;
    private Color originalColor;
    float elapsedTime;
    int state;

    private void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        originalColor = textMesh.color;
        elapsedTime = 0;
        state = 0;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (state == 0)
        {
            fadeIn();
        }
        else if (state == 1)
        {
            hold();
        }
        else if (state == 2)
        {
            fadeOut();
        }
    }

    void fadeIn()
    {
        if (elapsedTime <= fadeInDuration)
        {
            float t = Mathf.Clamp01(elapsedTime / fadeInDuration);
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(0f, 1f, t);
            textMesh.color = newColor;
        }
        else
        {
            elapsedTime = 0;
            state = 1;
        }
    }
    void fadeOut()
    {
        if (elapsedTime <= fadeOutDuration)
        {
            float t = Mathf.Clamp01(elapsedTime / fadeOutDuration);
            Color newColor = originalColor;
            newColor.a = Mathf.Lerp(1f, 0f, t);
            textMesh.color = newColor;
        }
        else
        {
            Destroy(thisText);
        }
    }

    void hold()
    {
        if (elapsedTime > holdDuration)
        {
            elapsedTime = 0;
            state = 2;
        }
    }
}

