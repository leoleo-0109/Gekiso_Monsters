using UnityEngine;

public class DifficultyButtonController : MonoBehaviour
{
    [SerializeField] private CanvasController canvasController;
    [SerializeField] private StaminaManager staminaManager;

    [SerializeField] private GameObject kiwamiQuest;
    [SerializeField] private GameObject ultimateQuest;
    [SerializeField] private GameObject superUltimateQuest;

    [SerializeField] private GameObject cannotKiwamiQuest;
    [SerializeField] private GameObject cannotUltimateQuest;
    [SerializeField] private GameObject cannotSuperUltimateQuest;

    [SerializeField] private GameObject staminaRecoveryYesButton;
    [SerializeField] private GameObject cannotStaminaRecoveryYesButton;


    private void Start()
    {
        if (StaminaManager.currentStamina < 50)
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

    public void CannotKiwamiButton()
    {
        canvasController.staminaRecoveryCanvas.SetActive(true);
    }

    public void CannotUltimateButton()
    {
        canvasController.staminaRecoveryCanvas.SetActive(true);
    }

    public void CannotSuperUltimateButton()
    {
        canvasController.staminaRecoveryCanvas.SetActive(true);
    }

    public void ContinueDifficultyButton()
    {
        cannotKiwamiQuest.SetActive(false);
        cannotUltimateQuest.SetActive(false);
        cannotSuperUltimateQuest.SetActive(false);
        staminaRecoveryYesButton.SetActive(false);

        kiwamiQuest.SetActive(true);
        ultimateQuest.SetActive(true);
        superUltimateQuest.SetActive(true);
        cannotStaminaRecoveryYesButton.SetActive(true);
    }
}
