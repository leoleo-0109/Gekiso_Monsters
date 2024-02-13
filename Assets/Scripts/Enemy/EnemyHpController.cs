using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using System.Collections.Generic;

public class EnemyHpController : MonoBehaviour
{
    //[SerializeField] private EnemyHpCanvasController enemyHpCanvasController;
    [SerializeField] private SEController seController;
    [SerializeField] private EnemyDead enemyDead;
    [SerializeField] private SketManager sketManager;
    public Slider enemyHpGauge;
    [SerializeField] private float enemyMaxHp; // �GHP�̍ő�l
    [SerializeField] private float currentEnemyHp; // �G�̌��݂�HP
    [Header("�v���C���[����̔�_���[�W��")] public float playerDamage = 10f;
    private Subject<Unit> isEnemyDead = new();
    public bool deadFlag = false;
    private bool takeDamageSEFlag = false;

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
        takeDamageSEFlag = true;
        if (takeDamageSEFlag)
        {
            seController.TakeDamageSEPlay();
            takeDamageSEFlag = false;
        }
        currentEnemyHp -= damage;
        UpdateEnemyHpUI();

        if (currentEnemyHp <= 0)
        {
            if (!deadFlag)
            {
                isEnemyDead.OnNext(Unit.Default);
                deadFlag = true;
                sketManager.EnemyKill(); // �X�P�b�g�X�L���̎g�p�ł���܂ł̃J�E���g��G��|���������₷
                //enemyHpCanvasController.DestroyEnemyHPCanvas();
            }
        }
    }

    private void UpdateEnemyHpUI()
    {
        enemyHpGauge.value = currentEnemyHp;
    }

    public IObservable<Unit> enemyDeadFlag()
    {
        return isEnemyDead;
    }

    public void EnemyDestroy()
    {
        deadFlag = false;
        enemyDead.EnemyDestroy(); // �G������
        //enemyHpCanvasController.DestroyEnemyHPCanvas();
        isEnemyDead.Dispose();
    }
}