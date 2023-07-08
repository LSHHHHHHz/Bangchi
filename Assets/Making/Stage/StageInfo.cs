using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/StageInfo")]
public class StageInfo : ScriptableObject
{
    public int coin;
    public int exp;
    public float weaponProbability;
    public float shieldProbability;
   
}
