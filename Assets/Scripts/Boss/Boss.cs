using System.Runtime.CompilerServices;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject bossBullet;
    [SerializeField] private GameObject targetPlayer1;
    [SerializeField] private GameObject targetPlayer2;
    [SerializeField] private GameObject targetPlayer3;
    [SerializeField] private GameObject targetPlayer4;

    private GameObject[] targetPlayers;

    private void Start()
    {
        // ボスの弾の対象プレイヤーを配列に格納
        targetPlayers = new GameObject[] { targetPlayer1, targetPlayer2, targetPlayer3, targetPlayer4 };
    }

    public void SetBossBullet()
    {
        if (targetPlayers != null)
        {
            foreach (GameObject player in targetPlayers)
            {
                if (player != null)
                {
                    Instantiate(bossBullet, transform).GetComponent<BossBullet>().ShotBossBulletToPlayer(player);
                }
                else
                {
                    Debug.LogWarning("Player target is null in Enemy.SetEnemyBullet()");
                }
            }
        }
        else
        {
            Debug.LogWarning("Target players array is null in Enemy.SetEnemyBullet()");
        }
    }
}
