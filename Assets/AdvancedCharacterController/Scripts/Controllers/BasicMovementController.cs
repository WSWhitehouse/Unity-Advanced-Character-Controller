using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AdvancedCharacterController))]
public class BasicMovementController : MonoBehaviour
{
    [InputSelector] public string horizontalInput = "Horizontal";
    [InputSelector] public string verticalInput = "Vertical";
    public bool useRawInput = false;

    [SearchableEnum]public KeyCode jumpKey = KeyCode.Space;

    public float moveSpeed = 7f;

    public float jumpSpeed = 15f;

    public bool jumpKeyPressed = true;
    private AdvancedCharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<AdvancedCharacterController>();
    }

    void Update()
    {
        jumpKeyPressed = Input.GetKey(jumpKey);
    }

    private void FixedUpdate()
    {
        Vector3 velocity = CalculateMovement() * moveSpeed;
        
        _characterController.Move(velocity);
    }

    private Vector3 CalculateMovement()
    {
        Vector3 velocity = Vector3.zero;

        float horizontal;
        float vertical;
        
        if (useRawInput)
        {
            horizontal = Input.GetAxisRaw(horizontalInput);
            vertical = Input.GetAxisRaw(verticalInput);
        }
        else
        {
            horizontal = Input.GetAxis(horizontalInput);
            vertical = Input.GetAxis(verticalInput);
        }

        velocity += transform.right * horizontal;
        velocity += transform.forward * vertical;
        
        if (velocity.magnitude > 1f)
            velocity.Normalize();

        return velocity;
    }
}
