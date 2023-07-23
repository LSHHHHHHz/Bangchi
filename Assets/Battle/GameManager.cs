using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolManager pool;
    public Player player;

    public StageInfo stageInfo; // StageInfo ��ü�� �Ҵ��ϱ� ���� ����
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // DropItem ���� ������Ʈ ã��
        DropItem dropItem = FindObjectOfType<DropItem>();

        if (dropItem != null && stageInfo != null)
        {
            // StageInfo�� coin�� exp ���� DropItem�� �ʵ忡 �Ҵ�
            dropItem.coin = stageInfo.coin;
            dropItem.exp = stageInfo.exp;
        }

        BattleManager.instance.StartStage(stageInfo);
    }
}
