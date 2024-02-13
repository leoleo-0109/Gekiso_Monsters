using System.Collections;
using UnityEngine;
using UniRx;
using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private static BackgroundScroller Instance; // �V���O���g��
    [SerializeField] private PlayerTurnManager playerTurnManager;
    [SerializeField] private PlayerHpController playerHpController;
    [SerializeField] private SEController seController;

    public List<Player> playerList = new List<Player>();

    public float scrollSpeed = 2.0f; // �w�i�̃X�N���[�����x
    public float stopPositionY = -5.7f; // ��~����Y���W
    public bool scrolling = false;
    private int enemiesCount; // �G�̐����Ǘ�����ϐ�

    private Vector3 startPosition; // �����ʒu
    private Transform backgroundContainer; // �w�i�̃R���e�i
    public Subject<Unit> scroll = new();

    private void Awake()
    {
        backgroundContainer = transform; // �R���e�i��Transform���擾

        // �����ʒu���L��
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
        // �X�N���[������Y���W��ω�������
        if (scrolling)
        {
            // �ォ�牺�ɃX�N���[�����邽�߁A�w�i��Y���W�����炷
            backgroundContainer.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

            // ����̈ʒu�Œ�~
            if (backgroundContainer.position.y <= (startPosition.y + stopPositionY))
            {
                startPosition = backgroundContainer.position;
                scrolling = false;
                ScrollBackgroundAndSpawnNextMap().Forget();
                // �}�b�v�N���A�� {�v�Z���F(MaxHP - ���݂�HP) / 2}
                seController.TakeHealSEPlay();
                playerHpController.currentPlayerHp += (playerHpController.playerMaxHp - playerHpController.currentPlayerHp) / 2;
                playerHpController.playerHpGauge.value = playerHpController.currentPlayerHp;
                playerHpController.playerHpText.text = playerHpController.currentPlayerHp.ToString();
            }
        }
    }

    public async UniTask BeforeCheckAllEnemiesDefeated()
    {
        // �G�̐����X�V
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesCount = enemies.Length;

        // �G��S�ł������ꍇ�ɔw�i���X�N���[��������
        if (enemiesCount == 0 && !scrolling)
        {
            playerList[(playerTurnManager.playerValue + 1) % 4].shotFlag = false;
            // �v���C���[�̑��x���~�܂�����A�w�i��ʂ��X�N���[������
            await UniTask.Delay(TimeSpan.FromSeconds(6f)); // �ҋ@����
            scrolling = true;
        }
    }

    public void CheckAllEnemiesDefeated()
    {
        BeforeCheckAllEnemiesDefeated().Forget();
    }

    private async UniTask ScrollBackgroundAndSpawnNextMap()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f)); // �ҋ@����
        scroll.OnNext(Unit.Default);
    }
}
