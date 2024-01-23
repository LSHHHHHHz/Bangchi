
using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "My Assets/PageDB")]
public class PageDB : ScriptableObject
{
    public List<PageInfo> stagePage;

    public StageInfo FindStageInfo(int stageNumber)
    {
        foreach (PageInfo page in stagePage)
        {
            foreach (StageInfo stageInfo in page.stages)
            {
                if (stageInfo.StageNumber == stageNumber)
                {
                    return stageInfo;
                }
            }
        }

        return null;
    }
}
