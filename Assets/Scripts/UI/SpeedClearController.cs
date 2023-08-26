using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedClearController : MonoBehaviour
{
    [SerializeField] private Image flontSpeedClearIcon;
    [SerializeField] private Image speedClearText;
    [SerializeField] private TextMeshProUGUI speedClearTurnText;

    public void SpeedClearShow()
    {
        flontSpeedClearIcon.gameObject.SetActive(false);
        speedClearText.gameObject.SetActive(true);
        speedClearTurnText.gameObject.SetActive(true);
    }
}
