using UnityEngine;
using TMPro;

public class AddGoldSceneController : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    [SerializeField] private float currentGold;

    private void Start()
    {
        currentGold = PlayerPrefs.GetFloat("CurrentGold", 0);
        string nowGold = currentGold.ToString("N0");
        goldText.text = nowGold;
    }
}
