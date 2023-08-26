using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaManager : MonoBehaviour
{
    [SerializeField] private Slider staminaGauge;
    [SerializeField] private TextMeshProUGUI staminaText;
    private static float maxStamina = 1000f; // �X�^�~�i�̍ő�l
    public static float currentStamina = maxStamina; // ���݂̃X�^�~�i

    private void Start()
    {
        staminaGauge.maxValue = maxStamina; // Slider�̍ő�l��ݒ�
        UpdateStaminaUI(); // �X�^�~�i�̌��ݒl���X�V
    }

    // SuperUltimateSortieScene�Ŏg�p
    public void SuperUltimateQuestSortieButtonClick() // �u�o���v�{�^��(������)
    {
        if (currentStamina >= 50)
        {
            currentStamina -= 50;
            UpdateStaminaUI();
        }
    }

    // UltimateSortieScene�Ŏg�p
    public void UltimateQuestSortieButtonClick() // �u�o���v�{�^��(����)
    {
        if (currentStamina >= 50)
        {
            currentStamina -= 50;
            UpdateStaminaUI();
        }
    }

    // KiwamiSortieScene�Ŏg�p
    public void KiwamiQuestSortieButtonClick() // �u�o���v�{�^��(��)
    {
        if (currentStamina >= 35)
        {
            currentStamina -= 35;
            UpdateStaminaUI();
        }
    }

    private void UpdateStaminaUI()
    {
        staminaGauge.value = currentStamina; // �X�^�~�i�̌��ݒl���X�V
        staminaText.text = currentStamina.ToString() + "/1000"; // �e�L�X�g�ɃX�^�~�i�ʂ�\��
    }

    public static void ResetStamina()
    {
        currentStamina = maxStamina;
    }
}
