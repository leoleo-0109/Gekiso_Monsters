using System.Collections;
using UnityEngine;
using UniRx;

public class GimmickManager : MonoBehaviour
{
    public GameObject[] stage1Gimmick; // �X�e�[�W1�̓G�̃v���n�u�z��
    public GameObject[] stage2Gimmick; // �X�e�[�W2�̓G�̃v���n�u�z��
    public GameObject[] stage3Gimmick; // �X�e�[�W3�̓G�̃v���n�u�z��
    public GameObject[] stage4Gimmick; // �X�e�[�W4�̓G�̃v���n�u�z��
    public GameObject[] stage5Gimmick; // �X�e�[�W5�̓G�̃v���n�u�z��
    public GameObject[] stage6Gimmick; // �X�e�[�W6�̓G�̃v���n�u�z��

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


