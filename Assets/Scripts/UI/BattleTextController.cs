using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI battleText;

    public void BattleCountUp(float currentStageIndex)
    {
        currentStageIndex++;
        battleText.text = currentStageIndex.ToString();
    }
}
