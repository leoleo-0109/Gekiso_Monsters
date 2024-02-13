using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System;

public class BattleTextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI battleText;
    //[SerializeField] private EnemyHpCanvasController enemyHpCanvasController;
    private bool delayTimeFlag = false;

    public async UniTask BattleCountUp(float currentStageIndex)
    {
        if (delayTimeFlag)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(9f)); // 待機処理
            currentStageIndex++;
            battleText.text = currentStageIndex.ToString();
            //enemyHpCanvasController.ShowEnemyHPCanvas().Forget();
        }
        else
        {
            // 開始時のみ実行される
            currentStageIndex++;
            battleText.text = currentStageIndex.ToString();
            delayTimeFlag = true;
        }
    }
}
