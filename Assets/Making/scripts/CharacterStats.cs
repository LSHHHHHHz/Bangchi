
using Assets.HeroEditor.Common.Scripts.Common;
using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;
public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public ColleaguePoly[] colleaguePoly;

    public Text _Attack;
    public Text _HP;
    public Text _HPRecovery;
    public Text _CriticalDamage;
    public Text _Criticalprobability;
    public Text _MP;
    public Text _MPRecovery;
    public Text _CoinGetAmount; 
    public Text _ExpGetAmount;  

    public RectTransform characterUI;
    public RectTransform characterUIClose;

    public ItemSlot equipWeaponSlot; //무기 슬롯을 담을 변수
    public ItemSlot equipShieldSLot;
    private ItemSlot currentWeapon;
    private ItemSlot currentShield;

    void Start()
    {
        RefreshWeapon();
        InventoryManager.instance.OnEquippedItemChanged += RefreshWeapon;
    }

    void Update()
    {
        Text_Stats();
    }
    void Text_Stats()
    {
        _Attack.text = player.Current_Attack + ""; 
        _HP.text = player.Max_HP + "";
        _HPRecovery.text = player.RecoveryHP + "";
        _CriticalDamage.text = player.Current_CriticalDamage + "";
        _Criticalprobability.text = player.Current_Criticalprobability.ToString("F2") + "%";
        _MP.text = player.Max_MP + "";
        _MPRecovery.text = player.RecoveryMP + "";
        _CoinGetAmount.text = player.Coin.ToString("N0");
        _ExpGetAmount.text = player.Max_Exp.ToString("N0");
    }

    private void RefreshWeapon()
    {
        foreach (ItemInstance equippedItem in InventoryManager.instance.equippedItems)
        {
            OnEquipItem(equippedItem.itemInfo);
        }
    }

    private void OnEquipItem(ItemInfo itemInfo)
    {
        if (itemInfo.type == ItemType.Sword)
        {
            ItemSlot equipSlot = equipWeaponSlot;
            if (equipSlot.itemInfo == itemInfo)
            {
                return;
            }
            else
            {
                equipWeaponSlot.SetData(itemInfo);
                equipSlot.backgroundImage.SetActive(false);
            }

        }
        else if (itemInfo.type == ItemType.Shield)
        {
            ItemSlot equipSlot = equipShieldSLot;
            if (equipSlot.itemInfo == itemInfo)
            {
                return;
            }
            else
            {
                equipShieldSLot.SetData(itemInfo);
                equipSlot.backgroundImage.SetActive(false);
            }
        }
    }
    public void statsUIopen()
    {
        characterUI.localPosition = new Vector3(306, 422, 0);
    }
    public void statsUIclose()
    {
        characterUI.localPosition = new Vector3(-620, 455, 0);
    }

}

