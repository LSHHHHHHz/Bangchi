using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Item1
{
    public enum SkillGrade
    {
        D,
        C,
        B,
        A,
        S
    }

    public enum SkillType
    {
        Active,
        Passive,
    }

    /// <summary>
    /// 게임에 등장하는 아이템 각각 하나하나를 나타내는 데이터.
    /// </summary>
    [CreateAssetMenu(menuName = "My Assets/SkillInfo")]
    public class SkillInfo : ScriptableObject
    {
        public GameObject skillPrefab;
        public SkillGrade grade;
        public SkillType type;
        public string name;
        public string iconPath;
        public string backGroundIconPath;
        public int Number;
        public float CooldownSeconds;
    }
}
