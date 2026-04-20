using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [Header("设置倒计时秒数")]
    public float remainingTime = 60.0f;
    private float initialTime;

    [Header("UI 组件引用")]
    public Text timerText;

    [Header("面板")]
    public GameObject titlePanel;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject losePanel;

    private bool isTimerRunning = false;

    void Start()
    {
        // 游戏开始时自动启动
        initialTime = remainingTime;
        isTimerRunning = true;

        titlePanel.SetActive(true);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);

    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (remainingTime > 0)
            {
               
                remainingTime -= Time.deltaTime;

               
                if (remainingTime < 0) remainingTime = 0;

              
                UpdateTimerDisplay();
            }
            else
            {
             
                remainingTime = 0;
                isTimerRunning = false;
                OnTimerComplete();
            }
        }
    }

    void UpdateTimerDisplay()
    {
       
        timerText.text = remainingTime.ToString("f1");
    }

    void OnTimerComplete()
    {
        Debug.Log("倒计时结束！");
       //暂停
       Time.timeScale = 0f;
       losePanel.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        remainingTime = initialTime;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WinGame()
    {
        Debug.Log("你赢了！");
        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }
}