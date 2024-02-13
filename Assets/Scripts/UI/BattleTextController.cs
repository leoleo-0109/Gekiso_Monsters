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
            await UniTask.Delay(TimeSpan.FromSeconds(9f)); // �ҋ@����
            currentStageIndex++;
            battleText.text = currentStageIndex.ToString();
            //enemyHpCanvasController.ShowEnemyHPCanvas().Forget();
        }
        else
        {
            // �J�n���̂ݎ��s�����
            currentStageIndex++;
            battleText.text = currentStageIndex.ToString();
            delayTimeFlag = true;
        }
    }
}
