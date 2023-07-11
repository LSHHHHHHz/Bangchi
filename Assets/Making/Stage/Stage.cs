using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public StageInfo stageInfo;
    public int page;
    public int stageNumber;

    private void Awake()
    {
        var dropItem = GetComponent<DropItem>();
        dropItem.exp = stageInfo.exp;
        dropItem.coin = stageInfo.coin;
    }
   
}
