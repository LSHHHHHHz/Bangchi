using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public PageDB pageDB;
    StageInfo stageInfo;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        int playStage = BattleManager.instance.GetLastPlayedNormalStage();
        stageInfo = pageDB.FindStageInfo(playStage);

        if (stageInfo != null)
        {
            BattleManager.instance.StartStage(stageInfo);
        }
    }
}
