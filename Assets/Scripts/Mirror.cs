using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour, IDestroyable
{
    [SerializeField] LevelManager levelManager;
    public void OnCollided()
    {
        Debug.Log("destroy mirror");
        levelManager.EndLevel();
        Destroy(gameObject);

    }
}
