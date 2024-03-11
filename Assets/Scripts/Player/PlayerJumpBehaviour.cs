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


    public override void Interact()
    {
        if (playerMovementBehaviour == null) playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();

        //Debug.Log("START");

        if (playerInput.jumpPressed && playerMovementBehaviour.isGrounded)
        {
            playerMovementBehaviour.SetYVelocity(jumpVelocity);
        }
    }
}
