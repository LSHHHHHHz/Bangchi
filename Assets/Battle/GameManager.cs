using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public PageDB pageDB;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // DropItem 게임 오브젝트 찾기
        DropItem dropItem = FindObjectOfType<DropItem>();

        int playStage = BattleManager.instance.GetLastPlayedNormalStage();
        StageInfo stageInfo = pageDB.FindStageInfo(playStage);

        if (dropItem != null && stageInfo != null)
        {
            // StageInfo의 coin과 exp 값을 DropItem의 필드에 할당
            dropItem.coin = stageInfo.coin;
            dropItem.exp = stageInfo.exp;
        }
        if (stageInfo != null)
        {
            BattleManager.instance.StartStage(stageInfo);
        }
    }
}
