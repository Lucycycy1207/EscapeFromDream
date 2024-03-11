using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    private PlayerInput playerInput;

    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float sprintMultiplier = 2;

    [Header("Ground Check")]
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance;


    private float initMoveSpeed;
    private float initStepOffset;

    private CharacterController characterController;
    private Vector3 playerVelocity;
    public bool isGrounded { get; private set; }
    private float moveMultiplier = 1.0f;

    public Vector3 MovementVector { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();

        playerInput = PlayerInput.GetInstance();

        initMoveSpeed = moveSpeed;
        initStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MovePlayer();
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }

    private void MovePlayer()
    {
        moveMultiplier = playerInput.sprintHeld ? sprintMultiplier : 1.0f;

        MovementVector = (transform.forward * playerInput.vertical + transform.right * playerInput.horizontal)
            * moveSpeed * moveMultiplier * Time.deltaTime;

       
        characterController.Move(MovementVector);

        //Ground Check
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void SetYVelocity(float value)
    {
        playerVelocity.y = value;
    }

    public float GetForwardSpeed()
    {
        return playerInput.vertical * moveSpeed * moveMultiplier;
    }

    public void ChangeMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
    public void ChangeMoveSpeed()
    {
        moveSpeed = initMoveSpeed;
    }

    public void ChangeStepOffset(float value)
    {
        this.characterController.stepOffset = value;    
    }
    public void ChangeStepOffset()
    {
        this.characterController.stepOffset = initStepOffset;
    }


}