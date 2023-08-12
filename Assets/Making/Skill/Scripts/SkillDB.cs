using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


/// <summary>
/// 게임에 존재하는 아이템들을 관리하기 위한 데이터베이스
/// </summary>
[CreateAssetMenu(menuName = "My Assets/SkillDB")]
public class SkillDB : ScriptableObject
{
    public List<SkillInfo> skillitems = new List<SkillInfo>(); //<> 안에 있는 타입 인스턴스를 여러개 갖는다

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

