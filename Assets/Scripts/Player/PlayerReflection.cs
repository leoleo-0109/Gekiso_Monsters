using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerReflection : MonoBehaviour
{
    [SerializeField] private PlayerHpController playerHpController;
    [SerializeField] private Player player;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // KiwamiQuestSceneで使用
        if (collision.CompareTag("Mine"))
        {
            playerHpController.PlayerTakeDamage(playerHpController.mineDamage); // 地雷を踏んだ時のダメージ処理
        }

        // UltimateQuestSceneで使用
        if (collision.CompareTag("Urchin"))
        {
            playerHpController.PlayerTakeDamage(playerHpController.urchinDamage); // ウニを踏んだ時のダメージ処理
        }

        // SuperUltimateQuestSceneで使用
        if (collision.CompareTag("Makibishi"))
        {
            playerHpController.PlayerTakeDamage(playerHpController.makibishiDamage); // まきびしを踏んだ時のダメージ処理
        }

        // SuperUltimateQuestSceneで使用
        if (collision.CompareTag("DamageWall"))
        {
            playerHpController.PlayerTakeDamage(playerHpController.damageWallDamage); // ダメージウォールに当たった時のダメージ処理
        }

        // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestSceneで使用
        if (collision.CompareTag("LargeHeartPanel"))
        {
            playerHpController.PlayerTakeHealing(playerHpController.largeHealingQuantity); // 回復パネル(大)を踏んだ時の回復処理
        }

        // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestSceneで使用
        if (collision.CompareTag("LessHeartPanel"))
        {
            playerHpController.PlayerTakeHealing(playerHpController.lessHealingQuantity); // 回復パネル(小)を踏んだ時の回復処理
        }

        if(collision.CompareTag("Player"))
        {
            try
            {
                collision.gameObject.GetComponent<Player>().SetPlayerBullet();
            }
            catch
            {
                Debug.Log(collision.gameObject);
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

    private void RigidbodySetReflect(Collision2D collision)
    {
        Vector2 reflect = Vector2.Reflect(currentVector2, collision.contacts[0].normal);
        rigidbody.velocity = reflect;
    }
}
