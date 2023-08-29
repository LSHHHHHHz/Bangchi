
using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class EquipAndEnforcePopup : MonoBehaviour
{
    public ItemSlot targetSlot;
    private static EquipAndEnforcePopup instance;
    
    private void Awake()
    {
        
    }
    public static void EquipAndEnforce(ItemSlot item)
    {
        if (instance == null)
        {
            var equipEnforcePrefab = Resources.Load<GameObject>("EquipAndEnforceUIPopup");
            instance = Instantiate(equipEnforcePrefab).GetComponent<EquipAndEnforcePopup>();
        }
        instance.targetSlot = item;
    }
    public void EquipOrUnEquip()
    {
        InventoryManager.instance.UnEquip(targetSlot.itemInfo);
        InventoryManager.instance.Equip(targetSlot.itemInfo);
    }
    public void Enforce()
    {
        /*for (int i = 0; i < itemInstance.Count; i++)
        {
            if (itemNum.itemInstance.count >= 5)
            {
                itemNum.itemInstance.count -= 5;

                // countText를 업데이트합니다.
                if (itemNum.countText != null)
                {
                    itemNum.countText.text = itemNum.itemInstance.count.ToString();
                }
                if (itemNum.itemInstance.count == 0)
                {
                    itemNum.countText.gameObject.SetActive(false);
                }
            }
            else
            {
                return;
            }
        }*/


        var currentSlot = targetSlot;
        if (currentSlot.itemInfo.Number >= EquipmentUI.instance.weaponSlots.Length)
        {
            return;
        }

        var nextSlot = EquipmentUI.instance.weaponSlots[currentSlot.itemInfo.Number];
        if (currentSlot.count >= 5) 
        {
            if (nextSlot.itemInfo == null)
            {
                int nextItemNumber = currentSlot.itemInfo.Number + 1;
                var nextItem = ItemDB.instance.GetItemInfoByNumber(nextItemNumber);
                if (nextItem != null)
                {
                    InventoryManager.instance.RemoveItem(currentSlot.itemInfo, 5);
                    ItemInstance nextItemInstance = InventoryManager.instance.AddItem(nextItem);
                    nextSlot.SetData(nextItemInstance);
                }
            }
            else
            {
                InventoryManager.instance.RemoveItem(currentSlot.itemInfo, 5);
                InventoryManager.instance.AddItem(nextSlot.itemInfo);
            }

            InventoryManager.instance.Save();
        }

        //if (currentSlot.countText != null)
        //{
        //    currentSlot.countText.text = currentSlot.count.ToString();
        //}
        //if (currentSlot.count == 0)
        //{
        //    currentSlot.countText.gameObject.SetActive(false);
        //}
    }
       
    public void Exit()
    {
        Destroy(gameObject);
    }
}