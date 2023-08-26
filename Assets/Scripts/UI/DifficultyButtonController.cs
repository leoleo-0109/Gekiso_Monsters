using UnityEngine;

public class DifficultyButtonController : MonoBehaviour
{
    [SerializeField] private StaminaManager staminaManager;

    [SerializeField] private GameObject kiwamiQuest;
    [SerializeField] private GameObject ultimateQuest;
    [SerializeField] private GameObject superUltimateQuest;

    [SerializeField] private GameObject cannotKiwamiQuest;
    [SerializeField] private GameObject cannotUltimateQuest;
    [SerializeField] private GameObject cannotSuperUltimateQuest;

    private void Start()
    {
        if(StaminaManager.currentStamina < 50)
        {
            ultimateQuest.SetActive(false);
            superUltimateQuest.SetActive(false);
            cannotUltimateQuest.SetActive(true);
            cannotSuperUltimateQuest.SetActive(true);

            if(StaminaManager.currentStamina < 35)
            {
                kiwamiQuest.SetActive(false);
                cannotKiwamiQuest.SetActive(true);
            }
        }
    }
}
