using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorPad : MonoBehaviour, ISelectable
{
    public UnityEvent ElevatorMove;

    [SerializeField] Transform nextPosition;

    public void OnHoverEnter()
    {
        Debug.Log("Start the elevator");

    }

    public void OnHoverExit()
    {
    }

    public void OnSelect()
    {
        Debug.Log("elevator start to move");
        gameObject.transform.GetComponent<BoxCollider>().enabled = false;
        ElevatorMove.Invoke();
    }

}
