using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "My Assets/SunBossDB")]
public class SunBossDB : ScriptableObject
{
    public List<SunBossPageInfo> Stages;

    public SunBossInfo FindSunBoss(int stageLevel, BossType bossType)
    {
        foreach (SunBossPageInfo stage in Stages)
        {
            foreach (SunBossInfo sunBossInfo in stage.sunBossInfos)
            {
                if (sunBossInfo.SunBossLevel == stageLevel && sunBossInfo.bossType == bossType)
                {
                    return sunBossInfo;
                }
            }
        }
        return null;
    }
}
