using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 20f;

    public Text timerText;
    public TextMeshProUGUI resultText;

    private float currentTime;
    private bool isGameOver = false;
    private bool hasWon = false;

    void Start()
    {
        currentTime = totalTime;

        if (resultText != null)
        {
            resultText.gameObject.SetActive(false);
        }

        //UpdateTimerUI();
    }

    void Update()
    {
        if (isGameOver || hasWon) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            isGameOver = true;

            if (resultText != null)
            {
                //resultText.gameObject.SetActive(true);
                //resultText.text = "Time Up";
                
            }
        }

        //UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + currentTime.ToString("F1");
        }
    }

    public bool IsTimeRemaining()
    {
        return currentTime > 0f;
    }

    public void WinGame()
    {
        if (isGameOver || hasWon) return;

        hasWon = true;

        if (resultText != null)
        {
            resultText.gameObject.SetActive(true);
            resultText.text = "You Win";
        }
    }
}