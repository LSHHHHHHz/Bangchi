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
    public List<ItemInfo> calculateItems = new(); 
}

internal class GachaCalculator
{
  /*  public static GachaResult Calculate(ItemDB itemDB, int count, ItemType type)
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
    }*/

    public static GachaResult Calculate(ItemDB itemDB, int count, ItemType type)
    {
        var result = new GachaResult();

        List<ItemInfo> typeAItems = itemDB.GetItemGradeAndType(ItemGrade.A, type);
        List<ItemInfo> typeBItems = itemDB.GetItemGradeAndType(ItemGrade.B, type);
        List<ItemInfo> typeCItems = itemDB.GetItemGradeAndType(ItemGrade.C, type);
        List<ItemInfo> typeDItems = itemDB.GetItemGradeAndType(ItemGrade.D, type);

        for (int i = 0; i < count; ++i)
        {
            float roll = UnityEngine.Random.Range(0f, 1f); // 0 to 1의 범위에서 랜덤 값을 얻습니다.

            ItemInfo selected;

            if (roll < 0.005f) // 0.5% 확률
                selected = typeAItems[UnityEngine.Random.Range(0, typeAItems.Count)];
            else if (roll < 0.05f) // 4.5% 확률
                selected = typeBItems[UnityEngine.Random.Range(0, typeBItems.Count)];
            else if (roll < 0.2f) // 15% 확률
                selected = typeCItems[UnityEngine.Random.Range(0, typeCItems.Count)];
            else // 80% 확률
                selected = typeDItems[UnityEngine.Random.Range(0, typeDItems.Count)];

            result.items.Add(selected);
        }

        return result;
    }
}
