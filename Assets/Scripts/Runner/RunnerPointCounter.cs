using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RunnerPointCounter : MonoBehaviour
{
    private TMP_Text tt;
    private RunnerPlayer rp;
    public GameObject player;

    void Start()
    {
        tt = GetComponent<TMP_Text>();
        rp = player.GetComponent<RunnerPlayer>();
    }

    void LateUpdate()
    {
        tt.text = "Score: " + rp.score;
    }
}
