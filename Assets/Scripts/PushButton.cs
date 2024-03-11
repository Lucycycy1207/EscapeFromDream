using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    [SerializeField] private Renderer buttonRenderer;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color hoverColor;


    public UnityEvent OnPushButton;
    Transform childCanvas;

    private void Start()
    {
        childCanvas = transform.Find("Canvas");
        childCanvas.gameObject.SetActive(false);
    }

    public void OnHoverEnter()// Û±Í–¸Õ£
    {
        buttonRenderer.material.color = hoverColor;
        childCanvas.gameObject.SetActive(true);
    }

    public void OnHoverExit()
    {
        buttonRenderer.material.color = defaultColor;
        childCanvas.gameObject.SetActive(false);
    }


    /// <summary>
    /// 
    /// called by playerController, Press L to trigger
    /// </summary>
    public void OnSelect()
    {
        OnPushButton.Invoke();
    }

}
