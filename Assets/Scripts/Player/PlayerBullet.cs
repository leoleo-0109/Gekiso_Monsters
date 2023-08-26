using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float playerBulletSpeed = 10f; // 発射オブジェクトの速さ
    [SerializeField] private float playerBulletDamage = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other != null)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyHpController>().EnemyTakeDamage(playerBulletDamage);
            }
            if (other.CompareTag("Boss"))
            {
                other.GetComponent<BossHpController>().BossTakeDamage(playerBulletDamage);
            }
            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }

        }
    }

    public void ShotPlayerBulletToEnemy(GameObject target)
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        // ターゲットの方向を計算し、発射する
        Vector3 direction = (target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * playerBulletSpeed;
    }
}
