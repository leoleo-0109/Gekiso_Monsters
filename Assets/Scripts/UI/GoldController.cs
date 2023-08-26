using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GoldController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText = default;
    [SerializeField] private float minGold = 0f;
    [SerializeField] private float currentGold = 0f;
    [SerializeField] private float newGetGold;
    [SerializeField] private float nowGold;

    private void Start()
    {
        nowGold = minGold;
        UpdateGoldUI(); // �S�[���h�̌��ݒl���X�V
    }

    public void KiwamiBossDeadGetGold()
    {
        GoldReset();
        currentGold += 125000;
        UpdateGoldUI();
    }

    public void UltimateBossDeadGetGold()
    {
        GoldReset();
        currentGold += 250000;
        UpdateGoldUI();
    }

    public void SuperUltimateBossDeadGetGold()
    {
        GoldReset();
        currentGold += 500000;
        UpdateGoldUI();
    }


    public void TitleStartButtonClick()
    {
        nowGold = 0;
        PlayerPrefs.SetFloat("CurrentGold", nowGold);
        SceneManager.LoadScene("HomeScene");
    }

    public void KiwamiQuestResultButtonClick()
    {
        float getGold = 125000;
        CountUpGold(getGold);
    }
    public void UltimateQuestResultButtonClick()
    {
        float getGold = 250000;
        CountUpGold(getGold);
    }

    public void SuperUltimateQuestResultButtonClick()
    {
        float getGold = 500000;
        CountUpGold(getGold);
    }

    private void CountUpGold(float gG) // gG�͊l�������S�[���h
    {
        float questGetGold = PlayerPrefs.GetFloat("CurrentGold", 0);
        nowGold = questGetGold + gG;
        PlayerPrefs.SetFloat("CurrentGold", nowGold);
        SceneManager.LoadScene("HomeScene");
    }

    private void UpdateGoldUI()
    {
        goldText.text = currentGold.ToString(); // �e�L�X�g�Ɋl���S�[���h��\��
    }

    public void GoldReset()
    {
        currentGold = 0;
    }
}
