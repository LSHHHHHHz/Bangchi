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
