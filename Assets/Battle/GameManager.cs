using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player;
    public PageDB pageDB;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // DropItem ���� ������Ʈ ã��
        DropItem dropItem = FindObjectOfType<DropItem>();

        int playStage = BattleManager.instance.GetLastPlayedNormalStage();
        StageInfo stageInfo = pageDB.FindStageInfo(playStage);

        if (dropItem != null && stageInfo != null)
        {
            // StageInfo�� coin�� exp ���� DropItem�� �ʵ忡 �Ҵ�
            dropItem.coin = stageInfo.coin;
            dropItem.exp = stageInfo.exp;
        }
        if (stageInfo != null)
        {
            BattleManager.instance.StartStage(stageInfo);
        }
    }
}
