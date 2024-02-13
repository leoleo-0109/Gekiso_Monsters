using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class EnemyHpCanvasController : MonoBehaviour
{
    [SerializeField, Header("�GHP�L�����o�X")] public GameObject enemyHpCanvas; // �GHP��Canvas

    private void Start()
    {
        enemyHpCanvas.SetActive(false);
    }

    public async UniTask ShowEnemyHPCanvas()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(9f)); // �ҋ@����
        enemyHpCanvas.SetActive(true);
    }

    public void DestroyEnemyHPCanvas()
    {
        enemyHpCanvas.SetActive(false);
    }
}
