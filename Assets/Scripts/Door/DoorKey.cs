using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A customizable component that unlocks a door when picked up by the player. 
/// Use triggers and destroy the key (the GameObject with the compoent) when picked up. 
/// This component needs to be used for other doors and should not be just limited to unlocking doors.
/// </summary>
public class DoorKey : MonoBehaviour, ISelectable
{
    public UnityEvent ActiveDoor;
    Transform childCanvas;

    private void Start()
    {
        childCanvas = transform.Find("Canvas");
        childCanvas.gameObject.SetActive(false);
    }
    public void OnHoverEnter()
    {
        //TODO: show
        //Debug.Log("press E to PickUp door key");
        childCanvas.gameObject.SetActive(true);

    }

    public void OnHoverExit()
    {
        //TODO: disable UI
        if (childCanvas != null)
        {
            childCanvas.gameObject.SetActive(false);
        }
        
    }

    public void OnSelect()
    {
        //Debug.Log("door key is picked");
        //TODO: picking sound
        ActiveDoor.Invoke();
        Destroy(gameObject);
    }

}
