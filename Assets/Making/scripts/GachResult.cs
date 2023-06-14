using Assets.HeroEditor.InventorySystem.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using Unity.VisualScripting;

public class GachaResult //가차를 돌린 결과(가챠에서 나온 아이템들 리스트)
{
    // public List<ItemInfo> items = new List<ItemInfo>();
    public List<ItemInfo> items = new List<ItemInfo>();
}

internal class GachaCalculator
{
    public static GachaResult Calculate(ItemDB itemDB, int count, ItemType type)
    //public static GachaResult Calculate 이런건 뭐지

    {
        var result = new GachaResult();
        List<ItemInfo> items = itemDB.GetItemsByType(type);
        //var random = new Random(); //ranmdom 객체를 만듬
        for (int i = 0; i < count; ++i)
        {
            //ItemInfo selected = itemDB.items[random.Next(0, itemDB.items.Count)];
            ItemInfo selected = items[UnityEngine.Random.Range(0, items.Count)];
            //itemDB.items에서 items은 뭔지
            result.items.Add(selected);
        }

        return result;
        //result는 뭔지
    }
}
