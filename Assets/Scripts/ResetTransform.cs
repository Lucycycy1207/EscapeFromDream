using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform : MonoBehaviour
{
    public void ResetObjectTransform()
    {
        
        transform.position = Vector3.zero;

        
        transform.rotation = Quaternion.identity;

        transform.localScale = Vector3.one;
    
}
}
