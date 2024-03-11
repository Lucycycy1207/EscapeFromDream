using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElevatorMovement : MonoBehaviour
{
    [SerializeField] private Transform nextPostion;
    [SerializeField] GameObject player;
    [SerializeField] LevelManager levelManager;
    
    public UnityEvent playerReset;
    
    public void MoveElevator()
    {
        //
        Debug.Log("move Elevator");
        Preparation();
        this.gameObject.transform.position = nextPostion.position;
        this.gameObject.transform.rotation = nextPostion.rotation;
        this.gameObject.transform.localScale = nextPostion.localScale;
        RestoreChanges();

        levelManager.EndLevel();
        Debug.Log("end of level 1");
    }

    private void RestoreChanges()
    {
        //detach elevator and player
        player.transform.SetParent(null);
        //unlock the door
        this.GetComponent<Elevator>().UnlockDoor();
        playerReset?.Invoke();
        player.GetComponent<CharacterController>().GetComponent<Collider>().enabled = true;
    }

    private void Preparation()
    {
        player.GetComponent<CharacterController>().GetComponent<Collider>().enabled = false;
        //set player as child of elevator
        player.transform.SetParent(this.gameObject.transform);
        //LOCK THE DOOR
        this.GetComponent<Elevator>().lockDoor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
