using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private StairCase stairCase;
    private int times = 0;
    [SerializeField] private bool isLast;

    private void OnEnable()
    {
        stairCase.ResetCheckPoint += ResetTimes;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log($"{this.name} Trigger entered!");
            if (isLast == false)
            {
                stairCase.CheckPointUpdate(this.transform, ++times);
            }
            else
            {
                Debug.Log("call end level");
                stairCase.GameFinish();
            }
            
        }
    }
    public void ResetTimes()
    {
        times = 0;
    }

}
