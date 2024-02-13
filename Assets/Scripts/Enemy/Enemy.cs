using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private GameObject targetPlayer1;
    [SerializeField] private GameObject targetPlayer2;
    [SerializeField] private GameObject targetPlayer3;
    [SerializeField] private GameObject targetPlayer4;

    private GameObject[] targetPlayers;

    private void Start()
    {
        // “G‚Ì’e‚Ì‘ÎÛƒvƒŒƒCƒ„[‚ğ”z—ñ‚ÉŠi”[
        targetPlayers = new GameObject[] { targetPlayer1, targetPlayer2, targetPlayer3, targetPlayer4 };
    }

    public void SetEnemyBullet()
    {
        if (targetPlayers != null)
        {
            foreach (GameObject player in targetPlayers)
            {
                if (player != null)
                {
                    Instantiate(enemyBullet, transform).GetComponent<EnemyBullet>().ShotEnemyBulletToPlayer(player);
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
