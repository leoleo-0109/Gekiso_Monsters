using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float enemyBulletSpeed = 10f; // �e�̈ړ����x
    [Header("�N�G�X�g��Փx(�ɁF1�`3�F������)"),SerializeField] private float questTypeNum = default; 
    [SerializeField] private float[] enemyBulletDamages = { 3000, 4000, 5000 };

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
                            other.GetComponent<PlayerHpController>().PlayerTakeDamage(enemyBulletDamages[0]);
                            break;
                        case 2:
                            other.GetComponent<PlayerHpController>().PlayerTakeDamage(enemyBulletDamages[1]);
                            break;
                        case 3:
                            other.GetComponent<PlayerHpController>().PlayerTakeDamage(enemyBulletDamages[2]);
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    Debug.Log(other.gameObject + "���G����̃_���[�W���󂯂�");
                }
            }
            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }

    public void ShotEnemyBulletToPlayer(GameObject target)
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        // �^�[�Q�b�g�̕������v�Z���A���˂���
        Vector3 direction = (target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * enemyBulletSpeed;
    }
}

