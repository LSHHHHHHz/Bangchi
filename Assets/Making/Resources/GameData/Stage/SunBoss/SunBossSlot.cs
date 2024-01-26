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
            SunBossPopup.instance.destorypopup();
        });
    }

    public void SetData(SunBossInfo subBossInfo)
    {
        this.sunBossInfo = subBossInfo;
    }
    public void SelectBoss(int stageLevel)
    {
        
        FadeInOutStageProcessor.instance.RunBossStage(sunBossInfo, stageLevel);
        FadeInOutStageProcessor.instance.bossstageDone = true;
        //        BattleManager.instance.StartSunbossStage(sunBossInfo, stageLevel);
    }
}