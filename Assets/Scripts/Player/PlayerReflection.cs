using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerReflection : MonoBehaviour
{
    [SerializeField] private PlayerHpController playerHpController;
    [SerializeField] private Player player;
    public List<Player> playerList = new List<Player>();
    private Rigidbody2D rigidbody;
    private Vector2 currentVector2;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        currentVector2 = rigidbody.velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // KiwamiQuestSceneで使用
        if (other.CompareTag("Mine"))
        {
            try
            {
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // ダメージを受けない
                }
                else
                {
                    // ダメージを受ける
                    playerHpController.PlayerTakeDamage(playerHpController.mineDamage); // 地雷を踏んだ時のダメージ処理
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }

        // UltimateQuestSceneで使用
        if (other.CompareTag("Urchin"))
        {
            try
            {
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // ダメージを受けない
                }
                else
                {
                    // ダメージを受ける
                    playerHpController.PlayerTakeDamage(playerHpController.urchinDamage); // ウニを踏んだ時のダメージ処理
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }

        // SuperUltimateQuestSceneで使用
        if (other.CompareTag("Makibishi"))
        {
            try
            {
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // ダメージを受けない
                }
                else
                {
                    // ダメージを受ける
                    playerHpController.PlayerTakeDamage(playerHpController.makibishiDamage); // まきびしを踏んだ時のダメージ処理
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }

        // SuperUltimateQuestSceneで使用
        if (other.CompareTag("DamageWall"))
        {
            try
            {
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // ダメージを受けない
                }
                else
                {
                    // ダメージを受ける
                    playerHpController.PlayerTakeDamage(playerHpController.damageWallDamage); // ダメージウォールに当たった時のダメージ処理
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }

        // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestSceneで使用
        if (other.CompareTag("LargeHeartPanel"))
        {
            try
            {
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // 回復しない
                }
                else
                {
                    // 回復する
                    playerHpController.PlayerTakeHealing(playerHpController.largeHealingQuantity); // 回復パネル(大)を踏んだ時の回復処理
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }

        // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestSceneで使用
        if (other.CompareTag("LessHeartPanel"))
        {
            try
            {
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // 回復しない
                }
                else
                {
                    // 回復する
                    playerHpController.PlayerTakeHealing(playerHpController.lessHealingQuantity); // 回復パネル(小)を踏んだ時の回復処理
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }

        // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestSceneで使用
        if (other.CompareTag("EnemyBullet"))
        {
            playerHpController.PlayerTakeDamage(playerHpController.enemyBulletDamage); // 敵からの攻撃を受けた時のダメージ処理
        }

        // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestSceneで使用
        if (other.CompareTag("BossBullet"))
        {
            playerHpController.PlayerTakeDamage(playerHpController.bossBulletDamage); // ボスからの攻撃を受けた時のダメージ処理
        }

        if (other.CompareTag("Player"))
        {
            try
            {
                // プレイヤー全員が停止している状態でプレイヤーに触れていたら貫通弾を出さない
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // 貫通弾を出さない
                }
                else
                {
                    // 貫通弾を生成
                    other.gameObject.GetComponent<Player>().SetPlayerBullet();
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Block"))
        {
            RigidbodySetReflect(collision);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            RigidbodySetReflect(collision);
            collision.gameObject.GetComponentInChildren<EnemyHpController>().EnemyTakeDamage(player.playerAttack); // 敵に攻撃した(=敵が食らった)時のダメージ処理
        }

        if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("MiddleBoss"))
        {
            RigidbodySetReflect(collision);
            collision.gameObject.GetComponentInChildren<BossHpController>().BossTakeDamage(player.playerAttack); // ボスに攻撃した(=ボスが食らった)時のダメージ処理
        }
    }

    // 当たり判定
    private void RigidbodySetReflect(Collision2D collision)
    {
        Vector2 reflect = Vector2.Reflect(currentVector2, collision.contacts[0].normal);
        rigidbody.velocity = reflect;
    }
}
