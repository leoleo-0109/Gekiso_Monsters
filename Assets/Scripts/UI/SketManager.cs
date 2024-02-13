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

    private int enemyKillCount = 0; // 倒した敵の数
    private int skillUseCount = 0;   // スキルを使用した回数


    private void Start()
    {
        sketIcon.fillAmount = 0; // 初期状態ではアイコンは空
        sketIconParent.SetActive(sketIcon.enabled);
        sketSkillButton.SetActive(false);
        sketSkillCannotButton.SetActive(true);
    }

    public void EnemyKill()
    {
        enemyKillCount++;
        skillTurnTextController.SketSkillTurnCountUp(enemyKillCount);

        // 敵を5体倒して、スキルを一度も使用していなかった場合のみ使用可能
        if (enemyKillCount >= 5 && skillUseCount == 0)
        {
            sketIcon.fillAmount = 1; // アイコンを満タンにする
            sketIconParent.SetActive(!sketIcon.enabled);
            sketSkillButton.SetActive(true);
            sketSkillCannotButton.SetActive(false);
            skillUseCount++;　// スキル使用回数を増やして、スキル使用できなくする
        }
        else
        {
            if (skillUseCount == 0)
            {
                float fillIncrement = 1f / 5f; // 1回倒すごとにアイコンを1/5分増やす
                sketIcon.fillAmount += fillIncrement; // アイコンを増やす
            }
        }
    }

    // KiwamiQuestScene & UltimateQuestScene & SuperUltimateQuestSceneで使用
    public void SketSkillButton()
    {
        seController.ClickSEPlay();
        seController.TakeHealSEPlay();
        // スケットスキルを使用するとプレイヤーのHPが全回復する
        playerHpController.currentPlayerHp = playerHpController.playerMaxHp;
        playerHpController.playerHpGauge.value = playerHpController.playerMaxHp;
        playerHpController.playerHpText.text = playerHpController.currentPlayerHp.ToString();
        sketSkillButton.SetActive(false);
        sketSkillCannotButton.SetActive(true);
    }
}
