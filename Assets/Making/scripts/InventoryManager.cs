using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using System;

public class InventoryData //이것도 뭐지
{
    public List<ItemInstance> myItems = new();  //유니티 Inventory Manager에서 My Items는 어디에 있고 어떻게 쓰는건지
}
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<ItemInstance> myItems = new();

    public void Awake()
    {
        instance = this;
    }


    public void AddItem(ItemInfo itemInfo)
    {        
        ItemInstance existItem = myItems.Find(item => item.itemInfo == itemInfo);
        if (existItem != null)
        {
            existItem.count++;
        }
        else
        {
            myItems.Add(new ItemInstance()
            {
                itemInfo = itemInfo,
                count = 1,
                upgradeLevel = 1
            });
        }
    }

    // 게임을 저장할 때, 아이템 획득시 저장해주면 됨
    public void Save()
    {
        var inventoryData = new InventoryData();
        inventoryData.myItems = myItems;

        string json = JsonUtility.ToJson(inventoryData);

        // PlayerPrefs : 데이터를 저장하고 불러오는데 쓰는 클래스
        PlayerPrefs.SetString("InventoryData", json);
        PlayerPrefs.Save();
    }

    // 게임을 처음에 켰을 때 내 아이템들 불러오기
    public void Load()
    {
        string json = PlayerPrefs.GetString("InventoryData");
        // 게임을 처음할 때는 저장된 데이터가 없을 수 있으니
        // 문자열이 비어있지 않을 때에만 처리
        if (string.IsNullOrEmpty(json) == false)
        {
            // json이 값이 들어있다는 뜻
            var inventoryData = JsonUtility.FromJson<InventoryData>(json);
            for (int i = 0; i < inventoryData.myItems.Count; ++i)
            {
                var item = inventoryData.myItems[i];
                if (item.itemInfo == null)
                    continue;

                myItems.Add(item);
            }

            //myItems = inventoryData.myItems;
        }
    }
}
