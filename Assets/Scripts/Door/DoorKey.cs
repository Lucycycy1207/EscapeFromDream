using System.Collections;
using System.Collections.Generic;
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

    public void OnHoverEnter()
    {
        Debug.Log("press E to PickUp door key");
        
    }

    public void OnHoverExit()
    {
    }

    public void OnSelect()
    {
        Debug.Log("door key is picked");
        ActiveDoor.Invoke();
        Destroy(gameObject);
    }

}
