using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SelectableInteractor : Interactor
{
    [Header("Select Interaction")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask interactionLayerMask;//Done
    [SerializeField] private float interactionDistance;//Done

    private RaycastHit hit;
    private ISelectable selection;
    public override void Interact()
    {
        // Cast a ray from the middle of the camera
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        // Check if the ray hits something within the specified distance and on a specific layer
        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayerMask))
        {
            //Debug.Log("We hit " + hit.collider.name);
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red);

            // Get the ISelectable component from the hit object
            selection = hit.transform.GetComponent<ISelectable>();

            // Check if the hit object has the ISelectable interface
            if (selection != null)//double check object has specific functions
            {
                // Call the OnHoverEnter method when the object is hovered
                selection.OnHoverEnter();

                // Check for the E key press and call OnSelect if pressed
                if (playerInput.activatePressed)
                {

                    selection.OnSelect();
                }
            }
        }

        // If the ray doesn't hit anything and there was a previous selection, call OnHoverExit
        if (hit.transform == null && selection != null)
        {
            selection.OnHoverExit();
            selection = null;
        }
    }

}
