using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class EnemyHpController : MonoBehaviour
{
    [SerializeField] private EnemyHpCanvasController enemyHpCanvasController;
    [SerializeField] private EnemyDead enemyDead;
    public Slider enemyHpGauge;
    [SerializeField] private float enemyMaxHp; // �GHP�̍ő�l
    [SerializeField] private float currentEnemyHp; // �G�̌��݂�HP
    [Header("�v���C���[����̔�_���[�W��")] public float playerDamage = 10f;
    private Subject<Unit> isEnemydead = new();
    private bool deadFlag = false;

    //[SerializeField] private AudioClip se;

    private void Awake()
    {
        currentEnemyHp = enemyMaxHp; // HP���ő�l�ŏ�����
        enemyHpGauge.maxValue = enemyMaxHp; // HP�̍ő�l��ݒ�
        UpdateEnemyHpUI();
    }

    public void EnemyTakeDamage(float damage)
    {
        //AudioSource.PlayClipAtPoint(se, transform.position);
        currentEnemyHp -= damage;
        UpdateEnemyHpUI();

        if (currentEnemyHp <= 0)
        {
            if (!deadFlag)
            {
                isEnemydead.OnNext(Unit.Default);
                deadFlag = true;
            }
            
            //enemyBattleController.stageCount = 0;

            // �}�b�v��̑S�Ă̓G��HP0�ɂȂ�����A���̃}�b�v���J�n���鏈�����Ăяo��
            //backgroundScroller.CheckAllEnemiesDefeated();
        }
    }

    private void UpdateEnemyHpUI()
    {
        enemyHpGauge.value = currentEnemyHp;
    }

    public IObservable<Unit> enemyDeadFlag()
    {
        return isEnemydead;
    }

    public void EnemyDestroy()
    {
        deadFlag = false;
        enemyDead.EnemyDestroy(); // �G������
        isEnemydead.Dispose();
    }
}