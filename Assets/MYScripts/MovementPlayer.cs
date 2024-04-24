using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    // checks if the player is controlling the chr; it's a property; private - don't want to set this outside of this script
    public bool CanMove = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;
    public DialogueSystem dialogue;

    private float gravity = 30.0f;
    private float rotationX = 0; // angle we try to clamp with the upper and lower look limit

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canUseHeadBob = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift; // the key for the sprint
    [SerializeField] private KeyCode jumpKey = KeyCode.Space; // the key for jump

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private float sprintSpeed = 4.0f;

    [Header("Look Parameters")]
    // look spd between 1 and 10 for the look speed on x and y axis
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    // upper and lower limit for the look up/ down
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f; // how many degrees can we look before camera stops movin'
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 6.0f;

    [Header("Headbob Parameters")]
    [SerializeField] private float walkBobSpeed = 10f;
    [SerializeField] private float walkBobAmount = 0.08f;
    [SerializeField] private float sprintBobSpeed = 14f;
    [SerializeField] private float sprintBobAmount = 0.16f;
    private float defaultYPos = 0;
    private float timer; // determines where abt the camera needs to be vertically for the head bob

    private Camera playerCamera; // reference to the camera
    private CharacterController characterController;

    private Vector3 moveDirection; // the final move amount applied to the chr controller
    private Vector2 currentInput; // the value given to the controller via keyboard, arrow keys or awsd

    private Vector2 CurrentVector;
    private Vector2 SmoothInputVel;
    private float SmoothInputSpeed;
    Rigidbody rb;


    void Awake() // Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>(); // camera is going to be a child of a first person controller
        characterController = GetComponent<CharacterController>(); // it's gonna be attached to the parent obj
        defaultYPos = playerCamera.transform.localPosition.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLock();

            if (canJump)
                HandleJump();

            if (canUseHeadBob)
                HandleHeadBob();

            ApplyFinalMovements();
        }
        rb.freezeRotation = true;
    }


    private void HandleMovementInput() // the keyboard controller
    {
        currentInput = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")).normalized;
        CurrentVector = Vector2.SmoothDamp(CurrentVector, currentInput, ref SmoothInputVel, SmoothInputSpeed); // don't move faster when diagonally
        // reset the y pos to its original value
        float moveDirectionY = moveDirection.y; // the actual vertical going upwards away from the floor are going towards the floor
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x * walkSpeed) + (transform.TransformDirection(Vector3.right) * currentInput.y * walkSpeed); //calculate the move dir so the vector 3 based on the chr's orientation
        moveDirection.y = moveDirectionY;
    }


    private void HandleMouseLock()
    {
        // what y axis is on the mouse - up & down value - the most y value controls the x rotation of the camera
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit); // should limit the camera at 80 degrees up/ down
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }


    private void HandleJump()
    {
        //if (ShouldJump) // checks whether or not is grounded
           // moveDirection.y = jumpForce;
    }


    private void HandleHeadBob()
    {
        if (!characterController.isGrounded) return;

        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (IsSprinting ? sprintBobSpeed : walkBobSpeed);
            // control the pos of the player's camera to match what the expected pos is gonna be for the head bob
            // y value -> amend; what pos would that be based on the timer
            // mathf.sin() => the sine angle of a flaw that's passed in, between -1 and 1: if it's negative - lower the camera; if it's positive - raise the camera
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, defaultYPos + Mathf.Sin(timer) * (IsSprinting ? sprintBobAmount : walkBobAmount), playerCamera.transform.localPosition.z);
        }
    }


    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime; // pulls down towards the nearest surface
        characterController.Move(moveDirection * (IsSprinting ? sprintSpeed : walkSpeed) * Time.deltaTime);
        // if grounded but the velocity is < -1 then that's the landing frame, so reset move dir Y value to 0
        if (characterController.velocity.y < -1 && characterController.isGrounded)
            moveDirection.y = 0;
    }
}

