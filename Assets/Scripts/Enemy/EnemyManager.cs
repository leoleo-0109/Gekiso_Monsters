using System.Collections;
using UnityEngine;
using UniRx;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private BackgroundScroller backgroundScroller;
    private GimmickManager gimmickManager;
    [SerializeField] private CanvasController canvasController;
    [SerializeField] private BattleTextController battleTextController;
    private BGMController bgmController;

    public GameObject[] stage1Enemies; // ステージ1の敵のプレハブ配列
    public GameObject[] stage2Enemies; // ステージ2の敵のプレハブ配列
    public GameObject[] stage3Enemies; // ステージ3の敵のプレハブ配列
    public GameObject[] stage4Enemies; // ステージ4の敵のプレハブ配列
    public GameObject[] stage5Enemies; // ステージ5の敵のプレハブ配列
    public GameObject[] stage6Enemies; // ステージ6の敵のプレハブ配列

    public GameObject[] currentStageEnemies{ get; private set; } // 現在のステージの敵を格納する配列
    private int currentStageIndex = 0; // 現在のステージのインデックス
    private int currentEnemyIndex = 0; // 現在のステージの敵のインデックス
    [SerializeField] private int currentBossIndex = 3;
    private int enemiesDefeated = 0; // 現在のステージで倒した敵の数

    private bool scrollingBackground = false; // 背景をスクロール中かどうか

    private void Awake()
    {
        bgmController = GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMController>();
        gimmickManager = GetComponent<GimmickManager>();
    }

    private void Start()
    {
        PlayerTurnManager.stageType = StageType.enemyStage;
        // 最初のステージの敵をセットアップ
        battleTextController.BattleCountUp(currentStageIndex);
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
            gimmickManager.StageGimmickDestroy();
            currentStageIndex++;
            battleTextController.BattleCountUp(currentStageIndex);
            if (currentBossIndex == currentStageIndex)
            {
                PlayerTurnManager.stageType = StageType.bossStage;
                canvasController.BossAttention();
                bgmController.ChangeBGM();
            }
            // BackgroundScrollerのCheckAllEnemiesDefeatedを呼び出す
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
}

