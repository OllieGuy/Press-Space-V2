using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public KeyCode[] acceptableKeys = {KeyCode.Space};

    public KeyController()
    {

    }

    public KeyController(KeyCode[] acceptableKeys)
    {
        this.acceptableKeys = acceptableKeys;
    }

    void Start()
    {
        acceptableKeys.Append(KeyCode.W);
    }

    void addAcceptableKey(KeyCode toAdd)
    {
        acceptableKeys.Append(toAdd);
    }
}
