using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager[] levels;
    [SerializeField] private Timer timer;

    public static GameManager Instance;

    private GameState currentState;
    private bool isInputActive = true;

    private LevelManager currentLevel;
    private int currentLevelIndex;

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


    private void OnEnable()
    {
        timer.OnTimeOut += GameLose;
    }
    // Start is called before the first frame update
    void Start()
    {

        //Go to the briefing state of the game
        if (levels.Length > 0)
        {
            UpdateState(GameState.Briefing, levels[currentLevelIndex]);
        }
    }

    // Update is called once per frame
    void Update()
    {

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
    }

    private void GameLose()
    {
        Debug.Log("Game ended and u lose");
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
