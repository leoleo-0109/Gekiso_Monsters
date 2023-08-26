using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedClearTurnController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedClearTurnText; // スピクリターン数字
    [SerializeField] private GameObject flontSpeedClearIcon;
    [SerializeField] private GameObject speedClearText; // スピクリの文字
    [SerializeField] private float speedClearTurnMax = 12f; // スピクリの最大値（極：12、究極：16、超究極：18）
    [SerializeField] private float currentSpeedClearTurn; // スピクリの現在のターン

    private void Awake()
    {
        speedClearText.SetActive(false);
        flontSpeedClearIcon.SetActive(true);
        currentSpeedClearTurn = speedClearTurnMax; // スピクリを最大値で初期化
        UpdateSpeedClearTurnUI();
    }

    public void TurnCountDown()
    {
        currentSpeedClearTurn--;
        UpdateSpeedClearTurnUI();
    }

    private void UpdateSpeedClearTurnUI()
    {
        if (currentSpeedClearTurn > 0)
        {
            speedClearTurnText.text = currentSpeedClearTurn.ToString();
        }
        if (currentSpeedClearTurn < 0)
        {
            speedClearTurnText.text = string.Empty; // 空の文字列を設定してテキストを削除する
            speedClearText.SetActive(false);
            flontSpeedClearIcon.SetActive(true);
        }
    }
}
