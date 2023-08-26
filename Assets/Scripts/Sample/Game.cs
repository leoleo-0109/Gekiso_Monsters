using UnityEngine;
using UnityEngine.UI; ///<add this

public class Game : MonoBehaviour
{
    public enum Turn { 
    StageBegin,
    AreaBegin,
    Player,
    PlayerToEnemy,
    Enemy,
    EnemyToPlayer,
    AreaCleared,
    AreaMoving,
    StageCleared,
    Gameover,
    Empty
    }

    public static bool canControl;
    public static Turn turn;
    [SerializeField]
    EnemyGroup enemyGroup;
    //[SerializeField]
    //Text cutin;
    [SerializeField]
    int maxArea = 1;
    [SerializeField]
    int nowArea;
    void Start()
    {
        turn = Turn.StageBegin;
        //cutin.gameObject.SetActive(false);
        //fade
        Fader.instance.BlackIn();
    }

    // Update is called once per frame
    /*void Update()
    {
        TurnControl();
    }
    void TurnControl() {

        switch (turn) { 
            case Turn.StageBegin:
                TurnChange(3, "START", Turn.AreaBegin);
                break;
            case Turn.AreaBegin:
                enemyGroup.SetEnemy(nowArea);
                TurnChange(2, "NEXT AREA", Turn.Player);
                break;
            case Turn.Player:
                //on Player,cs
                break;
            case Turn.PlayerToEnemy:
                if (enemyGroup.GetEnemyCount() <= 0) 
                {
                    if (enemyGroup.GetEnemyCount() <= 0)
                    {
                        turn = Turn.AreaCleared;
                    }
                    else
                    {
                        TurnChange(2, "NEXT TURN", Turn.Enemy);
                    }
                }
                break;
            case Turn.Enemy:
                turn = Game.Turn.EnemyToPlayer;
                break;
            case Turn.EnemyToPlayer:
                TurnChange(2, "NEXT TURN", Turn.Player);
                    break;
            case Turn.AreaCleared:
                if (nowArea == maxArea - 1)
                {
                    TurnChange(2, "STAGE CLEAR", Turn.StageCleared);
                }
                else
                {
                    ++nowArea;
                    TurnChange(2, "AREA CLEAR", Turn.AreaBegin);
                }
                break;
            case Turn.AreaMoving:
            case Turn.StageCleared:
                Fader.instance.BlackOut(1f, "Menu");
                break;
            case Turn.Gameover:
            case Turn.Empty:
            default:
                break;
        }
    }
    /*void TurnChange(float time, string cutinText, Turn nextTurn) { 
    cutin.text = cutinText;
        cutin.gameObject.SetActive(true);
        turn = Turn.Empty;
        CoroutineCommon.CallWaitForSeconds(time, () =>
        {
            cutin.gameObject.SetActive(false);
            turn = nextTurn;
        });
    }*/
}
