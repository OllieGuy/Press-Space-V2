using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftAndDie : MonoBehaviour
{
    float timeTilDeath = 5f;
    float speed = 10f;
    GroundAndObstacleSpawn gos;
    public GameObject thisThing;
    void Start()
    {
        gos = GameObject.Find("RunnerController").GetComponent<GroundAndObstacleSpawn>();
        StartCoroutine(doTheThing());
    }

    void Update()
    {
        if(!gos.pausedForDeath && !gos.paused)
        {
            Vector3 shift = new Vector3(-(speed * Time.deltaTime), 0, 0);
            thisThing.transform.position += shift;
        }
    }

    IEnumerator doTheThing()
    {
        yield return new WaitForSeconds(timeTilDeath);
        while (true)
        {
            if (!gos.paused)
            {
                yield return new WaitForSeconds(1);
                Destroy(thisThing);
            }
            yield return null;
        }
    }
}
