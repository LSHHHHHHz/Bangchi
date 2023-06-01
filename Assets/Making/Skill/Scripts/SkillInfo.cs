using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Item1
{
    public enum SkillGrade
    {
        FFF,
        EEE,
        DDD,
        CCC,
        BBB,
        AAA,
        SSS
    }

    public enum SkillType
    {
        Active,
        Passive,
    }

    /// <summary>
    /// ���ӿ� �����ϴ� ������ ���� �ϳ��ϳ��� ��Ÿ���� ������.
    /// </summary>
    [CreateAssetMenu(menuName = "My Assets/SkillInfo")]
    public class SkillInfo : ScriptableObject
    {
        public GameObject skillPrefab;
        public SkillGrade grade;
        public SkillType type;
        public string name;
        public string iconPath;
        public int Number;

        //��ų ���, Ÿ��, �̸�, ���, ����(������� �ʴ� ����?)
    }
}
