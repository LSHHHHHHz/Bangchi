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
}

internal class GachaCalculator
{
    public static GachaResult Calculate(ItemDB itemDB, int count)
    //public static GachaResult Calculate �̷��� ����

    {
        var result = new GachaResult();
        //var random = new Random(); //ranmdom ��ü�� ����
        UnityEngine.Random.Range(0, count);
        for (int i = 0; i < count; ++i)
        {
            //ItemInfo selected = itemDB.items[random.Next(0, itemDB.items.Count)];
            ItemInfo selected = itemDB.items[UnityEngine.Random.Range(0, itemDB.items.Count)];
            //itemDB.items���� items�� ����
            result.items.Add(selected);
        }

        return result;
        //result�� ����
    }
}
