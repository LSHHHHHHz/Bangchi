using Assets.Battle;
using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunBossSlot : MonoBehaviour
{
    public int bossLevel;
    public int typeNum;
    public SunBossInfo sunBossInfo;
    public GameObject sunBossButton;
    public BossType bossType;
    Button sunBossClickButton;
    public Image image;
    private void Awake()
    {
        Debug.Log("SunBossSlot Awake called");
        BossStageFunctionality.Instance.BossStageClear += StageClearUpdateImage;
        image = GetComponent<Image>();
        sunBossClickButton = sunBossButton.GetComponent<Button>();
        sunBossClickButton.onClick.AddListener(() =>
        {
            SelectBoss(bossLevel);
            SunBossPopup.instance.destorypopup();
        });
        StageClearUpdateImage();
    }

    public void SetData(SunBossInfo subBossInfo)
    {
        this.sunBossInfo = subBossInfo;
    }
    public void StageClearUpdateImage() //이거 넣고 망하는중
    {
        if (BattleManager.instance.SunBossStageClear[bossLevel - 1][typeNum - 1] == true)
        {
            image.sprite = Resources.Load<Sprite>("SunBossSlot");
        }
    }
    public void SelectBoss(int stageLevel)
    {
        if (BattleManager.instance.SunBossStageClear[stageLevel-1][typeNum-1] == true)
        {
            FadeInOutStageProcessor.instance.RunBossStage(sunBossInfo, stageLevel);
            FadeInOutStageProcessor.instance.bossstageDone = true;
            //        BattleManager.instance.StartSunbossStage(sunBossInfo, stageLevel);
        }
        else
        {
            return;
        }
    }
}
