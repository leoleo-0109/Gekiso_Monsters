using System.Collections;
using UnityEngine;
using UniRx;

public class GimmickManager : MonoBehaviour
{
    public GameObject[] stage1Gimmick; // ステージ1の敵のプレハブ配列
    public GameObject[] stage2Gimmick; // ステージ2の敵のプレハブ配列
    public GameObject[] stage3Gimmick; // ステージ3の敵のプレハブ配列
    public GameObject[] stage4Gimmick; // ステージ4の敵のプレハブ配列
    public GameObject[] stage5Gimmick; // ステージ5の敵のプレハブ配列
    public GameObject[] stage6Gimmick; // ステージ6の敵のプレハブ配列

    GameObject[] stages;
    public void SetStageGimmick(int currentStageIndex)
    {
        switch (currentStageIndex)
        {
            case 0:
                stages = stage1Gimmick;
                break;
            case 1:
                stages = stage2Gimmick;
                break;
            case 2:
                stages = stage3Gimmick;
                break;
            case 3:
                stages = stage4Gimmick;
                break;
            case 4:
                stages = stage5Gimmick;
                break;
            case 5:
                stages = stage6Gimmick;
                break;
            default:
                break;
        }

        foreach(GameObject stage in stages)
        {
            stage.SetActive(true);
        }
    }

    public void StageGimmickDestroy()
    {
        if (stages != null)
        {
            foreach (GameObject stage in stages)
            {
                stage.SetActive(false);
            }
        }
    }
}


