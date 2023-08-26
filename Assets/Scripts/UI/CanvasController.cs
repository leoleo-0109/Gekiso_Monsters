using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public enum PauseType
{
    pause,
    play
};

public class CanvasController : MonoBehaviour
{
    [SerializeField, Header("ボスアテンション画面")] private GameObject bossAttentionCanvas; // ボスアテンションCanvas
    [SerializeField, Header("メニュー画面")] private GameObject menuCanvas; // メニューCanvas
    [SerializeField, Header("ギブアップ画面")] private GameObject giveUpCanvas; // ギブアップCanvas
    [SerializeField, Header("あなたの負け画面")] private GameObject youLoseCanvas; // あなたの負けCanvas
    [SerializeField, Header("ゲームオーバー画面")] private GameObject gameOverCanvas; // ゲームオーバーCanvas
    [SerializeField, Header("ゲームクリア画面")] private GameObject stageClearCanvas; // ステージクリアCanvas
    [SerializeField, Header("リザルト画面へのOKボタン表示画面")] private GameObject stageClearOKButtonCanvas; // OKボタン表示Canvas
    [SerializeField, Header("スピードクリアコントローラー")] private SpeedClearController speedClearController;
    private BGMController bgmController;
    private CancellationTokenSource cancellationTokenSource;

    public static PauseType pauseType;
    private bool bossAttentionFlag = false; // ボスアテンションのフラグ
    private bool pauseFlag = false; // GameSceneを一時停止するためのフラグ
    private bool retireFlag = false; // リタイアのフラグ
    private bool youLoseFlag = false; // あなたの負けのフラグ
    private bool gameOverFlag = false; // ゲームオーバーのフラグ
    private bool stageClearFlag = false; // ステージクリアのフラグ

    private void Awake()
    {
        bgmController = GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMController>();
    }

    private void Start()
    {
        //StartCoroutine(RetireToHomeScene());
        pauseType = PauseType.play;
        cancellationTokenSource = new CancellationTokenSource();
    }
    private void Update()
    {
        
    }

    private async UniTask BeforeBossAttention()  // ボスアテンションCanvasを表示する
    {
        if (bossAttentionFlag) return;
        bossAttentionFlag = true;
        bossAttentionCanvas.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(4)); // 待機処理
        bossAttentionCanvas.SetActive(false);
        speedClearController.SpeedClearShow();
    }

    // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestSceneで使用
    public void BossAttention()
    {
        BeforeBossAttention().Forget();
    }

    // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestSceneで使用
    public void MenuButton() // 「三(menu)」ボタン
    {
        pauseFlag = !pauseFlag;
        pauseType = PauseType.pause;
        menuCanvas.SetActive(true);
    }

    // KiwamiQuestScene(menu画面) & UltimateQuestScene(menu画面) & SuperUltimateQuestScene(menu画面)で使用
    public void ReturnQuestButton() // 「クエストに戻る」ボタン
    {
        menuCanvas.SetActive(false);
        pauseFlag = !pauseFlag;
        pauseType = PauseType.play;
    }

    private async UniTask BeforeRetireButton()
    {
        if (retireFlag) return;
        retireFlag = true;
        menuCanvas.SetActive(false);
        giveUpCanvas.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(4)); // 待機処理
        bgmController.StopBGM();
        bgmController.ChangeHomeBGM();
        //await Task.Delay(2000, cancellationToken: cancellationTokenSource.Token); // 待機処理
        SceneManager.LoadScene("HomeScene");
    }

    // KiwamiQuestScene(menu画面) & UltimateQuestScene(menu画面) & SuperUltimateQuestScene(menu画面)で使用
    public void RetireButton() // 「リタイアする」ボタン
    {
        BeforeRetireButton().Forget();
    }



    //private IEnumerator RetireToHomeScene()
    //{
    //    yield return new WaitForSeconds(5f); // 待機時間
    //    SceneManager.LoadScene("HomeScene");
    //}

    private async UniTask BeforeQuestYouLose()  // HPが0になったら、あなたの負けCanvasを表示する
    {
        if (youLoseFlag) return;
        youLoseFlag = true;
        pauseType = PauseType.pause;
        youLoseCanvas.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(4)); // 待機処理
        QuestGameOver();
    }

    // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestSceneで使用
    public void QuestYouLose()
    {
        BeforeQuestYouLose().Forget();
    }

    private async UniTask BeforeQuestGameOver() // あなたの負けCanvasが出たら、ゲームオーバーCanvasを表示する
    {
        if (gameOverFlag) return;
        gameOverFlag = true;
        youLoseCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(4)); // 待機処理
        bgmController.StopBGM();
        bgmController.ChangeHomeBGM();
        //await Task.Delay(4000, cancellationToken:cancellationTokenSource.Token); // 待機処理
        SceneManager.LoadScene("HomeScene");
    }

    // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestSceneで使用
    public void QuestGameOver()
    {
        BeforeQuestGameOver().Forget();
    }

    private async UniTask BeforeQuestStageClear() // ボスのHPが0になったら、クリアCanvasを表示する
    {
        if (stageClearFlag) return;
        stageClearFlag = true;
        pauseType = PauseType.pause;
        stageClearCanvas.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(4), cancellationToken: cancellationTokenSource.Token); // 待機処理
        stageClearOKButtonCanvas.SetActive(true);
    }

    // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestSceneで使用
    public void QuestStageClear()
    {
        BeforeQuestStageClear().Forget();
    }

    private void OnDestroy()
    {
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
    }


}
