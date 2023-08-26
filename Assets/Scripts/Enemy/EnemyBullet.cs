using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float enemyBulletSpeed = 10f; // �e�̈ړ����x
    public float maxDistance = 10f; // �e�̍ő�򋗗�
    public float destroyDelay = 2f; // �e��������܂ł̎���

    private Transform target; // �ǔ�����^�[�Q�b�g�i�v���C���[�j

    // �e���������ꂽ�Ƃ��Ƀ^�[�Q�b�g��ݒ肷�郁�\�b�h
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    private void Update()
    {
        // �^�[�Q�b�g�����݂��Ȃ��ꍇ�͒e������
        if (target == null)
        {
            Destroy(gameObject, destroyDelay);
            return;
        }

        // �e�̈ړ������i�^�[�Q�b�g�̕����Ɍ������Ēǔ�����j
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * enemyBulletSpeed * Time.deltaTime;

        // �e����苗���ȏ��񂾂����
        if (Vector3.Distance(transform.position, target.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �e���v���C���[�ɓ��������ꍇ�̏����i�_���[�W��^����Ȃǁj
        if (other.CompareTag("Player"))
        {
            // �_���[�W�����Ȃǂ��L�q
            Destroy(gameObject);
        }
    }
}

