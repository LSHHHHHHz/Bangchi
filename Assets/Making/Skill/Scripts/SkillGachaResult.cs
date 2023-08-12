using Assets.HeroEditor.InventorySystem.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using Unity.VisualScripting;

public class SkillGachaResult
{
    // public List<ItemInfo> items = new List<ItemInfo>();
    public List<SkillInfo> items = new List<SkillInfo>();
}

internal class SkillGGachaCalculator
{
    /*public static SkillGachaResult Calculate(SkillDB skillitemDB, int count)
    //public static GachaResult Calculate ÀÌ·±°Ç ¹¹Áö


    {
        var result = new SkillGachaResult();
        //var random = new Random(); //ranmdom °´Ã¼¸¦ ¸¸µë
        UnityEngine.Random.Range(0, count);
        for (int i = 0; i < count; ++i)
        {
            //ItemInfo selected = itemDB.items[random.Next(0, itemDB.items.Count)];
            SkillInfo selected = skillitemDB.skillitems[UnityEngine.Random.Range(0, skillitemDB.skillitems.Count)];
            //itemDB.items¿¡¼­ itemsÀº ¹ºÁö
            result.items.Add(selected);
        }

        return result;
        //result´Â ¹ºÁö
    }*/
 
    public static SkillGachaResult Calculate(SkillDB skillDB, int count)
    {
        SkillGachaResult result = new();
        List<SkillInfo> AtypeSkill = skillDB.GetSkillGrade(SkillGrade.A);
        List<SkillInfo> BtypeSkill = skillDB.GetSkillGrade(SkillGrade.B);
        List<SkillInfo> CtypeSkill = skillDB.GetSkillGrade(SkillGrade.C);
        List<SkillInfo> DtypeSkill = skillDB.GetSkillGrade(SkillGrade.D);

        for (int i = 0; i < count; i++)
        {
            float roll = UnityEngine.Random.Range(0f, 1f);
            SkillInfo seleced;
            if (roll < 0.015) //1.5%
            {
                seleced = AtypeSkill[UnityEngine.Random.Range(0, AtypeSkill.Count)];
            }
            else if (roll < 0.05) // 3.5%
            {
                seleced = BtypeSkill[UnityEngine.Random.Range(0, BtypeSkill.Count)];
            }
            else if (roll < 0.2) //15 %
            {
                seleced = CtypeSkill[UnityEngine.Random.Range(0, CtypeSkill.Count)];
            }
            else //85%
            {
                seleced = DtypeSkill[UnityEngine.Random.Range(0, DtypeSkill.Count)];

            }
            result.items.Add(seleced);
        }
        return result;
    }
}


