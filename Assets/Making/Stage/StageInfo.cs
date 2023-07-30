using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/StageInfo")]
public class StageInfo : ScriptableObject
{
    public string stageName;
    public int StageNumber;
    public int coin;
    public int exp;
    public float weaponProbability;
    public float shieldProbability;
    public string stageIconPath;
    public string pageIconPath;

    public List<MonsterSpawnInfo> monsterSpawnInfos = new List<MonsterSpawnInfo>();
}

[System.Serializable]
public class MonsterSpawnInfo
{
    public GameObject prefab;
    public Vector3 position;
}
