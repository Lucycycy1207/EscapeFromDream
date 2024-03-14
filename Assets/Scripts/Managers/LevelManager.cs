using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool isFinalLevel = false;
    public UnityEvent OnLevelStart;
    public UnityEvent OnLevelEnd;

    public void StartLevel()
    {
        OnLevelStart?.Invoke();
    }

    public void EndLevel()
    {
        OnLevelEnd?.Invoke();

        if (isFinalLevel)
        {
            //TODO: Let game manager know to change game state to Game end
            GameManager.GetInstance().UpdateState(GameManager.GameState.GameEnd, this);
        }
        else
        {
            //TODO: change game state to level end
            GameManager.GetInstance().UpdateState(GameManager.GameState.LevelEnd, this);
        }
    }
}
