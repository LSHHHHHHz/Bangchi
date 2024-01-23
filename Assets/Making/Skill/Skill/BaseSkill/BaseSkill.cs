using Assets.Battle.Unit;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class BaseSkill : MonoBehaviour
{
    public BaseUnit owner;
    public int skillCount = 0;
    public int skillCollSownMaxCount;
    public abstract void Execute();
}