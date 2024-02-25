using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupInteractor : Interactor
{
    [Header("Pickup Interaction")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private float pickupDistance;
    [SerializeField] private Transform attachTransform;

    private RaycastHit hit;
    private IPickable pickable;
    private bool isPicked = false;

    public override void Interact()
    {
        //Debug.DrawRay(GetCamRay().origin, GetCamRay().direction * pickupDistance, Color.red);
        if (Physics.Raycast(GetCamRay(), out hit, pickupDistance, pickupLayerMask))
        {
            if (playerInput.activatePressed && !isPicked)
            {
                pickable = hit.transform.GetComponent<IPickable>();
                if (pickable == null) return;

                pickable.OnPicked(attachTransform);
                isPicked = true;
                return;
            }
        }

        if (playerInput.activatePressed && isPicked && pickable != null)
        {
            pickable.OnDropped();
            isPicked = false;

        }

    }
    private Ray GetCamRay()
    {
        //Cast a ray
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        return ray;
    }

}
