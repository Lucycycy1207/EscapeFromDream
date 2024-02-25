using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 10.0f; //Done
    [SerializeField] private float turnSpeed = 10.0f; //Done
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private bool invertMouse;
    [SerializeField] private float sprintMultiplier = 2; //Done


    [Header("Player Jump")]
    [SerializeField] private Transform groundCheck; //Done
    [SerializeField] private LayerMask groundMask; //Done
    [SerializeField] private float groundCheckDistance; //Done
    [SerializeField] private float gravity = -9.81f; //Done
    [SerializeField] private float jumpVelocity; //Done

    [Header("Player Shoot")]
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private Rigidbody rocketPrefab;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform shootPoint;

    [Header("Interaction")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask interactionLayerMask;//Done
    [SerializeField] private float interactionDistance;//Done

    [Header("Pickup Interaction")]
    [SerializeField] private LayerMask pickupLayerMask;//Done
    [SerializeField] private float pickupDistance;//Done
    [SerializeField] private Transform attachTransform;//Done

    private CharacterController characterController; //Done
    private float horizontalInput, verticalInput; //Done
    private float mouseX, mouseY; //Done
    private float moveMultiplier = 1.0f; //Done
    private float camXRotation; //Done
    private bool isGrounded; //Done
    private Vector3 playerVelocity; //Done
    [SerializeField] private ISelectable selection;

    //Interaction Raycasts
    private RaycastHit hit;
    private IPickable pickable;
    private bool isPicked = false;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();//Done
    }//Done
    // Start is called before the first frame update
    void Start()
    {
        //Hide Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    } //done

    // Update is called once per frame
    void Update()
    {
        GetInput();
        RotatePlayer();

        GroundCheck();
        MovePlayer();
        JumpCheck();

        Shoot();
        Interact();

        PickAndDrop();
    }

    private void GetInput()//done
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        moveMultiplier = Input.GetButton("Sprint") ? sprintMultiplier : 1.0f;

    }

    private void MovePlayer()//done
    {
        characterController.Move((transform.forward * verticalInput + transform.right * horizontalInput)
            * moveSpeed * moveMultiplier * Time.deltaTime);

        //Ground Check
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        playerVelocity.y += gravity * Time.deltaTime;

        characterController.Move(playerVelocity * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        //Player turn movement
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime * mouseX);

        //Camera upo/down movement
        camXRotation += Time.deltaTime * mouseY * turnSpeed * (invertMouse ? 1 : -1);
        camXRotation = Mathf.Clamp(camXRotation, -50.0f, 50.0f);

        cameraTransform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    } //Done

    private void JumpCheck()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = jumpVelocity;
        }

    } //Done

    //private void OnDrawGizmos()//give some visual debugging on ground check this case
    //{
    //    Gizmos.color = Color.black;

    //    Gizmos.DrawSphere(groundCheck.position, groundCheckDistance);
    //}

    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
            Destroy(bullet.gameObject, 5.0f);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Rigidbody bullet = Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
            Destroy(bullet.gameObject, 5.0f);
        }

    }
    private void Interact()
    {
        // Cast a ray from the middle of the camera
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        // Check if the ray hits something within the specified distance and on a specific layer
        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayerMask))
        {
            Debug.Log("We hit " + hit.collider.name);
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red);

            // Get the ISelectable component from the hit object
            selection = hit.transform.GetComponent<ISelectable>();

            // Check if the hit object has the ISelectable interface
            if (selection != null)//double check object has specific functions
            {
                // Call the OnHoverEnter method when the object is hovered
                selection.OnHoverEnter();

                // Check for the E key press and call OnSelect if pressed
                if (Input.GetKeyDown(KeyCode.E))
                {
                    selection.OnSelect();
                }
            }
        }

        // If the ray doesn't hit anything and there was a previous selection, call OnHoverExit
        if (hit.transform == null && selection != null)
        {
            selection.OnHoverExit();
            selection = null;
        }
    }


    private void PickAndDrop()
    {
        Debug.DrawRay(GetCamRay().origin, GetCamRay().direction * pickupDistance, Color.red);
        if (Physics.Raycast(GetCamRay(), out hit, pickupDistance, pickupLayerMask))
        {
            //Debug.Log("pickable in sight");
            if (Input.GetKeyDown(KeyCode.E) && !isPicked)
            {
                pickable = hit.transform.GetComponent<IPickable>();
                if (pickable == null) return;

                pickable.OnPicked(attachTransform);
                isPicked = true;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && isPicked && pickable != null)
        {
            pickable.OnDropped();
            isPicked = false;

        }
    }

    private Ray GetCamRay()
    {
        //Cast a ray
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        return ray;
    }
}
