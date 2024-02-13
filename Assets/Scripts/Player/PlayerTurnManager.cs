using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

public enum StageType
{
    enemyStage,
    bossStage
};

public class PlayerTurnManager : MonoBehaviour
{
    public List<Player> playerList = new List<Player>();
    [SerializeField] private SpeedClearTurnController speedClearTurnController;
    [SerializeField] private GameObject player1IconFilter;
    [SerializeField] private GameObject player2IconFilter;
    [SerializeField] private GameObject player3IconFilter;
    [SerializeField] private GameObject player4IconFilter;

    public List<EnemyHpController> enemyHpControllerList = new List<EnemyHpController>();
    public List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] private BossHpController bossHpController;
    [SerializeField] private Boss boss;
    [SerializeField] private SketManager sketManager;

    public int playerValue = 0;
    private Subject<Unit> nextOperation = new Subject<Unit>();
    private CompositeDisposable disposables = new CompositeDisposable();
    [HideInInspector] public static StageType stageType;
    private bool turnCountFlag = false;
    private bool enemyTurnFlag = false;
    private bool bossTurnFlag = false;

    void Start()
    {
        // ゲーム開始時のみプレイヤー全員のアイコンを表示する
        InitPlayerIcon().Forget();
        player1IconFilter.SetActive(false);
        player2IconFilter.SetActive(true);
        player3IconFilter.SetActive(true);
        player4IconFilter.SetActive(true);
        nextOperation.Subscribe(x => ChangePlayer().Forget())
            .AddTo(this);
        nextOperation.OnNext(Unit.Default);
    }

    private async UniTask ChangePlayer()
    {
        disposables.Clear();
        foreach (Player player in playerList)
        {
            player.FriendBulletTrue();
        }
        playerList[playerValue].UpdateStream();
        playerList[playerValue].GetMoveEnd()
            .Subscribe(async x =>
            {
                playerList[playerValue].StopUpdate();
                enemyTurnFlag = true;

                // プレイヤーの動きが終了した後、敵の攻撃を開始
                if (enemyTurnFlag)
                {
                    for (int enemyValue = 0; enemyValue < enemyList.Count; enemyValue++)
                    {
                        // 敵が死んでいたら、敵の弾は生成しない
                        if (enemyHpControllerList[enemyValue].deadFlag)
                        {
                            Debug.Log("敵" + (enemyValue + 1) + "を倒した");
                        }
                        // 敵が生き残っていたら、敵の弾を生成する
                        else
                        {
                            enemyList[enemyValue].SetEnemyBullet();
                        }
                    }
                }

                // ボス戦に突入したら
                if (stageType == StageType.bossStage)
                {
                    bossTurnFlag = true;
                    // ボスの攻撃を開始
                    if (bossTurnFlag)
                    {
                        // ボスが死んでいたら、ボスの弾は生成しない
                        if (bossHpController.currentBossHp <= 0)
                        {
                            Debug.Log("ボスを倒した");
                        }
                        // ボスが生き残っていたら、ボスの弾を生成する
                        else
                        {
                            boss.SetBossBullet();
                        }
                    }
                    // スピードクリアターンの表示＆カウントダウン
                    if (turnCountFlag)
                    {
                        speedClearTurnController.TurnCountDown();
                    }
                    turnCountFlag = true;
                }

                //await UniTask.Delay(TimeSpan.FromSeconds(2f)); // 待機処理
                WaitTime().Forget();

                if (playerValue < playerList.Count - 1)
                {
                    playerValue++;
                }
                else
                {
                    playerValue = 0;
                }

                // プレイヤーアイコンの表示を更新
                UpdatePlayerIcon();

                // 次のプレイヤーのターンを開始
                nextOperation.OnNext(Unit.Default);
            }).AddTo(disposables);
    }

    private void UpdatePlayerIcon()
    {
        // 各プレイヤーアイコンを表示させることで現在のプレイヤーを可視化させる
        switch (playerValue)
        {
            case 0:
                player1IconFilter.SetActive(false);
                player2IconFilter.SetActive(true);
                player3IconFilter.SetActive(true);
                player4IconFilter.SetActive(true);
                break;
            case 1:
                player1IconFilter.SetActive(true);
                player2IconFilter.SetActive(false);
                player3IconFilter.SetActive(true);
                player4IconFilter.SetActive(true);
                break;
            case 2:
                player1IconFilter.SetActive(true);
                player2IconFilter.SetActive(true);
                player3IconFilter.SetActive(false);
                player4IconFilter.SetActive(true);
                break;
            case 3:
                player1IconFilter.SetActive(true);
                player2IconFilter.SetActive(true);
                player3IconFilter.SetActive(true);
                player4IconFilter.SetActive(false);
                break;
            default:
                break;
        }
    }

    private async UniTask InitPlayerIcon()
    {
        player1IconFilter.SetActive(false);
        player2IconFilter.SetActive(false);
        player3IconFilter.SetActive(false);
        player4IconFilter.SetActive(false);
        await UniTask.Delay(TimeSpan.FromSeconds(2f)); // 待機処理
    }

    private async UniTask WaitTime()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
    }
}
