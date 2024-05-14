using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BedLie : MonoBehaviour
{
    public bool inRangeOfBed = false;
    public Transform layOnBed;
    Rigidbody rb;
    float speed = 1f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inRangeOfBed)
            {
                rb = GetComponent<Rigidbody>();
                rb.isKinematic = true;
                benchSit();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BedTrigger")
        {
            inRangeOfBed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "BedTrigger")
        {
            inRangeOfBed = false;
        }
    }

    void benchSit()
    {
        StartCoroutine(moveToPosition(2f));
    }

    IEnumerator moveToPosition(float timeToMove)
    {
        float singleStep = speed * Time.deltaTime;
        var currentPos = transform.position;
        var currentRot = transform.rotation;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, layOnBed.position, t);
            transform.rotation = Quaternion.Lerp(currentRot, layOnBed.rotation, t);
            yield return null;
        }
    }
}