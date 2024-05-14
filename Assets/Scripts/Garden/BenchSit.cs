using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BenchSit : MonoBehaviour
{
    public bool sat = false;
    public bool inRangeOfBench = false;
    public Transform satOnBench;
    Rigidbody rb;
    Player1stPersonMovement p1f;
    float speed = 1f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inRangeOfBench)
            {
                rb = GetComponent<Rigidbody>();
                rb.isKinematic = true;
                p1f = GetComponent<Player1stPersonMovement>();
                p1f.enabled = false;
                benchSit();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BenchTrigger")
        {
            inRangeOfBench = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "BenchTrigger")
        {
            inRangeOfBench = false;
        }
    }

    void benchSit()
    {
        sat = true;
        StartCoroutine(moveToPosition(2f));
    }

    IEnumerator moveToPosition(float timeToMove)
    {
        float singleStep = speed * Time.deltaTime;
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, satOnBench.position, t);
            yield return null;
        }
    }
}