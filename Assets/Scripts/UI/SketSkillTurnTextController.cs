using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SketSkillTurnTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sketSkillTurnText;

    public void SketSkillTurnCountUp(float currentSketSkillTurnIndex)
    {
        sketSkillTurnText.text = (currentSketSkillTurnIndex).ToString();

        if(currentSketSkillTurnIndex > 5)
        {
            currentSketSkillTurnIndex = 5;
            sketSkillTurnText.text = (currentSketSkillTurnIndex).ToString();
        }
    }
}
