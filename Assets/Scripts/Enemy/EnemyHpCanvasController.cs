using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class EnemyHpCanvasController : MonoBehaviour
{
    [SerializeField, Header("敵HPキャンバス")] public GameObject enemyHpCanvas; // 敵HPのCanvas

    private void Start()
    {
        enemyHpCanvas.SetActive(false);
    }

    public async UniTask ShowEnemyHPCanvas()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(9f)); // 待機処理
        enemyHpCanvas.SetActive(true);
    }

    public void DestroyEnemyHPCanvas()
    {
        enemyHpCanvas.SetActive(false);
    }
}
