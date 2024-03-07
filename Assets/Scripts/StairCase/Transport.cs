using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    public void TeleportOnY(GameObject target, Vector3 targetPoint)
    {
        //Debug.Log($"player position = {targetPoint}");
        target.transform.position = new Vector3(target.transform.position.x, targetPoint.y, target.transform.position.z);
    }
}
