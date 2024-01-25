using Assets.Battle;
using Assets.HeroEditor.Common.Scripts.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTimer : MonoBehaviour
{
    public Image bossTimeBarMask;
    public Image bossTimeBar;
    SunBossInfo sunbossInfo;
    StageInfo laststageInfo;
    public bool timeSet;

    private void Start()
    {
        BattleManager.instance.SunBossInfoStart += sunbossInfoSet;
    }

    private void Update()
    {
        if (BattleManager.instance.isBossStageStart )
        {
            sunbossInfo.BasicBossTime -= Time.deltaTime;
            bossTimeBarMask.SetActive(true);
            bossTimeBar.fillAmount = (float)(sunbossInfo.BasicBossTime / 5);
            timeSet= true;

            if (BattleManager.instance.isrestartNomarStage)
            {
                timeSet= false;
                bossTimeBarMask.SetActive(false);
            }
            if (sunbossInfo.BasicBossTime < 0 && timeSet)
            {
                BattleManager.instance.RestartStage();
                BattleManager.instance.bossStageDone = true;
                bossTimeBarMask.SetActive(false);
            }

        }

        //보스 스테이지가 시작하게 되면 타이머가 시작 됨
        //이미지 바 활성화 시키면됨
        //정해진 시간이 끝나면 원래 스테이지로 돌아감
        //원래스테이지로 돌아가게 되면 타이머를 비활성화 시킴
    }
    public void sunbossInfoSet(SunBossInfo sunbossinfo)
    {
        sunbossInfo = sunbossinfo;
    }
}
