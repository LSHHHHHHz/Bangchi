using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageResult : ScriptableObject //스테이지를 클릭했을 때 나온 스테이지들
{
    public List<StageInfo> stages = new List<StageInfo>();
}

internal class StageCalculator
{
    public static StageResult Calculate(StageDB stageDB, int page)
    {
        StageResult result = new StageResult();

        for (int i = page*20; i < 20 + page*20; i++)
        {
            StageInfo selected = stageDB.stages[i];
            result.stages.Add(selected); //아 이거 어디다 저장하는지 다시공부 seleced에
        }
        return result;
    }
}
