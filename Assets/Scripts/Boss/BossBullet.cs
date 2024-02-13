using Unity.VisualScripting;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float bossBulletSpeed = 10f; // �e�̈ړ����x
    [Header("�N�G�X�g��Փx(�ɁF1�`3�F������)"), SerializeField] private float questTypeNum = default;
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
                    Debug.Log(other.gameObject + "���{�X����̃_���[�W���󂯂�");
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
        // �^�[�Q�b�g�̕������v�Z���A���˂���
        Vector3 direction = (target.transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * bossBulletSpeed;
    }
}

