using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunBossSlot : MonoBehaviour
{
    public int bossLevel;
    public SunBossInfo sunBossInfo;
    public GameObject sunBossButton;
    public BossType bossType;
    Button sunBossClickButton;

    private void Awake()
    {
        sunBossClickButton = sunBossButton.GetComponent<Button>();
        sunBossClickButton.onClick.AddListener(() =>
        {
            SelectBoss(bossLevel);
        });
    }

    public void SetData(SunBossInfo subBossInfo)
    {
        this.sunBossInfo = subBossInfo;
    }
    public void SelectBoss(int stageLevel)
    {
        BattleManager.instance.StartSunbossStage(sunBossInfo, stageLevel);
        //if(sunBossInfo.bossType == BossType.DamageBoss)
        //{

        //}
        //if (sunBossInfo.bossType == BossType.HPBoss)
        //{

        //}
        //if (sunBossInfo.bossType == BossType.RecoveryBoss)
        //{

        //}
    }
}
