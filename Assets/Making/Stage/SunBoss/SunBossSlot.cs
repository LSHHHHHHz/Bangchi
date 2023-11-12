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
        BossStageProcessor.instance.RunBossStage(sunBossInfo, stageLevel);
//        BattleManager.instance.StartSunbossStage(sunBossInfo, stageLevel);
    }
}
