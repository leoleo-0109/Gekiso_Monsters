using UnityEngine;
using UnityEngine.UI;

public class BossHpDestroy : MonoBehaviour
{
    public Slider bossHpGauge;

    public void BossHpBarDestroy()
    {
        bossHpGauge.gameObject.SetActive(false);
    }
}
