using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class KeyVisual : MonoBehaviour
{
    public TextMeshPro tmp;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(PopInOut("other"));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PopInOut("space"));
        }
    }

    IEnumerator PopInOut(string newText)
    {
        tmp.gameObject.SetActive(true);
        tmp.text = newText;
        yield return new WaitForSeconds(0.4f);
        tmp.gameObject.SetActive(false);
    }
}
