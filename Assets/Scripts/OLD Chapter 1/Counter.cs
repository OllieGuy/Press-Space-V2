using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private TMP_Text tt;
    public Chapter1Controller c1c;

    void Start()
    {
        tt = GetComponent<TMP_Text>();
        c1c = (GameObject.Find("Chapter 1 Controller")).GetComponent<Chapter1Controller>();
    }

    void LateUpdate()
    {
        tt.text = "Counter: " + c1c.acceptableKeyCounter;
    }
}
