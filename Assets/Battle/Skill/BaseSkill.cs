using Assets.Battle.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    // 스킬을 실행하는 함수
    public BaseUnit owner;
    public abstract void Execute();
}