using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//code generated in part by ChatGPT
public class Player3rdPersonController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 1f;
    public float groundDistance = 0.4f;
    public int jumpCount;

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.transform.position.y == 101.5 || rb.transform.position.y == 51.5)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        // Move the player horizontally
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Jump the player if they are grounded and the space key is pressed
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }
}
