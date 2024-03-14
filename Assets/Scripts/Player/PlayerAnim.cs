
using UnityEngine;

[RequireComponent(typeof(PlayerMovementBehaviour))]
public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private PlayerInput playerInput;
    private PlayerMovementBehaviour playerMovementBehaviour;

    private static PlayerAnim instance;
    public static PlayerAnim GetInstance()
    {
        return instance;
    }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        

        animator = GetComponent<Animator>();
        playerInput = PlayerInput.GetInstance();
    }

    public void SetParameter(string name)
    {
        animator.SetBool(name, animator.GetBool(name)?false:true);
    }

    private void Start()
    {
        animator.SetBool("ForwardWalk", false);
        animator.SetBool("BackwardWalk", false);
        animator.SetBool("Run", false);
        animator.SetBool("BackRun", false);
        animator.SetBool("GoLeft", false);
        animator.SetBool("GoRight", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Turn", false);
    }


    private void Update()
    {
        //Movement
        animator.SetBool("ForwardWalk", playerInput.vertical > 0);
        animator.SetBool("BackwardWalk", playerInput.vertical < 0);
        animator.SetBool("Run", playerInput.sprintHeld && playerInput.vertical > 0);
        animator.SetBool("BackRun", playerInput.sprintHeld && playerInput.vertical < 0);
        animator.SetBool("GoLeft", playerInput.horizontal < 0);
        animator.SetBool("GoRight", playerInput.horizontal > 0);

        animator.SetBool("Idle", this.GetComponent<PlayerMovementBehaviour>().GetForwardSpeed() == 0);
      
        //Turn
        if (playerInput.mouseX != 0)
        {
            animator.SetBool("Turn", true);

        }
        else
        {
            animator.SetBool("Turn", false);
        }


        //Jump
        if (playerMovementBehaviour == null) playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
        if (playerInput.jumpPressed && playerMovementBehaviour.isGrounded)
        {
            animator.SetBool("Jump", true);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") &&
           animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        
        {
            animator.SetBool("Jump", false);
        }
    
    }
}
