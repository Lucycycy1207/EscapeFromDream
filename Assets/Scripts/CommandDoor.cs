using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CommandDoor : MonoBehaviour
{
    
    private Animator doorAnimator;
    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
        doorAnimator.SetBool("Door", false); ;
    }

    public void SetDoorStatus(bool value)
    {
        doorAnimator.SetBool("Door", value);
    }

}
