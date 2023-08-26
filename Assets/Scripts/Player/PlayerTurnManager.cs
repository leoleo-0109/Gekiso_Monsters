using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum StageType
{
	enemyStage,
	bossStage
};

public class PlayerTurnManager : MonoBehaviour
{
	[SerializeField] private List<Player> playerList = new(4);
	[SerializeField] private SpeedClearTurnController speedClearTurnController;
	private int playerValue = 0;
	private Subject<Unit> nextOperation = new();
	private CompositeDisposable disposables = new CompositeDisposable();
	[HideInInspector] public static StageType stageType;
	private bool turnCountFlag = false;

	void Start()
	{
		nextOperation.Subscribe(x => ChengePlayer())
				.AddTo(this);
		nextOperation.OnNext(Unit.Default);
	}

	private void ChengePlayer()
	{
		disposables.Clear();
		foreach(Player player in playerList)
        {
			player.FriendBulletTrue();
        }
		playerList[playerValue].UpdateStream();
		playerList[playerValue].GetMoveEnd()
			.Subscribe(x =>
			{
				playerList[playerValue].StopUpdate();
				if(stageType == StageType.bossStage)
                {
					if(turnCountFlag)
                    {
						speedClearTurnController.TurnCountDown();
					}
					turnCountFlag = true;
				}

				if (playerValue < playerList.Count - 1)
				{
					playerValue++;
				}
				else
				{
					playerValue = 0;
				}
				Debug.Log(playerValue + 1 + "プレイヤーのターン");

				//敵の攻撃を入れるならここには追加しない
				nextOperation.OnNext(Unit.Default);
			}).AddTo(disposables);
	}
}
