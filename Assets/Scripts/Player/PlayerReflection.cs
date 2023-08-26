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
        // KiwamiQuestScene�Ŏg�p
        if (collision.CompareTag("Mine"))
        {
            playerHpController.PlayerTakeDamage(playerHpController.mineDamage); // �n���𓥂񂾎��̃_���[�W����
        }

        // UltimateQuestScene�Ŏg�p
        if (collision.CompareTag("Urchin"))
        {
            playerHpController.PlayerTakeDamage(playerHpController.urchinDamage); // �E�j�𓥂񂾎��̃_���[�W����
        }

        // SuperUltimateQuestScene�Ŏg�p
        if (collision.CompareTag("Makibishi"))
        {
            playerHpController.PlayerTakeDamage(playerHpController.makibishiDamage); // �܂��т��𓥂񂾎��̃_���[�W����
        }

        // SuperUltimateQuestScene�Ŏg�p
        if (collision.CompareTag("DamageWall"))
        {
            playerHpController.PlayerTakeDamage(playerHpController.damageWallDamage); // �_���[�W�E�H�[���ɓ����������̃_���[�W����
        }

        // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestScene�Ŏg�p
        if (collision.CompareTag("LargeHeartPanel"))
        {
            playerHpController.PlayerTakeHealing(playerHpController.largeHealingQuantity); // �񕜃p�l��(��)�𓥂񂾎��̉񕜏���
        }

        // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestScene�Ŏg�p
        if (collision.CompareTag("LessHeartPanel"))
        {
            playerHpController.PlayerTakeHealing(playerHpController.lessHealingQuantity); // �񕜃p�l��(��)�𓥂񂾎��̉񕜏���
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
            collision.gameObject.GetComponentInChildren<EnemyHpController>().EnemyTakeDamage(player.playerAttack); // �G�ɍU������(=�G���H�����)���̃_���[�W����
        }

        if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("MiddleBoss"))
        {
            RigidbodySetReflect(collision);
            collision.gameObject.GetComponentInChildren<BossHpController>().BossTakeDamage(player.playerAttack); // �{�X�ɍU������(=�{�X���H�����)���̃_���[�W����
        }
    }

    private void RigidbodySetReflect(Collision2D collision)
    {
        Vector2 reflect = Vector2.Reflect(currentVector2, collision.contacts[0].normal);
        rigidbody.velocity = reflect;
    }
}
