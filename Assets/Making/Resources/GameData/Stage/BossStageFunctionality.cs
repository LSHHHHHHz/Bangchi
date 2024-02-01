using Assets.Battle;
using Assets.Battle.Unit;
using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStageFunctionality : MonoBehaviour
{
    public Image BossStageFunction;
    public Image bossTimeBar;
    public Image bossHPBar;
    public Button BossStageRunButton;
    SunBossInfo sunbossInfo;
    float initialBossBasicTime;
    public float elasedBossBasicTime;
   public bool rewardToPlayer;

    int[] bossStageNumber = new int[3];
    public event Action BossStageClear;

    public static BossStageFunctionality Instance;
    private void Awake()
    {
        Instance = this;
        for(int i = 0; i <3; i++)
        {
            bossStageNumber[i] = 0; ;
        }
    }
    private void Start()
    {
        BattleManager.instance.SunBossInfoStart += sunbossInfoSet;
    }

    private void Update()
    {
        if (BattleManager.instance.isBossStageStart )
        {
            updateBossStageTimer();
            bossStageDone();
            BossStageHPProcessor();
        }
    }

    void OnEnable()
    {
        BattleManager.instance.OnNormalStageRestart += HandleNormalStageRestart;
    }
    void OnDisable()
    {
        BattleManager.instance.OnNormalStageRestart -= HandleNormalStageRestart;
    }
    private void HandleNormalStageRestart()
    {
        BossStageFunction.SetActive(false);
        if (BattleManager.instance.sunbossInfo != null)
        {
            BattleManager.instance.sunbossInfo.BasicBossTime = initialBossBasicTime;
        }
    }
    void updateBossStageTimer()
    {
        elasedBossBasicTime -= Time.deltaTime;
        BossStageFunction.SetActive(true);
        bossTimeBar.fillAmount = (float)(elasedBossBasicTime / initialBossBasicTime);
    }
    void bossStageDone()
    {
        if (elasedBossBasicTime < 0)
        {
            RunBossStage();
        }
    }

    public void sunbossInfoSet(SunBossInfo sunbossinfo)
    {
        sunbossInfo = sunbossinfo;
        initialBossBasicTime = sunbossInfo.BasicBossTime;
        elasedBossBasicTime= sunbossinfo.BasicBossTime;
    }
    public void RunBossStage()
    {
        BattleManager.instance.RestartStage();
        BattleManager.instance.bossStageDoneTostartNormalStage = true;
        BossStageFunction.SetActive(false);
    }
    public void BossStageHPProcessor()
    {
        if (UnitManager.instance.monsterList.Count > 0 && UnitManager.instance.monsterList[0]._MonsterInfoType == MonsterInfoType.boss)
        {
            Monster bossMonster = null;
            if (UnitManager.instance.monsterList[0] != null)
            {
                var boss = UnitManager.instance.monsterList[0];
                bossMonster = boss;
            }
            bossHPBar.fillAmount = bossMonster._Current_HP / bossMonster._Max_HP;
            if (bossMonster._Current_HP <= 0 && rewardToPlayer ==false)
            {
                BossRewardOfType();
                RunBossStage();
            }
        }
    }
    public void BossRewardOfType()
    {
        int BossStageNum = (int)sunbossInfo.bossType;

        switch (sunbossInfo.bossType)
        {
            case BossType.DamageBoss:
                Player.instance.Current_Attack += sunbossInfo.RewardDamage[BossStageNum];
                break;
            case BossType.HPBoss:
                Player.instance.Max_HP += sunbossInfo.RewardHP[BossStageNum];
                break;
            case BossType.RecoveryBoss:
                Player.instance.RecoveryHP += sunbossInfo.RewardHPRecovery[BossStageNum];
                break;
        }
        BattleManager.instance.SunBossStageClear[bossStageNumber[BossStageNum] + 1][BossStageNum] = true;
        bossStageNumber[BossStageNum]++;
        rewardToPlayer = true;
        RewardToPlayer();
    }
    private void RewardToPlayer()
    {
        if (rewardToPlayer)
        {
            BossStageClear?.Invoke();
        }
    }
}
