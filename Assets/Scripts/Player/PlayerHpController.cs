using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using System.Collections.Generic;

public class PlayerHpController : MonoBehaviour
{
    public List<Player> playerList = new List<Player>();
    [SerializeField] private SEController seController;
    [SerializeField] private CanvasController canvasController;
    public Slider playerHpGauge;
    public TextMeshProUGUI playerHpText;
    public float playerMaxHp = 100000f; // プレイヤーHPの最大値
    public float currentPlayerHp; // プレイヤーの現在のHP
    [Header("地雷の被ダメージ量")] public float mineDamage = 3000f;
    [Header("ウニの被ダメージ量")] public float urchinDamage = 5000f;
    [Header("まきびしの被ダメージ量")] public float makibishiDamage = 8000f;
    [Header("ダメージウォールの被ダメージ量")] public float damageWallDamage = 15000f;
    [Header("回復パネル(大)の回復量")] public float largeHealingQuantity = 20000f;
    [Header("回復パネル(小)の回復量")] public float lessHealingQuantity = 2000f;
    [Header("敵弾の攻撃ダメージ量")] public float enemyBulletDamage = default;
    [Header("ボス弾の攻撃ダメージ量")] public float bossBulletDamage = default;
    private bool takeDamageSEFlag = false;
    private bool takeHealSEFlag = false;

    private void Awake()
    {
        currentPlayerHp = playerMaxHp; // HPを最大値で初期化
        playerHpGauge.maxValue = playerMaxHp; // HPの最大値を設定
        UpdatePlayerHpUI();
    }

    public void PlayerTakeDamage(float damage)
    {
        takeDamageSEFlag = true;
        if (takeDamageSEFlag)
        {
            seController.TakeDamageSEPlay();
            takeDamageSEFlag = false;
        }
        currentPlayerHp -= damage;
        UpdatePlayerHpUI();


        if (currentPlayerHp <= 0)
        {
            // HPが0になって且つ、全てのプレイヤーが停止したら、ゲームオーバーになる
            // (= HPが0になっても動いている間に回復すれば生き延びれる)
            if (playerList[0].force.magnitude == 0 && 
                playerList[1].force.magnitude == 0 &&
                playerList[2].force.magnitude == 0 &&
                playerList[3].force.magnitude == 0)
            {
                canvasController.QuestYouLose(); // 死亡処理
            }
        }
    }

    public void PlayerTakeHealing(float healing)
    {
        takeHealSEFlag = true;
        if (takeHealSEFlag)
        {
            seController.TakeHealSEPlay();
            takeHealSEFlag = false;
        }
        currentPlayerHp += healing;
        if (currentPlayerHp >= playerMaxHp)
        {
            currentPlayerHp = playerMaxHp;
        }
        UpdatePlayerHpUI();
    }

    private void UpdatePlayerHpUI()
    {
        playerHpGauge.value = currentPlayerHp;
        if (currentPlayerHp > 0)
        {
            playerHpText.text = currentPlayerHp.ToString(); // テキストにプレイヤーHPを表示
        }
        if (currentPlayerHp < 0)
        {
            currentPlayerHp = 0;
            playerHpText.text = currentPlayerHp.ToString(); // テキストにプレイヤーHPを表示
        }
    }
}
