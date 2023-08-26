using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float enemyBulletSpeed = 10f; // 弾の移動速度
    public float maxDistance = 10f; // 弾の最大飛距離
    public float destroyDelay = 2f; // 弾が消えるまでの時間

    private Transform target; // 追尾するターゲット（プレイヤー）

    // 弾が生成されたときにターゲットを設定するメソッド
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    private void Update()
    {
        // ターゲットが存在しない場合は弾を消す
        if (target == null)
        {
            Destroy(gameObject, destroyDelay);
            return;
        }

        // 弾の移動処理（ターゲットの方向に向かって追尾する）
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * enemyBulletSpeed * Time.deltaTime;

        // 弾が一定距離以上飛んだら消す
        if (Vector3.Distance(transform.position, target.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 弾がプレイヤーに当たった場合の処理（ダメージを与えるなど）
        if (other.CompareTag("Player"))
        {
            // ダメージ処理などを記述
            Destroy(gameObject);
        }
    }
}

