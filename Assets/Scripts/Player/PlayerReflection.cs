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
        // KiwamiQuestScene�Ŏg�p
        if (other.CompareTag("Mine"))
        {
            try
            {
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // �_���[�W���󂯂Ȃ�
                }
                else
                {
                    // �_���[�W���󂯂�
                    playerHpController.PlayerTakeDamage(playerHpController.mineDamage); // �n���𓥂񂾎��̃_���[�W����
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }

        // UltimateQuestScene�Ŏg�p
        if (other.CompareTag("Urchin"))
        {
            try
            {
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // �_���[�W���󂯂Ȃ�
                }
                else
                {
                    // �_���[�W���󂯂�
                    playerHpController.PlayerTakeDamage(playerHpController.urchinDamage); // �E�j�𓥂񂾎��̃_���[�W����
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }

        // SuperUltimateQuestScene�Ŏg�p
        if (other.CompareTag("Makibishi"))
        {
            try
            {
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // �_���[�W���󂯂Ȃ�
                }
                else
                {
                    // �_���[�W���󂯂�
                    playerHpController.PlayerTakeDamage(playerHpController.makibishiDamage); // �܂��т��𓥂񂾎��̃_���[�W����
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }

        // SuperUltimateQuestScene�Ŏg�p
        if (other.CompareTag("DamageWall"))
        {
            try
            {
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // �_���[�W���󂯂Ȃ�
                }
                else
                {
                    // �_���[�W���󂯂�
                    playerHpController.PlayerTakeDamage(playerHpController.damageWallDamage); // �_���[�W�E�H�[���ɓ����������̃_���[�W����
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }

        // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestScene�Ŏg�p
        if (other.CompareTag("LargeHeartPanel"))
        {
            try
            {
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // �񕜂��Ȃ�
                }
                else
                {
                    // �񕜂���
                    playerHpController.PlayerTakeHealing(playerHpController.largeHealingQuantity); // �񕜃p�l��(��)�𓥂񂾎��̉񕜏���
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }

        // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestScene�Ŏg�p
        if (other.CompareTag("LessHeartPanel"))
        {
            try
            {
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // �񕜂��Ȃ�
                }
                else
                {
                    // �񕜂���
                    playerHpController.PlayerTakeHealing(playerHpController.lessHealingQuantity); // �񕜃p�l��(��)�𓥂񂾎��̉񕜏���
                }
            }
            catch
            {
                Debug.Log(other.gameObject);
            }
        }

        // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestScene�Ŏg�p
        if (other.CompareTag("EnemyBullet"))
        {
            playerHpController.PlayerTakeDamage(playerHpController.enemyBulletDamage); // �G����̍U�����󂯂����̃_���[�W����
        }

        // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestScene�Ŏg�p
        if (other.CompareTag("BossBullet"))
        {
            playerHpController.PlayerTakeDamage(playerHpController.bossBulletDamage); // �{�X����̍U�����󂯂����̃_���[�W����
        }

        if (other.CompareTag("Player"))
        {
            try
            {
                // �v���C���[�S������~���Ă����ԂŃv���C���[�ɐG��Ă�����ђʒe���o���Ȃ�
                if (playerList[0].force.magnitude == 0 &&
                    playerList[1].force.magnitude == 0 &&
                    playerList[2].force.magnitude == 0 &&
                    playerList[3].force.magnitude == 0)
                {
                    // �ђʒe���o���Ȃ�
                }
                else
                {
                    // �ђʒe�𐶐�
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
            collision.gameObject.GetComponentInChildren<EnemyHpController>().EnemyTakeDamage(player.playerAttack); // �G�ɍU������(=�G���H�����)���̃_���[�W����
        }

        if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("MiddleBoss"))
        {
            RigidbodySetReflect(collision);
            collision.gameObject.GetComponentInChildren<BossHpController>().BossTakeDamage(player.playerAttack); // �{�X�ɍU������(=�{�X���H�����)���̃_���[�W����
        }
    }

    // �����蔻��
    private void RigidbodySetReflect(Collision2D collision)
    {
        Vector2 reflect = Vector2.Reflect(currentVector2, collision.contacts[0].normal);
        rigidbody.velocity = reflect;
    }
}
