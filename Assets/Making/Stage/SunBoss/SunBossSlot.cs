using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunBossSlot : MonoBehaviour
{
    int[] BossLevel;
    int gridCount;
    public SunBossInfo sunBossInfo;
    public GameObject sunBossButton;
    public BossType bossType;
    Button sunBossClickButton;

    private void Awake()
    {
        gridCount = SunBossUI.instance.GetGridCount();
        sunBossClickButton = sunBossButton.GetComponent<Button>();
        BossLevel = new int[gridCount];
    }

    public void SetData(SunBossInfo subBossInfo)
    {
        this.sunBossInfo = subBossInfo;
    }
    public void SelectBoss(int stageLevel)
    {
        if(sunBossInfo.bossType == BossType.DamageBoss)
        {

        }
        if (sunBossInfo.bossType == BossType.HPBoss)
        {

        }
        if (sunBossInfo.bossType == BossType.RecoveryBoss)
        {

        }
    }
}
