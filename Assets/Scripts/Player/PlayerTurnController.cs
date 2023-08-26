using UnityEngine;

public class PlayerTurnController : MonoBehaviour
{
    [SerializeField] private Player player;
    //public string playerName; // プレイヤーの名前など、必要なプレイヤー情報を追加することができます
    public bool isMyTurn;     // そのプレイヤーのターンかどうかを判定するフラグ

    private void Update()
    {
        TakeAction();
    }

    // プレイヤーのターンが始まる処理
    public void StartTurn()
    {
        isMyTurn = true;
    }

    // プレイヤーのターンが終了する処理
    public void EndTurn()
    {
        isMyTurn = false;
    }

    // プレイヤーが行動を選択した際の処理
    public void TakeAction()
    {
        if (isMyTurn)
        {
            player.PlayerMove();
        }
    }
}
