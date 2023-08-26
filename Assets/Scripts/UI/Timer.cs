using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText = default;
    private static float countTime;
    private static bool timerFlag;

    private void Start()
    {

    }

    private void Update()
    {
        if (timerText != null)
        {
            UpdateTimerText();
        }
        if (timerFlag)
        {
            countTime += Time.deltaTime;
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(countTime / 60f);
        int seconds = Mathf.FloorToInt(countTime % 60f);
        //int milliseconds = Mathf.FloorToInt((countTime * 1000) % 1000f);

        //string timerString = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        timerText.text = timerString;
    }

    public static void TimerStart()
    {
        timerFlag = true;
        countTime = 0;
    }

    public static void TimerFinish()
    {
        timerFlag = false;
    }
}
