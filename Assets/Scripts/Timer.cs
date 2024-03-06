using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float maxTime = 5f;
    [SerializeField] UIManager uiManager;
    [SerializeField] private RawImage ShadowImg;

    private Color shadowColor;
    
    private float time;
    public bool isTimeOut { get; private set; }

    public Action<float> OnTimerUpdate;
    public Action OnTimeOut;

    private float shadowRate;
    private float currentTime;


    

    // Start is called before the first frame update
    void Start()
    {
        time = maxTime;
        OnTimerUpdate(maxTime);
        shadowColor = ShadowImg.color;
        shadowColor.a = 0;

        ShadowImg.color = shadowColor;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DeductTime();
    }

    public void DeductTime()
    {
        if (isTimeOut) return;

        time -= Time.deltaTime;
        if (time <= 0)
        {
            isTimeOut = true;
            OnTimeOut();
            time = 0;
        }
        
        shadowColor.a = (maxTime-time)/maxTime;
        ShadowImg.color = shadowColor;
        OnTimerUpdate(time);
    }
}
