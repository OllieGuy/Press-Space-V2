using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEnter : MonoBehaviour
{
    public bool inRangeOfGarden = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GardenTrigger")
        {
            inRangeOfGarden = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "GardenTrigger")
        {
            inRangeOfGarden = false;
        }
    }
}
