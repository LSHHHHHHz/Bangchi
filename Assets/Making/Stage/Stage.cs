using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int page;
    public int stageNumber;

    private void Awake()
    {
    }
    public void StageSelect(int index)
    {
        if (index == 0)
        {
            BattleManager.instance.LoadStage(page, stageNumber);
        }
    }
}
