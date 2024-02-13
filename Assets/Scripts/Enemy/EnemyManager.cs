using System.Collections;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private BackgroundScroller backgroundScroller;
    [SerializeField] private CanvasController canvasController;
    [SerializeField] private BattleTextController battleTextController;
    [SerializeField] private WaitDelayTime waitDelayTime;
    //public GameObject enemyHpCanvas;
    //[SerializeField] private EnemyHpCanvasController enemyHpCanvasController;
    private GimmickManager gimmickManager;
    private BGMController bgmController;

    public GameObject[] stage1Enemies; // ステージ1の敵のプレハブ配列
    public GameObject[] stage2Enemies; // ステージ2の敵のプレハブ配列
    public GameObject[] stage3Enemies; // ステージ3の敵のプレハブ配列
    public GameObject[] stage4Enemies; // ステージ4の敵のプレハブ配列
    public GameObject[] stage5Enemies; // ステージ5の敵のプレハブ配列
    public GameObject[] stage6Enemies; // ステージ6の敵のプレハブ配列

    public GameObject[] currentStageEnemies{ get; private set; } // 現在のステージの敵を格納する配列
    public int currentStageIndex = 0; // 現在のステージのインデックス
    private int currentEnemyIndex = 0; // 現在のステージの敵のインデックス
    [SerializeField] private int currentBossIndex = 3;
    private int enemiesDefeated = 0; // 現在のステージで倒した敵の数

    private bool scrollingBackground = false; // 背景をスクロール中かどうか

    private void Awake()
    {
        bgmController = GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMController>();
        gimmickManager = GetComponent<GimmickManager>();
        //enemyHpCanvasController.enemyHpCanvas.SetActive(false);
    }

    private void Start()
    {
        PlayerTurnManager.stageType = StageType.enemyStage;
        // 最初のステージの敵をセットアップ
        battleTextController.BattleCountUp(currentStageIndex).Forget();
        SetStageEnemies();
        backgroundScroller.scroll.Subscribe(x => {
            SetStageEnemies();
        }).AddTo(gameObject);
    }

    private void Update()
    {
        // 敵が全滅したら次のステージに進む
        if (enemiesDefeated == currentStageEnemies.Length)
        {
            gimmickManager.StageGimmickDestroy().Forget();
            currentStageIndex++;
            battleTextController.BattleCountUp(currentStageIndex).Forget();
            //if (currentStageIndex == 3)
            //{
            //    waitDelayTime.WaitTime().Forget();
            //    enemyHpCanvasController.enemyHpCanvas.SetActive(true);
            //}
            //else
            //{
            //    enemyHpCanvasController.enemyHpCanvas.SetActive(false);
            //}
            if (currentBossIndex == currentStageIndex)
            {
                PlayerTurnManager.stageType = StageType.bossStage;
                canvasController.BossAttention();
                bgmController.ChangeBGM();
            }
            
            backgroundScroller.CheckAllEnemiesDefeated();
            enemiesDefeated = 0;
        }
    }


    private void SetStageEnemies()
    {
        
        switch (currentStageIndex)
        {
            case 0:
                currentStageEnemies = stage1Enemies;
                break;
            case 1:
                currentStageEnemies = stage2Enemies;
                break;
            case 2:
                currentStageEnemies = stage3Enemies;
                break;
            case 3:
                currentStageEnemies = stage4Enemies;
                break;
            case 4:
                currentStageEnemies = stage5Enemies;
                break;
            case 5:
                currentStageEnemies = stage6Enemies;
                break;
            default:
                break;
        }

        enemiesDefeated = 0;
        currentEnemyIndex = 0;
        foreach (GameObject enemyPrefab in currentStageEnemies)
        {
            enemyPrefab.SetActive(true);
            if (enemyPrefab.CompareTag("Boss"))
            {
                BossHpController bossHpController = enemyPrefab.GetComponent<BossHpController>();
                bossHpController.BossHpShow();
                bossHpController.BossDeadFlag().Subscribe(x =>
                {
                    if(bossHpController.bossType == BossType.boss)
                    {
                        for (int i = 0; i < currentStageEnemies.Length; i++)
                        {
                            GameObject enemy = currentStageEnemies[i];
                            if (enemy != null && enemy != enemyPrefab)
                            {
                                EnemyHpController enemyHpController = enemy.GetComponent<EnemyHpController>();
                                enemyHpController.EnemyDestroy();
                            }
                        }
                    }
                    else
                    {
                        enemiesDefeated++;
                    }
                    bossHpController.BossDestroy();
                });
            }
            else
            {
                EnemyHpController enemyHpController = enemyPrefab.GetComponent<EnemyHpController>();
                enemyHpController.enemyDeadFlag().Subscribe(x =>
                {
                    enemiesDefeated++;
                    enemyHpController.EnemyDestroy();
                });
            }
        }
        gimmickManager.SetStageGimmick(currentStageIndex); // ギミックの表示
    }

    private async UniTask WaitTime()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(9f)); // 待機処理
    }
}

