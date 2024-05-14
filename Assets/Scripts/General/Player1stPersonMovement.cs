using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1stPersonMovement : MonoBehaviour
{
    public float speed = 5f; // Player movement speed
    public float mouseSensitivity = 100f; // Mouse sensitivity
    public Camera playerCamera; // Player camera

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Freeze the rotation of the Rigidbody so we don't fall over

        //Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        //Cursor.visible = false; // Hide the cursor
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Get horizontal input (-1 to 1)
        float vertical = Input.GetAxis("Vertical"); // Get vertical input (-1 to 1)

        Vector3 movement = transform.right * horizontal + transform.forward * vertical; // Create movement vector relative to the player's rotation

        //rb.MovePosition(transform.position + (movement.normalized * speed * Time.fixedDeltaTime)); // Move the player
        transform.position += (movement.normalized * speed * Time.fixedDeltaTime);

        Quaternion CharacterRotation = playerCamera.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;

        transform.rotation = CharacterRotation;
    }
}
