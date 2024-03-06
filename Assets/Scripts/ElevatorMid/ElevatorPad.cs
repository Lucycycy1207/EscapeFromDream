using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorPad : MonoBehaviour, ISelectable
{
    public UnityEvent ElevatorMove, levelComplete;

    [SerializeField] Transform nextPosition;
    
    private bool active;

    private void Start()
    {
        active = true;
    }
    public void OnHoverEnter()
    {
        Debug.Log("Start the elevator");

    }

    public void OnHoverExit()
    {
    }

    public void OnSelect()
    {
        active = false;
        Debug.Log("elevator start to move");
        gameObject.transform.GetComponent<BoxCollider>().enabled = false;
        //end current level
        levelComplete?.Invoke();
        ElevatorMove?.Invoke();
    }

}
