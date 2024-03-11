using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour, ISelectable
{
    public UnityEvent playerGunActive;
    public void OnHoverEnter()
    {
    }

    public void OnHoverExit()
    {
    }

    public void OnSelect()
    {
        Debug.Log("the gun is picked");
        playerGunActive?.Invoke();
        this.transform.parent.gameObject.SetActive(false);
    }

}
