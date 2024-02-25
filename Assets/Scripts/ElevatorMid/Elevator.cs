using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private Animator elevatorDoorAnim;

    private float timer = 0;
    [SerializeField] private Renderer elevatorDoorRenderer;

    [SerializeField] private float waitTime = 1.0f;

    [SerializeField] private ElevatorPad elevatorPad;


    private bool isLocked = true;

    private void Awake()
    {
        //get Door animator at the start
        elevatorDoorAnim = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        //detect player,set color to active
        if (!isLocked && other.CompareTag("Player"))
        {
            timer = 0;

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (isLocked) return;
        if (!other.CompareTag("Player")) { return; } //Guard clause to exit if not player
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            timer = waitTime;//increase timer value
            elevatorDoorAnim.SetBool("isDoorOpen", true);
            //waitTime *= 1.5f;//increase waitTime value
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elevatorDoorAnim.SetBool("isDoorOpen", false);
        }
    }

    public void UnlockDoor()
    {
        Debug.Log("Unlock");
        isLocked = false;
    }
    public void lockDoor()
    {
        isLocked = true;
        Debug.Log("lock");
    }


}
