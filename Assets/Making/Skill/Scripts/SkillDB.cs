using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// ���ӿ� �����ϴ� �����۵��� �����ϱ� ���� �����ͺ��̽�
/// </summary>
[CreateAssetMenu(menuName = "My Assets/SkillDB")]
public class SkillDB : ScriptableObject
{
    public List<SkillInfo> skillitems = new List<SkillInfo>(); //<> �ȿ� �ִ� Ÿ�� �ν��Ͻ��� ������ ���´�

    public List<SkillInfo> GetSkillGrade(SkillGrade grade)
    {
        List<SkillInfo> result = new();
        foreach (SkillInfo skill in skillitems)
        {
            if(skill.grade == grade)
            {
                result.Add(skill);
            }
        }
        return result;
    }
}

