using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager[] levels;
    [SerializeField] private Timer timer;
    [SerializeField] private TimeLineManager timelineManager;

    public static GameManager Instance;

    private GameState currentState;
    private bool isInputActive = true;

    private LevelManager currentLevel;
    private int currentLevelIndex;

    [SerializeField] private int GameTimer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public static GameManager GetInstance()
    {
        return Instance;
    }

    public bool IsInputActive()
    {
        return isInputActive;
    }

    public int GetGameTimer()
    {
        return GameTimer;
    }

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        timer.OnTimeOut += GameLose;
    }
    

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        PlayerInput.GetInstance().SetCursor(false);
        //Go to the briefing state of the game
        if (levels.Length > 0)
        {
            UpdateState(GameState.Briefing, levels[currentLevelIndex]);
        }
    }

    public void UpdateState(GameState state, LevelManager level)
    {
        currentState = state;
        currentLevel = level;
    
        switch (state)
        {
            case GameState.Briefing:
                StartBriefing(); break;

            case GameState.LevelStart:
                InitLevel(); break;

            case GameState.LevelIn:
                RunLevel(); break;

            case GameState.LevelEnd:
                CompleteLevel(); break;

            case GameState.GameOver:
                GameOver(); break;

            case GameState.GameEnd:
                GameEnd(); break;
        }
    }

    /// <summary>
    /// Disable Player Input
    /// </summary>
    private void StartBriefing()
    {
        Debug.Log($"Briefing Started: {currentLevel.gameObject.name}");

        //Disable Player Input
        isInputActive = false;

        //Start the level
        UpdateState(GameState.LevelStart, currentLevel);
    }

    /// <summary>
    /// StartLevel event is called
    /// </summary>
    private void InitLevel()
    {
        Debug.Log("Level Initialised");
        isInputActive = true;

        currentLevel.StartLevel();
        UpdateState(GameState.LevelIn, currentLevel);

            
    }
    private void RunLevel()
    {
        Debug.Log("Level Running");

        //if (levels.Length > 0)
        //{
        //    ChangeState(GameState.LevelEnd, levels[currentLevelIndex]);
        //}

    }

    private void CompleteLevel()
    {
        Debug.Log($"Level Completed: {currentLevel.gameObject.name} with index {currentLevelIndex}");
     
        //Go to the briefing state of the next level
        if (currentLevelIndex < levels.Length - 1)
        {
            UpdateState(GameState.LevelStart, levels[++currentLevelIndex]);
        }
    }

    private void GameOver()
    {
        Debug.Log("Level game over");

    }

    private void GameEnd()
    {
        Debug.Log("Game ended and u win");
        UIManager.GetInstance().SetInGameUI(false);
        PlayerInput.GetInstance().SetInputStatus(false);
        timelineManager.ActiveTimeLine(2);
    }

    private void GameLose()
    {
        Debug.Log("Game ended and u lose");
        UIManager.GetInstance().SetInGameUI(false);
        PlayerInput.GetInstance().SetInputStatus(false);
        timelineManager.ActiveTimeLine(1);

    }


    public enum GameState
    { 
        Briefing,
        LevelStart,
        LevelIn,
        LevelEnd,
        GameOver,
        GameEnd
    }

}
