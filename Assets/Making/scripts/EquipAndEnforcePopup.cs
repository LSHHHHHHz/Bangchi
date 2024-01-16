
using Assets.Item1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipAndEnforcePopup : MonoBehaviour
{
    public ItemSlot targetSlot;
    public ItemInfo originitemInfo;
    private static EquipAndEnforcePopup instance;
    private bool buttonPressed = false;
    public float interval = 0.1f;
    
    private void Awake()
    {
        
    }
    private void Start()
    {
        originitemInfo = InventoryManager.instance.equippedItems[0].itemInfo;
    }
    private void OnEnable()
    {
        Equips.OnExitEquip += Exit;
    }

    private void OnDisable()
    {
        Equips.OnExitEquip -= Exit;
    }

    public void OnPressDown()
    {
        buttonPressed = true;
        StartCoroutine(RepeatEnforce());
    }
    public void OnPointerUp()
    {
        buttonPressed = false;
    }
    private IEnumerator RepeatEnforce()
    {
        while (buttonPressed)
        {
            Enforce(); // 반복적으로 호출하고 싶은 함수
            yield return new WaitForSeconds(interval);
        }
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

    //플레이어 무기 장착 시 데미지 추가
    public void EquipOrUnEquip()
    {
        InventoryManager.instance.UnEquip(originitemInfo);
        InventoryManager.instance.UnEquip(targetSlot.itemInfo);
        InventoryManager.instance.Equip(targetSlot.itemInfo);
        originitemInfo = targetSlot.itemInfo;
        Player.instance.Current_Attack += targetSlot.itemInfo.Attack;
        Player.instance.Current_Attack -= originitemInfo.Attack;
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