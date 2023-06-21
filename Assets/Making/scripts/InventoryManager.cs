using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using System;
using System.Linq;

[Serializable] // Ŭ������ json�� �����ͷ� ������ �� [Serializable]�� �ٿ���� ��.
public class InventoryData //�̰͵� ����
{
    public List<ItemInstance> myItems = new();  //����Ƽ Inventory Manager���� My Items�� ��� �ְ� ��� ���°���
    public List<ItemInstance> equippedItems = new();

}
public class InventoryManager : MonoBehaviour
{
    public event Action OnInventoryChanged; //event �ְ� ���� ���� Ȯ��
    public event Action OnEquippedItemChanged;

    public static InventoryManager instance;
    public List<ItemInstance> myItems = new();
    public List<ItemInstance> equippedItems = new(); //���� ������ ����Ʈ

    public void Awake()
    {
        instance = this;
        Load();
    }

    //-----------------------------------------------------------------------------------------------
    public void AddItem(ItemInfo itemInfo) //�κ��丮�� �����ϴ� �޼���
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

    // ������ ������ ��, ������ ȹ��� �������ָ� ��
    public void Save()   
    {
        var inventoryData = new InventoryData();
        inventoryData.myItems = myItems;
        inventoryData.equippedItems = equippedItems;
        string json = JsonUtility.ToJson(inventoryData);
        // PlayerPrefs : �����͸� �����ϰ� �ҷ����µ� ���� Ŭ����
        PlayerPrefs.SetString("InventoryData", json);
        PlayerPrefs.Save();
    }

    // ������ ó���� ���� �� �� �����۵� �ҷ�����
    private void Load()
    {
        string json = PlayerPrefs.GetString("InventoryData");
        // ������ ó���� ���� ����� �����Ͱ� ���� �� ������
        // ���ڿ��� ������� ���� ������ ó��
        if (string.IsNullOrEmpty(json) == false)
        {
            // json�� ���� ����ִٴ� ��
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
            // �������� ������ ���� �ʴٴ� ��!
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
            // ������ ���ߴٴ� ���
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
