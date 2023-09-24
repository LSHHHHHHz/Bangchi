
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
        var currentSlot = targetSlot;
        if (currentSlot.itemInfo.Number >= EquipmentUI.instance.weaponSlots.Length)
        {
            return;
        }

        var nextSlot = EquipmentUI.instance.weaponSlots[currentSlot.itemInfo.Number];
        if (currentSlot.count >= 5) 
        {
            Achievement.instance.FusionCount += 1;
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
    }
       
    public void Exit()
    {
        Destroy(gameObject);
    }
}