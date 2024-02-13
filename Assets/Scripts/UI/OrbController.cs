using UnityEngine;
using TMPro;

public class OrbController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI orbTextHeader;
    [SerializeField] private TextMeshProUGUI orbTextStaminRecovery;
    private static float maxOrb = 100f; // 虹珠の最大値
    public static float currentOrb = maxOrb; // 現在の虹珠数

    public void Start()
    {
        //currentOrb = maxOrb;
        UpdateOrbUI(); // 虹珠数の現在値を更新
    }

    public void UseOrb()
    {
        currentOrb--;
        UpdateOrbUI();
    }

    public static void ResetOrb()
    {
        currentOrb = maxOrb;
    }

    private void UpdateOrbUI()
    {
        orbTextHeader.text = currentOrb.ToString(); // テキストに虹珠数を表示
        orbTextStaminRecovery.text = currentOrb.ToString(); // テキストに虹珠数を表示
    }
}
