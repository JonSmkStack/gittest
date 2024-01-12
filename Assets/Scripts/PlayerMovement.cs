using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Access Modifiers
    // private = NO ONE else besides this file can access this variable
    // public = anyone can access this variable

    // Information hiding / data encapsulation - best practices


    private Rigidbody rb;

    // Called an "annotation"
    [SerializeField]
    private float movementSpeed = 6f;

    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private Transform groundcheck;
    [SerializeField] private LayerMask ground;
    private float powerUpSpeedIncrease = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 5f, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }

        if (collision.gameObject.CompareTag("Power Up"))
        {
            movementSpeed += powerUpSpeedIncrease;
            Destroy(collision.gameObject);
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundcheck.position, .1f, ground);
    }
}
