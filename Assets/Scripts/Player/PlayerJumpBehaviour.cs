using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Null check
[RequireComponent(typeof(PlayerMovementBehaviour))]
public class PlayerJumpBehaviour : Interactor
{

    [Header("Player Jump")]
    [SerializeField] private float jumpVelocity;

    private PlayerMovementBehaviour playerMovementBehaviour;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Jump", false);
    }

    public override void Interact()
    {
        if (playerMovementBehaviour == null) playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();

        
        if (playerInput.jumpPressed && playerMovementBehaviour.isGrounded)
        {
            playerMovementBehaviour.SetYVelocity(jumpVelocity);

            //Debug.Log("jump");
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

    }
}
