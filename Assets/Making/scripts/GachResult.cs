using Assets.HeroEditor.InventorySystem.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using Unity.VisualScripting;

public class GachaResult //������ ���� ���(��í���� ���� �����۵� ����Ʈ)
{
    // public List<ItemInfo> items = new List<ItemInfo>();
    public List<ItemInfo> items = new List<ItemInfo>();
    public List<ItemInfo> calculateItems = new(); 
}

internal class GachaCalculator
{
  /*  public static GachaResult Calculate(ItemDB itemDB, int count, ItemType type)
    //public static GachaResult Calculate �̷��� ����

    {
        var result = new GachaResult();
        List<ItemInfo> items = itemDB.GetItemsByType(type);
        //var random = new Random(); //ranmdom ��ü�� ����
        for (int i = 0; i < count; ++i)
        {
            //ItemInfo selected = itemDB.items[random.Next(0, itemDB.items.Count)];
            ItemInfo selected = items[UnityEngine.Random.Range(0, items.Count)];
            //itemDB.items���� items�� ����
            result.items.Add(selected);
        }

        return result;
        //result�� ����
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
            float roll = UnityEngine.Random.Range(0f, 1f); // 0 to 1�� �������� ���� ���� ����ϴ�.

            ItemInfo selected;

            if (roll < 0.005f) // 0.5% Ȯ��
                selected = typeAItems[UnityEngine.Random.Range(0, typeAItems.Count)];
            else if (roll < 0.05f) // 4.5% Ȯ��
                selected = typeBItems[UnityEngine.Random.Range(0, typeBItems.Count)];
            else if (roll < 0.2f) // 15% Ȯ��
                selected = typeCItems[UnityEngine.Random.Range(0, typeCItems.Count)];
            else // 80% Ȯ��
                selected = typeDItems[UnityEngine.Random.Range(0, typeDItems.Count)];

            result.items.Add(selected);
        }

        return result;
    }
}
