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
    float enemyXPos; // 敵の初期X座標を保存する変数
    float enemyYPos; // 敵の初期Y座標を保存する変数

    void Start()
    {
        _rand = Random.value * Mathf.PI * 2;
        enemyXPos = transform.localPosition.x; // 敵の初期X座標を取得
        enemyYPos = transform.localPosition.y; // 敵の初期Y座標を取得
    }

    void Update()
    {
        _elapsed += Time.unscaledDeltaTime * floatingSpeed;
        // Y座標にSin関数を使ってフワフワ動かす
        sprite.transform.localPosition = new Vector3(enemyXPos, enemyYPos + Mathf.Sin(_elapsed) * floatingRange, 0);
    }
}

