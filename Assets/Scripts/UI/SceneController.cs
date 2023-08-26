using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

public class SceneController : MonoBehaviour
{
    private BGMController bgmController;
    private bool kiwamiQuestStartFlag = false; // 極クエストスタートのフラグ
    private bool ultimateQuestStartFlag = false; // 究極クエストスタートのフラグ
    private bool superUltimateQuestStartFlag = false; // 超究極クエストスタートのフラグ
    private CancellationTokenSource cts;

    private void Awake()
    {
        bgmController = GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMController>();
    }
    private void Start()
    {
        cts = new CancellationTokenSource();
    }

    private void Update()
    {
        // TitleSceneで使用
        //if (SceneManager.GetActiveScene().name == "TitleScene")
        //{
        //    if (Input.GetKeyDown(KeyCode.Space)) // スペースキーを押したら
        //    {

        //        SceneManager.LoadScene("HomeScene");
        //    }
        //}
    }

    // TitleSceneで使用
    public void HomeButton() // 「START」ボタン
    {
        bgmController.ChangeHomeBGM();
        SceneManager.LoadScene("HomeScene");
    }

    // HomeSceneで使用
    public void TitleButton() // 「タイトルに戻る」ボタン
    {
        bgmController.ChangeTitleBGM();
        StaminaManager.ResetStamina();
        SceneManager.LoadScene("TitleScene");
    }

    // HomeSceneで使用
    public void QuestButton() // 「クエスト」ボタン
    {
        SceneManager.LoadScene("DifficultyScene");
    }

    // HomeSceneで使用
    public void MonsterButton() // 「モンスター」ボタン
    {
        SceneManager.LoadScene("MonsterScene");
    }

    // MonsterScene & DifficultySceneで使用
    public void RuturnHomeButton() // 「←(戻る)」ボタン
    {
        SceneManager.LoadScene("HomeScene");
    }

    // DifficultySceneで使用
    public void KiwamiButton() // 「極」ボタン
    {
        SceneManager.LoadScene("KiwamiSortieScene");
    }

    // DifficultySceneで使用
    public void UltimateButton() // 「究極」ボタン
    {
        SceneManager.LoadScene("UltimateSortieScene");
    }

    // DifficultySceneで使用
    public void SuperUltimateButton() // 「超究極」ボタン
    {
        SceneManager.LoadScene("SuperUltimateSortieScene");
    }

    // KiwamiSortieScene & UltimateSortieScene & SuperUltimateSortieSceneで使用
    public void RuturnDifficultyButton() // 「←(戻る)」ボタン
    {
        SceneManager.LoadScene("DifficultyScene");
    }

    private async UniTask BeforeKiwamiSortieButton()
    {
        bgmController.StopBGM();
        SceneManager.LoadScene("KiwamiQuestStartScene");
        if (kiwamiQuestStartFlag) return;
        kiwamiQuestStartFlag = true;
        await UniTask.Delay(TimeSpan.FromSeconds(4)); // 待機処理
        Timer.TimerStart();
        bgmController.ChangeKiwamiQuestBGM();
        SceneManager.LoadScene("KiwamiQuestScene");
    }

    // KiwamiSortieSceneで使用
    public void KiwamiSortieButton() // 「出撃」ボタン(極)
    {
        BeforeKiwamiSortieButton().Forget();
    }

    private async UniTask BeforeUltimateSortieButton()
    {
        bgmController.StopBGM();
        SceneManager.LoadScene("UltimateQuestStartScene");
        if (ultimateQuestStartFlag) return;
        ultimateQuestStartFlag = true;
        await UniTask.Delay(TimeSpan.FromSeconds(4)); // 待機処理
        Timer.TimerStart();
        bgmController.ChangeUltimateQuestBGM();
        SceneManager.LoadScene("UltimateQuestScene");
    }

    // UltimateSortieSceneで使用
    public void UltimateSortieButton() // 「出撃」ボタン(究極)
    {
        BeforeUltimateSortieButton().Forget();
    }

    private async UniTask BeforeSuperUltimateSortieButton()
    {
        bgmController.StopBGM();
        SceneManager.LoadScene("SuperUltimateQuestStartScene");
        if (superUltimateQuestStartFlag) return;
        superUltimateQuestStartFlag = true;
        await UniTask.Delay(TimeSpan.FromSeconds(4)); // 待機処理
        Timer.TimerStart();
        bgmController.ChangeSuperUltimateQuestBGM();
        SceneManager.LoadScene("SuperUltimateQuestScene");
    }

    // SuperUltimateSortieSceneで使用
    public void SuperUltimateSortieButton() // 「出撃」ボタン(超究極)
    {
        BeforeSuperUltimateSortieButton().Forget();
    }

    // KiwamiSortieScene & UltimateSortieScene & SuperUltimateSortieSceneで使用
    public void AllFormationButton() // 「一括編成」ボタン
    {
        SceneManager.LoadScene("MonsterScene");
    }

    // KiwamiQuestSceneで使用
    public void KiwamiQuestOKButton() // 「OK」ボタン(極)
    {
        bgmController.StopBGM();
        Timer.TimerFinish();
        bgmController.ChangeHomeBGM();
        SceneManager.LoadScene("KiwamiResultScene");
    }

    // UltimateQuestSceneで使用
    public void UltimateQuestOKButton() // 「OK」ボタン(究極)
    {
        bgmController.StopBGM();
        Timer.TimerFinish();
        bgmController.ChangeHomeBGM();
        SceneManager.LoadScene("UltimateResultScene");
    }

    // SuperUltimateQuestSceneで使用
    public void SuperUltimateQuestOKButton() // 「OK」ボタン(超究極)
    {
        bgmController.StopBGM();
        Timer.TimerFinish();
        bgmController.ChangeHomeBGM();
        SceneManager.LoadScene("SuperUltimateResultScene");
    }

    // KiwamiResultScene & UltimateResultScene & SuperUltimateResultSceneで使用
    public void ResultOKButton() // 「OK」ボタン
    {
        SceneManager.LoadScene("HomeScene");
    }
}
