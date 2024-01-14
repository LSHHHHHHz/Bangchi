using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageBackGroundScript : MonoBehaviour
{
    public int pageNum;

    //다른 곳에서 이벤트로 페이지 번호를 호출하면 그 페이지만 활성화 하고 나머지 비활성화시키면 됨

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
