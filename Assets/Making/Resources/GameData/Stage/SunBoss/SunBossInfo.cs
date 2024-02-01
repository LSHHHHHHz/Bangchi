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
public class SunBossInfo : StageInfo
{
    public BossType bossType;
    public StageType stageType;
    public int SunBossLevel;

    public int[] RewardDamage;
    public int[] RewardHP;
    public int[] RewardHPRecovery;
    public int[] BossHPByLevel;
    public float BasicBossTime = 30f;
} 
