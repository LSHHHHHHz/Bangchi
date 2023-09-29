using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BossType
{
    DamageBoss,
    HPBoss,
    RecoveryBoss
}
[CreateAssetMenu(menuName = "My Assets/SunBossInfo")]
public class SunBossInfo : ScriptableObject
{
    public BossType bossType;

    public int SunBossLevel;
    public int PlusDamage;
    public int PlusHP;
    public int PlusRecovered;

    public int BossHP;
    public float BasicBossTime = 30f;

    public MonsterSpawnInfo MonsterSpawn;

    [System.Serializable]
    public class MonsterSpawnInfo
    {
        public GameObject prefab;
        public Vector3 position;
    }
} 
