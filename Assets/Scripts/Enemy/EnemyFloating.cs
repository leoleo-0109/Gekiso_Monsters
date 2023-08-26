using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloating : MonoBehaviour
{
    [SerializeField] EnemyGroup enemyGroup;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float floatingRange = 0.05f;
    [SerializeField] float floatingSpeed = 2f;
    float _elapsed;
    float _rand;
    float enemyXPos; // �G�̏���X���W��ۑ�����ϐ�
    float enemyYPos; // �G�̏���Y���W��ۑ�����ϐ�

    void Start()
    {
        _rand = Random.value * Mathf.PI * 2;
        enemyXPos = transform.localPosition.x; // �G�̏���X���W���擾
        enemyYPos = transform.localPosition.y; // �G�̏���Y���W���擾
    }

    void Update()
    {
        _elapsed += Time.unscaledDeltaTime * floatingSpeed;
        // Y���W��Sin�֐����g���ăt���t��������
        sprite.transform.localPosition = new Vector3(enemyXPos, enemyYPos + Mathf.Sin(_elapsed) * floatingRange, 0);
    }
}

