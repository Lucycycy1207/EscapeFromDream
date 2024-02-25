using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerTurnBehaviour : Interactor
{
   
    [Header("Player Turn")]
    [SerializeField] private float turnSpeed = 10.0f;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Turn", false);
    }

    public override void Interact()
    {
        //Player turn movement
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * playerInput.mouseX);
        
        if (playerInput.mouseX != 0)
        {
            animator.SetBool("Turn", true);
        }
        else
        {
            animator.SetBool("Turn", false);
        }
        

    }
}

