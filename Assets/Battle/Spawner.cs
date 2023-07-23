using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Spawner : MonoBehaviour
{
    private bool isEventRegistered = false;

    public PoolManager poolManager;
    public Transform[] spawnPoint;
    public Transform BoxPoint;

    public int stageMonster;
    public bool monster = true;

    private void Awake()
    {
        //spawnPoint = GetComponentsInChildren<Transform>();  <-- �θ���� ������� ������ �Ʒ��� �θ� ������Ʈ�� �ҷ����� ���ϰ� ����
        spawnPoint = GetComponentsInChildren<Transform>(false).Where(t => t != transform).ToArray();
       
    }

    private void Start()

    {
        if (!isEventRegistered)
        {
            //BattleManager.instance.restartStage += () => Spawn(stageMonster);
            isEventRegistered = true;
        }

        Spawn(stageMonster);
    }
    public void Spawn(int value)
    {
        if (monster)
        {
            for (int i = 0; i < spawnPoint.Length; i++)
            {
                GameObject enemy = PoolManager.Instance.Get(value);
                enemy.transform.position = spawnPoint[i].position;
            }
        }
        else
        {
            for (int i = 0; i < spawnPoint.Length; i++)
            {
                GameObject enemy = PoolManager.Instance.Get(0);
                enemy.transform.position = spawnPoint[i].position;

            }
        }
    }

    
}
