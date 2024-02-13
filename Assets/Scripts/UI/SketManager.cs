using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SketManager : MonoBehaviour
{
    [SerializeField] private SEController seController;
    [SerializeField] private SketSkillTurnTextController skillTurnTextController;
    [SerializeField] private PlayerHpController playerHpController;
    public List<EnemyHpController> enemyHpControllerList = new List<EnemyHpController>();
    public List<Enemy> enemyList = new List<Enemy>();

    [SerializeField] private GameObject sketIconParent;
    [SerializeField] private Image sketIcon;
    [SerializeField] private GameObject sketSkillButton;
    [SerializeField] private GameObject sketSkillCannotButton;

    private int enemyKillCount = 0; // �|�����G�̐�
    private int skillUseCount = 0;   // �X�L�����g�p������


    private void Start()
    {
        sketIcon.fillAmount = 0; // ������Ԃł̓A�C�R���͋�
        sketIconParent.SetActive(sketIcon.enabled);
        sketSkillButton.SetActive(false);
        sketSkillCannotButton.SetActive(true);
    }

    public void EnemyKill()
    {
        enemyKillCount++;
        skillTurnTextController.SketSkillTurnCountUp(enemyKillCount);

        // �G��5�̓|���āA�X�L������x���g�p���Ă��Ȃ������ꍇ�̂ݎg�p�\
        if (enemyKillCount >= 5 && skillUseCount == 0)
        {
            sketIcon.fillAmount = 1; // �A�C�R���𖞃^���ɂ���
            sketIconParent.SetActive(!sketIcon.enabled);
            sketSkillButton.SetActive(true);
            sketSkillCannotButton.SetActive(false);
            skillUseCount++;�@// �X�L���g�p�񐔂𑝂₵�āA�X�L���g�p�ł��Ȃ�����
        }
        else
        {
            if (skillUseCount == 0)
            {
                float fillIncrement = 1f / 5f; // 1��|�����ƂɃA�C�R����1/5�����₷
                sketIcon.fillAmount += fillIncrement; // �A�C�R���𑝂₷
            }
        }
    }

    // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestScene�Ŏg�p
    public void SketSkillButton()
    {
        seController.ClickSEPlay();
        seController.TakeHealSEPlay();
        // �X�P�b�g�X�L�����g�p����ƃv���C���[��HP���S�񕜂���
        playerHpController.currentPlayerHp = playerHpController.playerMaxHp;
        playerHpController.playerHpGauge.value = playerHpController.playerMaxHp;
        playerHpController.playerHpText.text = playerHpController.currentPlayerHp.ToString();
        sketSkillButton.SetActive(false);
        sketSkillCannotButton.SetActive(true);
    }
}
