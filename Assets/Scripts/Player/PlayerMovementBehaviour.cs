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

    private Animator animator;

    private float initMoveSpeed;
    private float initStepOffset;

    private CharacterController characterController;
    private Vector3 playerVelocity;
    public bool isGrounded { get; private set; }
    private float moveMultiplier = 1.0f;
    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();

        playerInput = PlayerInput.GetInstance();

        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
        animator.SetBool("ForwardWalk", false);
        animator.SetBool("BackwardWalk", false);
        animator.SetBool("GoLeft", false);
        animator.SetBool("GoRight", false);
        initMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MovePlayer();
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        //turn and jump is well done

        //moving animation
        
        
        animator.SetBool("ForwardWalk", playerInput.vertical > 0);
        animator.SetBool("BackwardWalk", playerInput.vertical < 0);
        animator.SetBool("Run", playerInput.sprintHeld && playerInput.vertical > 0);
        animator.SetBool("BackRun", playerInput.sprintHeld && playerInput.vertical < 0);

        animator.SetBool("GoLeft", playerInput.horizontal<0);
        animator.SetBool("GoRight", playerInput.horizontal>0);
        
        animator.SetBool("Idle", GetForwardSpeed()==0);



    }
    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }

    private void MovePlayer()
    {
        moveMultiplier = playerInput.sprintHeld ? sprintMultiplier : 1.0f;

        characterController.Move((transform.forward * playerInput.vertical + transform.right * playerInput.horizontal)
            * moveSpeed * moveMultiplier * Time.deltaTime);

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