using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Control cutscenes of begin, defeat and complete.
/// </summary>
public class TimeLineManager : MonoBehaviour
{
    [SerializeField] private List<Transform> timeLines = new List<Transform>();

    private void Awake()
    {
        for (int i = 0; i < timeLines.Count; i++)
        {
            timeLines[i].gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// begin, defeat, complete for index 0,1,2
    /// </summary>
    /// <param name="index"></param>
    public void ActiveTimeLine(int index)
    {
        timeLines[index].gameObject.SetActive(true);
    }


}
