using System.Collections;
using UnityEngine;
using UniRx;
using System;

public class BackgroundScroller : MonoBehaviour
{
    public static BackgroundScroller Instance; // Singleton

    public float scrollSpeed = 2.0f; // 背景のスクロール速度
    public float stopPositionY = -5.7f; // 停止するY座標
    private bool scrolling = false;
    private int enemiesCount; // 敵の数を管理する変数

    private Vector3 startPosition; // 初期位置
    private Transform backgroundContainer; // 背景のコンテナ
    public Subject<Unit> scroll = new();

    private void Awake()
    {
        backgroundContainer = transform; // コンテナのTransformを取得

        // 初期位置を記憶
        startPosition = backgroundContainer.position;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //// 敵を全員倒した後にスクロールを開始
        //if (AllEnemiesDefeated() && !scrolling)
        //{
        //    scrolling = true;
        //    // 次のマップの敵を出現させる処理を呼び出す
        //    StartCoroutine(ScrollBackgroundAndSpawnNextMap());
        //}

        // スクロール中はY座標を変化させる
        if (scrolling)
        {
            // 上から下にスクロールするため、背景のY座標を減らす
            backgroundContainer.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

            // 特定の位置で停止
            if (backgroundContainer.position.y <= (startPosition.y + stopPositionY))
            {
                startPosition = backgroundContainer.position;
                scrolling = false;
                StartCoroutine(ScrollBackgroundAndSpawnNextMap());
            }
        }
    }

    public void CheckAllEnemiesDefeated()
    {
        // 敵の数を更新
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesCount = enemies.Length;

        // 敵を全滅させた場合に背景をスクロールさせる
        if (enemiesCount == 0 && !scrolling)
        {
            scrolling = true;
            // 次のマップの敵を出現させる処理を呼び出す
        }
    }

    //private void StartNextMap()
    //{
    //    // 背景をスクロールさせる処理を開始
    //    StartCoroutine(ScrollBackgroundAndSpawnNextMap());
    //}

    private IEnumerator ScrollBackgroundAndSpawnNextMap()
    {
        // 少し待機してから次のマップの敵を出現させる
        yield return new WaitForSeconds(1.0f);
        scroll.OnNext(Unit.Default);
    }

    private bool AllEnemiesDefeated()
    {
        // 敵を全員倒したかどうかの判定ロジックを実装
        return enemiesCount == 0;
    }
}
