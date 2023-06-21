using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using System;
using System.Linq;

[Serializable] // 클래스를 json등 데이터로 저장할 때 [Serializable]을 붙여줘야 함.
public class InventoryData //이것도 뭐지
{
    public List<ItemInstance> myItems = new();  //유니티 Inventory Manager에서 My Items는 어디에 있고 어떻게 쓰는건지
    public List<ItemInstance> equippedItems = new();

}
public class InventoryManager : MonoBehaviour
{
    public event Action OnInventoryChanged; //event 있고 없고 차이 확인
    public event Action OnEquippedItemChanged;

    public static InventoryManager instance;
    public List<ItemInstance> myItems = new();
    public List<ItemInstance> equippedItems = new(); //장착 아이템 리스트

    public void Awake()
    {
        instance = this;
        Load();
    }

    //-----------------------------------------------------------------------------------------------
    public void AddItem(ItemInfo itemInfo) //인벤토리를 변경하는 메서드
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

        OnInventoryChanged?.Invoke(); 
    }

    //-----------------------------------------------------------------------------------------------

    // 게임을 저장할 때, 아이템 획득시 저장해주면 됨
    public void Save()   
    {
        var inventoryData = new InventoryData();
        inventoryData.myItems = myItems;
        inventoryData.equippedItems = equippedItems;
        string json = JsonUtility.ToJson(inventoryData);
        // PlayerPrefs : 데이터를 저장하고 불러오는데 쓰는 클래스
        PlayerPrefs.SetString("InventoryData", json);
        PlayerPrefs.Save();
    }

    // 게임을 처음에 켰을 때 내 아이템들 불러오기
    private void Load()
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

            for (int i = 0; i < inventoryData.equippedItems.Count; ++i)
            {
                var item = inventoryData.equippedItems[i];
                if (item.itemInfo == null)
                    continue;

                equippedItems.Add(item);
            }

            //myItems = inventoryData.myItems;
        }
    }

    public void Equip(ItemInfo itemInfo)
    {
        ItemInstance existItem = myItems.Find(item => item.itemInfo == itemInfo); //
        //IEnumerable<ItemInstance> existItems = myItems.Where(item => item.itemInfo == itemInfo);
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

    public void UnEquip(ItemType type)
    {
        for (int i = 0; i < equippedItems.Count;)
        {
            ItemInstance equippedItem = equippedItems[i];
            if (equippedItem.itemInfo.type == type)
            {
                equippedItems.RemoveAt(i);
                continue;
            }

            ++i;
        }

        OnEquippedItemChanged?.Invoke();
    }
}
