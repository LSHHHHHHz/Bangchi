using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolManager pool;
    public Player player;

    public StageInfo stageInfo; // StageInfo 객체를 할당하기 위한 변수
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // DropItem 게임 오브젝트 찾기
        DropItem dropItem = FindObjectOfType<DropItem>();

        if (dropItem != null && stageInfo != null)
        {
            // StageInfo의 coin과 exp 값을 DropItem의 필드에 할당
            dropItem.coin = stageInfo.coin;
            dropItem.exp = stageInfo.exp;
        }

        BattleManager.instance.StartStage(stageInfo);
    }
}
