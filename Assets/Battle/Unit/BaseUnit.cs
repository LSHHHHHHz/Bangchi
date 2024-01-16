using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Battle.Unit
{
    public enum MonsterInfoType
    {
        normar,
        boss
    }
    public abstract class BaseUnit : MonoBehaviour
    {
        public float _Current_HP;
        public int _Max_HP;
        public MonsterInfoType _MonsterInfoType;
    }
}
