using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code generated in part by ChatGPT
public class Player3rdPersonControllerPrison : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 1f;
    public float groundDistance = 0.4f;
    public GameObject chapter2Controller;

    private Rigidbody rb;
    private Chapter2Controller c2c;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        c2c = chapter2Controller.GetComponent<Chapter2Controller>();
    }

    private void Update()
    {

        // Move the player horizontally
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Jump the player if they are grounded and the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space pressed. Interact here");
        }
    }
}
