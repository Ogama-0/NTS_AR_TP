using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction spaceAction;
    private InputAction horizontalAction;

    private Rigidbody rb;
    private bool inputJump;
    private bool isGrounded;
    
    // Start is called before the first frame update
    private void Start()
    {
        spaceAction = playerInput.actions["Space"];
        horizontalAction = playerInput.actions["Horizontal"];

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!inputJump)
        {
            inputJump = spaceAction.WasPerformedThisFrame();
        }

        float horizontal = horizontalAction.ReadValue<float>();
        if (horizontal != 0)
        {
            
            transform.Translate(Vector3.forward * horizontal * Time.deltaTime); 
        }
    }
    private void FixedUpdate()
    {
        if (inputJump && isGrounded)
        {
            rb.AddForce(Vector3.up * 3, ForceMode.VelocityChange);
            inputJump = false;
            isGrounded = false;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
//rb.AddForce(Vector3.forward * horizontal * 8, ForceMode.Acceleration);