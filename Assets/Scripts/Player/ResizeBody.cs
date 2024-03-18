using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResizeBody : MonoBehaviour
{
    [SerializeField] private PlayerMovementBehaviour playerMovement;
    [SerializeField] private float ShrinkMoveSpeed = 1f;

    [SerializeField] private float shrinkSize = 0.10f;
    [SerializeField] private float shrinkStepOffset = 0.06f;

    [SerializeField] private CinemachineDollyCart deadCart;
    private Vector3 originSize;
    private void Start()
    {
        originSize = this.transform.localScale;
    }
    public void Shrink()
    {
        Debug.Log("shrink");
        playerMovement.ChangeMoveSpeed(ShrinkMoveSpeed);
        playerMovement.ChangeStepOffset(shrinkStepOffset);
        this.transform.localScale = new Vector3(shrinkSize, shrinkSize, shrinkSize);
        
        deadCart.m_Speed /= shrinkSize;

    }

    public void ChangeToDefault()
    {
        Debug.Log("ChangeToDefault");
        playerMovement.ChangeMoveSpeed();
        playerMovement.ChangeStepOffset();
        this.transform.localScale = new Vector3(1, 1, 1);
    }
}
