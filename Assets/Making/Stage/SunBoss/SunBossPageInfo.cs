using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/SunBossPageInfo")]
public class SunBossPageInfo : ScriptableObject
{
    public int SunBossPageLevel;

    public List<SunBossInfo> sunBossInfos = new();

}
