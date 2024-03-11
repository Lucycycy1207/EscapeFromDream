using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
    public float horizontal { get; private set; }
    public float vertical { get; private set; }
    public float mouseX { get; private set; }
    public float mouseY { get;private set; }

    public bool sprintHeld { get; private set; }

    public bool jumpPressed { get; private set; }
    public bool primaryButtonPressed { get; private set; } //left click
    public bool secondaryButtonPressed { get; private set; } //right click
    public bool activatePressed { get; private set; }

    public bool weapon1Pressed { get; private set; }
    public bool weapon2Pressed { get; private set; }
    public bool commandPressed { get; private set; }

    public bool clear; //clear all the input

    private static PlayerInput instance;


    public bool isActive { private set; get; }
    public static PlayerInput GetInstance()
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

        isActive = true;
    }

    public void SetInputStatus(bool value)
    {
        isActive = value;
    }

    // Update is called once per frame
    void Update()
    {
        ClearInputs();
        if (isActive)
        {
            ProcessInputs();
        }
        
    }

    void ProcessInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        sprintHeld = sprintHeld || Input.GetButton("Sprint");
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");

        activatePressed = activatePressed || Input.GetKeyDown(KeyCode.E);

        primaryButtonPressed = primaryButtonPressed || Input.GetButtonDown("Fire1");
        secondaryButtonPressed = secondaryButtonPressed || Input.GetButtonDown("Fire2");

        weapon1Pressed = weapon1Pressed || Input.GetKeyDown(KeyCode.Alpha1);
        weapon2Pressed = weapon2Pressed || Input.GetKeyDown(KeyCode.Alpha2);

        commandPressed = commandPressed || Input.GetKeyDown(KeyCode.G);
    }

    private void FixedUpdate()
    {
        clear = true;
    }
    void ClearInputs()
    {
        if (!clear) return;
        horizontal = 0;
        vertical = 0;
        mouseX = 0;
        mouseY = 0;

        sprintHeld = false;
        jumpPressed = false;

        activatePressed = false;

        primaryButtonPressed = false;
        secondaryButtonPressed = false;

        weapon1Pressed = false;
        weapon2Pressed = false;

        commandPressed = false;
    }
}
