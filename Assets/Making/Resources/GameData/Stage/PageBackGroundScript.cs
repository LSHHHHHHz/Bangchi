using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageBackGroundScript : MonoBehaviour
{
    public int pageNum;
    public StageType stageType;
    private void Start()
    {
        BattleManager.instance.OnStageRestart += backGroundChange;
    }

    public void backGroundChange()
    {
        if (BattleManager.instance.currentStageInfo.pageNumber == pageNum && BattleManager.instance.currentStageInfo.Type == StageType.Normal)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

}
