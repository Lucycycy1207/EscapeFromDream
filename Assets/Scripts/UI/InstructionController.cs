using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstructionController : MonoBehaviour
{
    private Image backGroundImg;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private TextMeshProUGUI continueUI;
    [SerializeField] private LevelManager briefing1;
    [SerializeField] private LevelManager briefingEnd1;
    private PlayerInput playerInput;

    Transform[] instructionList;

    private int currentIndex;

    private bool isActive;
    [SerializeField] private int totalInstructions = 9;

    private void Awake()
    {
        backGroundImg = GetComponent<Image>();
        playerInput = PlayerInput.GetInstance();
    }

    void Start()
    {
        backGroundImg.enabled = false;
        continueUI.enabled = false;
        instructionList = new Transform[totalInstructions];

        for (int i = 0; i < totalInstructions; i++)
        {
            instructionList[i] = this.transform.GetChild(i);
        }
    }

    public void ShowNextInstruction()
    {
        uIManager.SetInGameUI(false);
        playerInput.SetInputStatus(false);
        backGroundImg.enabled = true;
        continueUI.enabled = true;
        instructionList[currentIndex].gameObject.SetActive(true);
        isActive = true;
    }

    public void DisableInstruction()
    {
        instructionList[currentIndex++].gameObject.SetActive(false);
        continueUI.enabled = false;
        backGroundImg.enabled = false;
        playerInput.SetInputStatus(true);
        uIManager.SetInGameUI(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.anyKeyDown)
            {
                DisableInstruction();
                isActive = false;
                if (currentIndex == 1)
                {
                    ShowNextInstruction();
                }
                else if (currentIndex == 2)
                {
                    briefing1.EndLevel();
                }
                else if (currentIndex == 7)
                {
                    briefingEnd1.EndLevel();
                }
            }
        }

    }

    enum Instructions
    {
        T1,
        T2,
        maze,
        mirror,
        mirror1,
        staircase,
        staircase1,
        End,
        End1
    }
}
