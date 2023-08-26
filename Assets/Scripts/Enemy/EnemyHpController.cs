using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class EnemyHpController : MonoBehaviour
{
    [SerializeField] private EnemyHpCanvasController enemyHpCanvasController;
    [SerializeField] private EnemyDead enemyDead;
    public Slider enemyHpGauge;
    [SerializeField] private float enemyMaxHp; // 敵HPの最大値
    [SerializeField] private float currentEnemyHp; // 敵の現在のHP
    [Header("プレイヤーからの被ダメージ量")] public float playerDamage = 10f;
    private Subject<Unit> isEnemydead = new();
    private bool deadFlag = false;

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

            // マップ上の全ての敵がHP0になったら、次のマップを開始する処理を呼び出す
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
        enemyDead.EnemyDestroy(); // 敵を消す
        isEnemydead.Dispose();
    }
}