using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 마트에서 준 비닐봉지
public class StageResult : ScriptableObject //스테이지를 클릭했을 때 나온 스테이지들
{
    // 사탕 봉지
    public List<StageInfo> stages = new List<StageInfo>();
    public List<StageInfo> stagePage = new();
}

internal class StageCalculator
{
    public static StageResult Calculate(StageDB stageDB, int page)
    {
        StageResult result = new StageResult();

        for (int i = page*20; i < 20 + page*20; i++)
        {
            if (i >= stageDB.stages.Count)
                continue;

            StageInfo selected = stageDB.stages[i];
            result.stages.Add(selected); 
        }

        




        //StagePopup.instance.pageIcon.sprite = Resources.Load<Sprite>(StageInfo.pageIconPath);
        return result;

    }
}
