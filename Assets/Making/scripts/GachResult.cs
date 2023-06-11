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
    public List<ItemInfo> itemsSH = new List<ItemInfo>();
}

internal class GachaCalculator
{
    public static GachaResult Calculate(ItemDB itemDB, int count)
    //public static GachaResult Calculate 이런건 뭐지

    {
        var result = new GachaResult();
        //var random = new Random(); //ranmdom 객체를 만듬
        UnityEngine.Random.Range(0, count);
        for (int i = 0; i < count; ++i)
        {
            //ItemInfo selected = itemDB.items[random.Next(0, itemDB.items.Count)];
            ItemInfo selected = itemDB.items[UnityEngine.Random.Range(0, itemDB.items.Count)];
            //itemDB.items에서 items은 뭔지
            result.items.Add(selected);
        }

        return result;
        //result는 뭔지
    }

    //추가 여기로 와야함, 아이템을 뽑을 때 여기서 랜덤으로 아이템 뽑기가 나옴
    public static GachaResult CalculateSH(ItemDB_SH itemDB_SH, int count)
    {
        var result = new GachaResult();

        for (int i = 0; i < count; ++i)
        {
            ItemInfo selected = itemDB_SH.items[UnityEngine.Random.Range(0, itemDB_SH.items.Count)];
            result.itemsSH.Add(selected);
        }

        return result;
    }
}
