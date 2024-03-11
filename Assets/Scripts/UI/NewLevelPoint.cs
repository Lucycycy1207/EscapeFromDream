using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewLevelPoint : MonoBehaviour
{
    public UnityEvent EnableInstruction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("trigger success");
            EnableInstruction?.Invoke();
            this.gameObject.SetActive(false);

        }
    }
}
