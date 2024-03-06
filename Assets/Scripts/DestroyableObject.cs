using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour, IDestroyable
{
    public void OnCollided()
    {
        Debug.Log("destroy cube");
        Destroy(gameObject, 0.5f);
    }

    
}
