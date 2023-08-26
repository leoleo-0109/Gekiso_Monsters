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

    public GameObject[] stage1Enemies; // �X�e�[�W1�̓G�̃v���n�u�z��
    public GameObject[] stage2Enemies; // �X�e�[�W2�̓G�̃v���n�u�z��
    public GameObject[] stage3Enemies; // �X�e�[�W3�̓G�̃v���n�u�z��
    public GameObject[] stage4Enemies; // �X�e�[�W4�̓G�̃v���n�u�z��
    public GameObject[] stage5Enemies; // �X�e�[�W5�̓G�̃v���n�u�z��
    public GameObject[] stage6Enemies; // �X�e�[�W6�̓G�̃v���n�u�z��

    public GameObject[] currentStageEnemies{ get; private set; } // ���݂̃X�e�[�W�̓G���i�[����z��
    private int currentStageIndex = 0; // ���݂̃X�e�[�W�̃C���f�b�N�X
    private int currentEnemyIndex = 0; // ���݂̃X�e�[�W�̓G�̃C���f�b�N�X
    [SerializeField] private int currentBossIndex = 3;
    private int enemiesDefeated = 0; // ���݂̃X�e�[�W�œ|�����G�̐�

    private bool scrollingBackground = false; // �w�i���X�N���[�������ǂ���

    private void Awake()
    {
        bgmController = GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMController>();
        gimmickManager = GetComponent<GimmickManager>();
    }

    private void Start()
    {
        PlayerTurnManager.stageType = StageType.enemyStage;
        // �ŏ��̃X�e�[�W�̓G���Z�b�g�A�b�v
        battleTextController.BattleCountUp(currentStageIndex);
        SetStageEnemies();
        backgroundScroller.scroll.Subscribe(x => {
            SetStageEnemies();
        }).AddTo(gameObject);
    }

    private void Update()
    {
        // �G���S�ł����玟�̃X�e�[�W�ɐi��
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
            // BackgroundScroller��CheckAllEnemiesDefeated���Ăяo��
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
        gimmickManager.SetStageGimmick(currentStageIndex); // �M�~�b�N�̕\��
    }
}

