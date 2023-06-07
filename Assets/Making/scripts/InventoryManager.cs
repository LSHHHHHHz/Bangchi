using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using System;

[Serializable] // Ŭ������ json�� �����ͷ� ������ �� [Serializable]�� �ٿ���� ��.
public class InventoryData //�̰͵� ����
{
    public List<ItemInstance> myItems = new();  //����Ƽ Inventory Manager���� My Items�� ��� �ְ� ��� ���°���
    public List<ItemInstance> myitemsSH = new();
}
public class InventoryManager : MonoBehaviour
{
    public event Action OnInventoryChanged; //event �ְ� ���� ���� Ȯ��

    public static InventoryManager instance;
    public List<ItemInstance> myItems = new();
    public List<ItemInstance> myItemsSH = new();

    public void Awake()
    {
        instance = this;
    }


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

    public void AddItemSH(ItemInfo itemInfo) //�κ��丮�� �����ϴ� �޼���
    {
        ItemInstance existItem = myItemsSH.Find(item => item.itemInfo == itemInfo);
        if (existItem != null)
        {
            existItem.count++;
        }
        else
        {
            myItemsSH.Add(new ItemInstance()
            {
                itemInfo = itemInfo,
                count = 1,
                upgradeLevel = 1
            });
        }

        OnInventoryChanged?.Invoke();
    }



    // ������ ������ ��, ������ ȹ��� �������ָ� ��
    public void Save()
    {
        var inventoryData = new InventoryData();
        inventoryData.myItems = myItems;

        string json = JsonUtility.ToJson(inventoryData);

        // PlayerPrefs : �����͸� �����ϰ� �ҷ����µ� ���� Ŭ����
        PlayerPrefs.SetString("InventoryData", json);
        PlayerPrefs.Save();
    }

    // ������ ó���� ���� �� �� �����۵� �ҷ�����
    public void Load()
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

            //myItems = inventoryData.myItems;
        }
    }
}
