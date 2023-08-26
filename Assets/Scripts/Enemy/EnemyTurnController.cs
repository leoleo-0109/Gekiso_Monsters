using UnityEngine;

public class EnemyTurnController : MonoBehaviour
{
    // 敵の行動が終了したかどうかのフラグ
    private bool isTurnFinished = true;

    // 敵の行動が終了したことを報告するメソッド
    public void FinishTurn()
    {
        isTurnFinished = true;
    }

    // 敵の行動を開始するメソッド
    public void StartTurn()
    {
        isTurnFinished = false;
        // ここに敵の行動処理を記述する
        Debug.Log("敵のターンです。");
    }

    // ダメージを受ける処理の例
    public void TakeDamage(int damage)
    {
        // ダメージを受ける処理を記述する
    }
}
