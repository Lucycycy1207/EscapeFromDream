using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Timer timer;
    



    [Header("UI Elements")]
    public TMP_Text timerText;
    public TMP_Text healthTxt;

    public GameObject gameOverTxt;

    // Start is called before the first frame update
    void Start()
    {
        gameOverTxt.SetActive(false);
    }

    private void OnEnable()
    {
        playerHealth.OnHealthUpdated += OnHealthUpdate;
        playerHealth.OnDeath += OnDeath;
        timer.OnTimerUpdate += OnTimerUpdate;
        timer.OnTimeOut += OnTimeOut;


    }

    private void OnHealthUpdate(float health)
    {
        healthTxt.text = "Health: " + Mathf.Floor(health).ToString();
    }
    public void OnTimerUpdate(float time)
    {
        timerText.text = "Timer: " + Mathf.Floor(time).ToString();
        
    }

    void OnDeath()
    {
        gameOverTxt.SetActive(true);
    }

    void OnTimeOut()
    {
        gameOverTxt.SetActive(true);
    }

    private void OnDestroy()
    {
        playerHealth.OnHealthUpdated -= OnHealthUpdate;
        timer.OnTimerUpdate -= OnTimerUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
