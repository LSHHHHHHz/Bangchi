using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Spawner : MonoBehaviour
{
    public PoolManager poolManager;
    public Transform[] spawnPoint;
    public Transform BoxPoint;

    public int stageMonster;
    public bool monster = true;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Start()

    {

        Spawn(stageMonster);


        BattleManager.instance.restartStage += () => Spawn(stageMonster);
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
