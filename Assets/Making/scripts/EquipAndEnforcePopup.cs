
using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EquipAndEnforcePopup : MonoBehaviour
{
    EquipAndEnforcePopup EquipAndEnforceUI;
    public ItemSlot itemNum;
    
    private void Awake()
    {
    }
    public void EquipAndEnforce(ItemSlot item)
    {
        itemNum = item;
        if (EquipAndEnforceUI == null)
        {
            var EquipEnforcePrefab = Resources.Load<GameObject>("EquipAndEnforceUIPopup");
            EquipAndEnforceUI = Instantiate(EquipEnforcePrefab).GetComponent<EquipAndEnforcePopup>();
        }
        else
        {
            Destroy(EquipAndEnforceUI.gameObject);
            EquipAndEnforceUI = null;
            var EquipEnforcePrefab = Resources.Load<GameObject>("EquipAndEnforceUIPopup");
            EquipAndEnforceUI = Instantiate(EquipEnforcePrefab).GetComponent<EquipAndEnforcePopup>();
        }
    }
    public void EquipOrUnEquip()
    {
        InventoryManager.instance.UnEquip(itemNum.itemInfo);
        InventoryManager.instance.Equip(itemNum.itemInfo);
    }
    public void Enforce()
    {
    }
    public void Exit()
    {
        Destroy(gameObject);
    }
}