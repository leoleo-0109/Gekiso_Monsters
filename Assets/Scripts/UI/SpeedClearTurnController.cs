using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedClearTurnController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedClearTurnText; // �X�s�N���^�[������
    [SerializeField] private GameObject flontSpeedClearIcon;
    [SerializeField] private GameObject speedClearText; // �X�s�N���̕���
    [SerializeField] private float speedClearTurnMax = 12f; // �X�s�N���̍ő�l�i�ɁF12�A���ɁF16�A�����ɁF18�j
    [SerializeField] private float currentSpeedClearTurn; // �X�s�N���̌��݂̃^�[��

    private void Awake()
    {
        speedClearText.SetActive(false);
        flontSpeedClearIcon.SetActive(true);
        currentSpeedClearTurn = speedClearTurnMax; // �X�s�N�����ő�l�ŏ�����
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
            speedClearTurnText.text = string.Empty; // ��̕������ݒ肵�ăe�L�X�g���폜����
            speedClearText.SetActive(false);
            flontSpeedClearIcon.SetActive(true);
        }
    }
}
