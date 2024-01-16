using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using System;
using System.Linq;

[Serializable] // 클래스를 json등 데이터로 저장할 때 [Serializable]을 붙여줘야 함.
public class InventoryData 
{
    public List<ItemInstance> myItems = new();  
    public List<ItemInstance> equippedItems = new();
    public List<ItemInstance> colleague = new();

}
public class InventoryManager : MonoBehaviour
{
    public event Action OnInventoryChanged; 
    public event Action OnEquippedItemChanged;

    public static InventoryManager instance;
    public List<ItemInstance> myItems = new();
    public List<ItemInstance> equippedItems = new(); //장착 아이템 리스트
    public List<ItemInstance> colleague = new();

    public int originalWeaponAddDamage = 100;
    public void Awake()
    {
        instance = this;
        Load();
    }
    public ItemInstance AddItem(ItemInfo itemInfo, int count = 1) //인벤토리를 변경하는 메서드
    {   
        ItemInstance itemInstance = myItems.Find(item => item.itemInfo == itemInfo);
        if (itemInstance != null)
        {
            itemInstance.count += count;
        }
        else
        {
            itemInstance = new ItemInstance()
            {
                itemInfo = itemInfo,
                count = count,
            };
            myItems.Add(itemInstance);
        }

        OnInventoryChanged?.Invoke();
        return itemInstance;
    }

    public ItemInstance RemoveItem(ItemInfo itemInfo, int count = 1)
    {
        ItemInstance itemInstance = myItems.Find(item => item.itemInfo == itemInfo);
        if (itemInstance != null)
        {
            if (itemInstance.count < count)
                throw new Exception($"Not enough item : {itemInfo.name}");

            itemInstance.count -= count;
        }
        else
        {
            throw new Exception($"Don't have item : {itemInfo.name}");
        }

        OnInventoryChanged?.Invoke();
        return itemInstance;
    }
    public void Save()   
    {
        var inventoryData = new InventoryData();
        inventoryData.myItems = myItems;
        inventoryData.equippedItems = equippedItems;
        string json = JsonUtility.ToJson(inventoryData);

        PlayerPrefs.SetString("InventoryData", json);
        PlayerPrefs.Save();
    }
    private void Load()
    {
        string json = PlayerPrefs.GetString("InventoryData");

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

            for (int i = 0; i < inventoryData.equippedItems.Count; ++i)
            {
                var item = inventoryData.equippedItems[i];
                if (item.itemInfo == null)
                    continue;

                equippedItems.Add(item);
            }
        }
    }
 

    public void Equip(ItemInfo itemInfo)
    {
        ItemInstance existItem = myItems.Find(item => item.itemInfo == itemInfo); 
        if (existItem == null)
        {
            // 아이템을 가지고 있지 않다는 것!
            throw new Exception($"Item not found : {itemInfo.name}");
        }

        equippedItems.Add(existItem);
        
        OnEquippedItemChanged?.Invoke();

        Save();
    }

    public void UnEquip(ItemInfo itemInfo)
    {
        ItemInstance existItem = equippedItems.Find(item => item.itemInfo == itemInfo);
        if (existItem == null)
        {
            // 장착을 안했다는 얘기
            return;
        }

        equippedItems.Remove(existItem);
        OnEquippedItemChanged?.Invoke();
        Save();
    }
}
