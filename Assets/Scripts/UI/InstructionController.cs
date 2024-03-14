using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private Timer timer;

    private PlayerInput playerInput;

    Transform[] instructionList;

    private int currentIndex = -1;

    [SerializeField] private int[] continueIndex;
    [SerializeField] private int breifing1EndIndex;
    [SerializeField] private int End1Index;

    private bool isActive;
    [SerializeField] private int totalInstructions = 9;


    public Action endGame;

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
        instructionList[++currentIndex].gameObject.SetActive(true);
       
        isActive = true;
    }

    public void DisableInstruction()
    {
        instructionList[currentIndex].gameObject.SetActive(false);
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
                if (continueIndex.Contains(currentIndex))
                {
              
                    ShowNextInstruction();
                }
                else if (currentIndex == breifing1EndIndex)
                {
                    briefing1.EndLevel();
                }
                else if (currentIndex == End1Index)
                {
                    Debug.Log("end in instruction");
                    endGame?.Invoke();
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
