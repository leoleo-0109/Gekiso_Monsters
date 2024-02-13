using UnityEngine;
using UnityEngine.UI;

public class EnemyDead : MonoBehaviour
{
    //public Slider enemyHpGauge;

    public void EnemyDestroy()
    {
        Destroy(gameObject);
    }
}
