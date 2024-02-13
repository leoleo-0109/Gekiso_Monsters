using Unity.VisualScripting;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float bossBulletSpeed = 10f; // 弾の移動速度
    [Header("クエスト難易度(極：1～3：超究極)"), SerializeField] private float questTypeNum = default;
    [SerializeField] private float[] bossBulletDamages = { 5000, 7500, 10000 };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            if (other.CompareTag("Player"))
            {
                try
                {
                    switch (questTypeNum)
                    {
                        case 1:
                            other.GetComponent<PlayerHpController>().PlayerTakeDamage(bossBulletDamages[0]);
                            break;
                        case 2:
                            other.GetComponent<PlayerHpController>().PlayerTakeDamage(bossBulletDamages[1]);
                            break;
                        case 3:
                            other.GetComponent<PlayerHpController>().PlayerTakeDamage(bossBulletDamages[2]);
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    Debug.Log(other.gameObject + "がボスからのダメージを受けた");
                }
            }
            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }

    public void ShotBossBulletToPlayer(GameObject target)
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        // ターゲットの方向を計算し、発射する
        Vector3 direction = (target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * bossBulletSpeed;
    }
}

