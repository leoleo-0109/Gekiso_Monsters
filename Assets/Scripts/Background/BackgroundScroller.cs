using System.Collections;
using UnityEngine;
using UniRx;
using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private static BackgroundScroller Instance; // シングルトン
    [SerializeField] private PlayerTurnManager playerTurnManager;
    [SerializeField] private PlayerHpController playerHpController;
    [SerializeField] private SEController seController;

    public List<Player> playerList = new List<Player>();

    public float scrollSpeed = 2.0f; // 背景のスクロール速度
    public float stopPositionY = -5.7f; // 停止するY座標
    public bool scrolling = false;
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
                ScrollBackgroundAndSpawnNextMap().Forget();
                // マップクリア回復 {計算式：(MaxHP - 現在のHP) / 2}
                seController.TakeHealSEPlay();
                playerHpController.currentPlayerHp += (playerHpController.playerMaxHp - playerHpController.currentPlayerHp) / 2;
                playerHpController.playerHpGauge.value = playerHpController.currentPlayerHp;
                playerHpController.playerHpText.text = playerHpController.currentPlayerHp.ToString();
            }
        }
    }

    public async UniTask BeforeCheckAllEnemiesDefeated()
    {
        // 敵の数を更新
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesCount = enemies.Length;

        // 敵を全滅させた場合に背景をスクロールさせる
        if (enemiesCount == 0 && !scrolling)
        {
            playerList[(playerTurnManager.playerValue + 1) % 4].shotFlag = false;
            // プレイヤーの速度が止まったら、背景画面をスクロールする
            await UniTask.Delay(TimeSpan.FromSeconds(6f)); // 待機処理
            scrolling = true;
        }
    }

    public void CheckAllEnemiesDefeated()
    {
        BeforeCheckAllEnemiesDefeated().Forget();
    }

    private async UniTask ScrollBackgroundAndSpawnNextMap()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f)); // 待機処理
        scroll.OnNext(Unit.Default);
    }
}
