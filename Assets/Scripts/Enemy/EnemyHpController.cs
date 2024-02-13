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
    [SerializeField] private float enemyMaxHp; // 敵HPの最大値
    [SerializeField] private float currentEnemyHp; // 敵の現在のHP
    [Header("プレイヤーからの被ダメージ量")] public float playerDamage = 10f;
    private Subject<Unit> isEnemyDead = new();
    public bool deadFlag = false;
    private bool takeDamageSEFlag = false;

    //[SerializeField] private AudioClip se;

    private void Awake()
    {
        currentEnemyHp = enemyMaxHp; // HPを最大値で初期化
        enemyHpGauge.maxValue = enemyMaxHp; // HPの最大値を設定
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
                sketManager.EnemyKill(); // スケットスキルの使用できるまでのカウントを敵を倒した分増やす
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
        enemyDead.EnemyDestroy(); // 敵を消す
        //enemyHpCanvasController.DestroyEnemyHPCanvas();
        isEnemyDead.Dispose();
    }
}