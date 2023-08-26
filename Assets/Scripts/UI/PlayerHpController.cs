using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerHpController : MonoBehaviour
{
    [SerializeField] private CanvasController canvasController;
    public Slider playerHpGauge;
    [SerializeField] private TextMeshProUGUI playerHpText;
    [SerializeField] private float playerMaxHp = 100000f; // プレイヤーHPの最大値
    [SerializeField] private float currentPlayerHp; // プレイヤーの現在のHP
    [Header("地雷の被ダメージ量")] public float mineDamage = 3000f;
    [Header("ウニの被ダメージ量")] public float urchinDamage = 5000f;
    [Header("まきびしの被ダメージ量")] public float makibishiDamage = 8000f;
    [Header("ダメージウォールの被ダメージ量")] public float damageWallDamage = 15000f;
    [Header("回復パネル(大)の回復量")] public float largeHealingQuantity = 10000f;
    [Header("回復パネル(小)の回復量")] public float lessHealingQuantity = 1500f;

    //private bool isGameOver = false; // ゲームオーバーフラグ

    private void Awake()
    {
        currentPlayerHp = playerMaxHp; // HPを最大値で初期化
        playerHpGauge.maxValue = playerMaxHp; // HPの最大値を設定
        UpdatePlayerHpUI();
        //canvasController = FindObjectOfType<CanvasController>();

        //var x = FindObjectsByType<CanvasController>(FindObjectsSortMode.None);
        //Scene上にある<>内を全て探すときに使うと良い。
        //var x = FindObjectsOfType<CanvasController>();
        //for(int i = 0; i < x.Length; i++)
        //{
        //    Debug.Log(x[i]);
        //}
    }

    public void PlayerTakeDamage(float damage)
    {
        currentPlayerHp -= damage;
        UpdatePlayerHpUI();

        if (currentPlayerHp <= 0)
        {
            canvasController.QuestYouLose(); // 死亡処理
        }
    }

    public void PlayerTakeHealing(float healing)
    {
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
        if(currentPlayerHp > 0)
        {
            playerHpText.text = currentPlayerHp.ToString() + "/100000"; // テキストにプレイヤーHPを表示
        }
        if(currentPlayerHp < 0)
        {
            currentPlayerHp = 0;
            playerHpText.text = currentPlayerHp.ToString() + "/100000"; // テキストにプレイヤーHPを表示
        }
    }

    //private IEnumerator GameOverToHomeScene()
    //{
    //    yield return new WaitForSeconds(2f); // 待機時間
    //    SceneManager.LoadScene("HomeScene");
    //}
}
