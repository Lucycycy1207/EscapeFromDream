using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
    [SerializeField] Transform playerCam;
    private float maxAngle = 90f;
    private float minAngle = 90f;
    private float originRotationOnY;
    private Vector3 newRotation;

    private Vector3 newPosition;
    private float cameraZValue;
    private float cameraYValue;

    private float CameraYRotationOffset;


    // Start is called before the first frame update
    void Start()
    {
        cameraZValue = this.transform.position.z;
        cameraYValue = this.transform.position.y;
        originRotationOnY = -180f;
        newPosition = new Vector3(playerCam.position.x, cameraYValue, cameraZValue);

        CameraYRotationOffset = this.transform.rotation.eulerAngles.y;
        //this.transform.position = newPosition;

        newRotation = transform.rotation.eulerAngles;

        Debug.Log($"origin: {newRotation}");
    }

    // Update is called once per frame
    void Update()
    {
        //if (playerCam.transform.position.x != this.transform.position.x)
        //{
        //    newPosition.x = playerCam.position.x;
        //    this.transform.position = newPosition;
        //}

        if (playerCam.parent.rotation.y != 0)
        {
            newRotation.y = CameraYRotationOffset - playerCam.parent.rotation.eulerAngles.y;
            this.transform.rotation = Quaternion.Euler(newRotation);
        }


    }
    

    
}
