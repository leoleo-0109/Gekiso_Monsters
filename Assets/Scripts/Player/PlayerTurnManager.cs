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
        // �Q�[���J�n���̂݃v���C���[�S���̃A�C�R����\������
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

                // �v���C���[�̓������I��������A�G�̍U�����J�n
                if (enemyTurnFlag)
                {
                    for (int enemyValue = 0; enemyValue < enemyList.Count; enemyValue++)
                    {
                        // �G������ł�����A�G�̒e�͐������Ȃ�
                        if (enemyHpControllerList[enemyValue].deadFlag)
                        {
                            Debug.Log("�G" + (enemyValue + 1) + "��|����");
                        }
                        // �G�������c���Ă�����A�G�̒e�𐶐�����
                        else
                        {
                            enemyList[enemyValue].SetEnemyBullet();
                        }
                    }
                }

                // �{�X��ɓ˓�������
                if (stageType == StageType.bossStage)
                {
                    bossTurnFlag = true;
                    // �{�X�̍U�����J�n
                    if (bossTurnFlag)
                    {
                        // �{�X������ł�����A�{�X�̒e�͐������Ȃ�
                        if (bossHpController.currentBossHp <= 0)
                        {
                            Debug.Log("�{�X��|����");
                        }
                        // �{�X�������c���Ă�����A�{�X�̒e�𐶐�����
                        else
                        {
                            boss.SetBossBullet();
                        }
                    }
                    // �X�s�[�h�N���A�^�[���̕\�����J�E���g�_�E��
                    if (turnCountFlag)
                    {
                        speedClearTurnController.TurnCountDown();
                    }
                    turnCountFlag = true;
                }

                //await UniTask.Delay(TimeSpan.FromSeconds(2f)); // �ҋ@����
                WaitTime().Forget();

                if (playerValue < playerList.Count - 1)
                {
                    playerValue++;
                }
                else
                {
                    playerValue = 0;
                }

                // �v���C���[�A�C�R���̕\�����X�V
                UpdatePlayerIcon();

                // ���̃v���C���[�̃^�[�����J�n
                nextOperation.OnNext(Unit.Default);
            }).AddTo(disposables);
    }

    private void UpdatePlayerIcon()
    {
        // �e�v���C���[�A�C�R����\�������邱�ƂŌ��݂̃v���C���[������������
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
        await UniTask.Delay(TimeSpan.FromSeconds(2f)); // �ҋ@����
    }

    private async UniTask WaitTime()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
    }
}
