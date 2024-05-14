using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FillScreen : MonoBehaviour
{
    public AudioManager am;
    public bool inRangeOfComputer = false;
    public Canvas whiteToFade;
    public Image whiteOnCanvasImg;
    public Chapter3Controller c3c;
    float timer = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inRangeOfComputer)
            {
                whiteFillScreen();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CompTrigger")
        {
            inRangeOfComputer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "CompTrigger")
        {
            inRangeOfComputer = false;
        }
    }

    void whiteFillScreen()
    {
        whiteToFade.gameObject.SetActive(true);
        StartCoroutine(fadeInWhite());
    }

    IEnumerator fadeInWhite()
    {
        c3c.enabled = false;
        yield return new WaitForSeconds(am.play("C3_Narr_Thank"));
        while(timer < 1)
        {
            whiteOnCanvasImg.color = new Color(255f, 255f, 255f, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene("Chapter 1");
        
    }
}