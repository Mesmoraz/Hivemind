using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI timerText;
    public float timeRemaining = 60;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            gameManager.GameOver();
            timeRemaining += 60;
        }

        DisplayTime(timeRemaining);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("Time: {00}", seconds);
    }
}
