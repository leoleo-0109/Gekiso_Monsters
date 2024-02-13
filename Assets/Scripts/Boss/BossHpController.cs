using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public enum BossType
{
    middleBoss,
    boss
};

public enum QuestType
{
    kiwami,
    ultimate,
    superUltimate
};

public class BossHpController : MonoBehaviour
{
    [SerializeField] private SEController seController;
    [SerializeField] private BossHpCanvasController bossHpCanvasController;
    [SerializeField] private BossDead bossDead;
    [SerializeField] private BossHpDestroy bossHpDestroy;
    [SerializeField] private CanvasController canvasController;
    [SerializeField] private GoldController goldController;
    public BossType bossType;
    public QuestType questType;
    [SerializeField] private GameObject bossHpCanvas;
    [SerializeField] private float bossMaxHp; // �{�XHP�̍ő�l
    public float currentBossHp; // �{�X�̌��݂�HP
    [Header("�v���C���[����̔�_���[�W��")] public float playerDamage = 10f;
    public Slider bossHpGauge;
    private bool takeDamageSEFlag = false;

    public Subject<Unit> isBossDead = new();

    //[SerializeField] private AudioClip se;

    private void Awake()
    {
        currentBossHp = bossMaxHp; // HP���ő�l�ŏ�����
        bossHpGauge.maxValue = bossMaxHp; // HP�̍ő�l��ݒ�
        UpdateBossHpUI();
    }

    public void BossTakeDamage(float damage)
    {
        //AudioSource.PlayClipAtPoint(se, transform.position);
        takeDamageSEFlag = true;
        if (takeDamageSEFlag)
        {
            seController.TakeDamageSEPlay();
            takeDamageSEFlag = false;
        }
        currentBossHp -= damage;
        UpdateBossHpUI();

        if (currentBossHp <= 0)
        {
            seController.DestroyBossSEPlay();
            isBossDead.OnNext(Unit.Default);
        }
    }

    private void UpdateBossHpUI()
    {
        bossHpGauge.value = currentBossHp;
    }

    public IObservable<Unit> BossDeadFlag()
    {
        return isBossDead;
    }

    public void BossDestroy()
    {
        bossDead.BossDestroy(); // �{�X������
        isBossDead.Dispose();
        bossHpDestroy.BossHpBarDestroy(); // �{�X��HP�o�[������
        if (bossType == BossType.boss)
        {
            if (questType == QuestType.kiwami)
            {
                goldController.KiwamiBossDeadGetGold();
            }
            else if (questType == QuestType.ultimate)
            {
                goldController.UltimateBossDeadGetGold();
            }
            else if (questType == QuestType.superUltimate)
            {
                goldController.SuperUltimateBossDeadGetGold();
            }
            canvasController.QuestStageClear(); // �N���ACanvas��\������
        }
    }

    public void BossHpShow()
    {
        bossHpCanvas.SetActive(true);
    }
}
