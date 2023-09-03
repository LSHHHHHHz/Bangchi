using Assets.Item1;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스킬 정보, 스킬 숫자, 스킬 레벨 (변경될 수 있는 정보)
[Serializable]
public class SkillInstance 
{
    public SkillInfo skillInfo;
    public int count;
    public int upgradeLevel;
}
