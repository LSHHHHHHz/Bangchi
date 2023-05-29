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
    public static SkillGachaResult Calculate(SkillDB skillitemDB, int count)
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
    }
}
