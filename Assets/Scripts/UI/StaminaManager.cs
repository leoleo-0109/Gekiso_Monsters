using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaManager : MonoBehaviour
{
    [SerializeField] private Slider staminaGauge;
    [SerializeField] private TextMeshProUGUI staminaText;
    private static float maxStamina = 1000f; // スタミナの最大値
    public static float currentStamina = maxStamina; // 現在のスタミナ

    private void Start()
    {
        staminaGauge.maxValue = maxStamina; // Sliderの最大値を設定
        UpdateStaminaUI(); // スタミナの現在値を更新
    }

    // SuperUltimateSortieSceneで使用
    public void SuperUltimateQuestSortieButtonClick() // 「出撃」ボタン(超究極)
    {
        if (currentStamina >= 50)
        {
            currentStamina -= 50;
            UpdateStaminaUI();
        }
    }

    // UltimateSortieSceneで使用
    public void UltimateQuestSortieButtonClick() // 「出撃」ボタン(究極)
    {
        if (currentStamina >= 50)
        {
            currentStamina -= 50;
            UpdateStaminaUI();
        }
    }

    // KiwamiSortieSceneで使用
    public void KiwamiQuestSortieButtonClick() // 「出撃」ボタン(極)
    {
        if (currentStamina >= 35)
        {
            currentStamina -= 35;
            UpdateStaminaUI();
        }
    }

    private void UpdateStaminaUI()
    {
        staminaGauge.value = currentStamina; // スタミナの現在値を更新
        staminaText.text = currentStamina.ToString() + "/1000"; // テキストにスタミナ量を表示
    }

    public static void ResetStamina()
    {
        currentStamina = maxStamina;
    }
}
