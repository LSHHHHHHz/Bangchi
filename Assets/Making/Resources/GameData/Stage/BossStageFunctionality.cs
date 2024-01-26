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
    StageInfo laststageInfo;
    bool outBossStage;
    float initialBossBasicTime;
    public float elasedBossBasicTime;
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
    void updateBossStageTimer()
    {
        elasedBossBasicTime -= Time.deltaTime;
        BossStageFunction.SetActive(true);
        bossTimeBar.fillAmount = (float)(elasedBossBasicTime / initialBossBasicTime);
    }
    void bossStageDone()
    {
        if (BattleManager.instance.isrestartNomarStage)
        {
            outBossStage = true;
            BossStageFunction.SetActive(false);
        }
        if (elasedBossBasicTime < 0)
        {
            RunBossStage();
        }
        if (outBossStage)
        {
            BattleManager.instance.sunbossInfo.BasicBossTime = initialBossBasicTime;
            outBossStage = false;
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
        outBossStage = true;
        BattleManager.instance.RestartStage();
        BattleManager.instance.bossStageDoneTostartNormalStage = true;
        BattleManager.instance.isrestartNomarStage = false;
        BossStageFunction.SetActive(false);
    }
    public void BossStageHPProcessor()
    {
        Monster bossMonster = null;
        if (UnitManager.instance.monsterList[0] != null && UnitManager.instance.monsterList[0]._MonsterInfoType == MonsterInfoType.boss)
        {
            var boss = UnitManager.instance.monsterList[0];
            bossMonster = boss;
        }
        bossHPBar.fillAmount = bossMonster._Current_HP / bossMonster._Max_HP;
        if(bossMonster._Current_HP<=0)
        {
            RunBossStage();
        }
    }
}
