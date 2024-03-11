using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorPad : MonoBehaviour, ISelectable
{
    public UnityEvent ElevatorMove, levelComplete;

    [SerializeField] Transform nextPosition;
    
    private bool active;

    Transform childCanvas;

    private void Start()
    {
        active = true;
        childCanvas = transform.Find("Canvas");
        childCanvas.gameObject.SetActive(false);
        
    }
    public void OnHoverEnter()
    {
        Debug.Log("Start the elevator");
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
        active = false;
        Debug.Log("elevator start to move");
        gameObject.transform.GetComponent<BoxCollider>().enabled = false;

        ElevatorMove?.Invoke();
    }

}
