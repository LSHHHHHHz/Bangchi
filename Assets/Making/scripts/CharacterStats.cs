using Assets.HeroEditor.InventorySystem.Scripts.Elements;
using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;
public class CharacterStats : MonoBehaviour
{
    //public Player player;
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
    public Text _CoinGetAmount; //ȹ�淮�� ���⼭�� ����
    public Text _ExpGetAmount;  //ȹ�淮�� ���⼭�� ����

    public RectTransform characterUI;
    public RectTransform characterUIClose;

    public ItemSlot equipWeaponSlot; //���� ������ ���� ����
    public ItemSlot equipShieldSLot;


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
        _Attack.text = player.Current_Attack + ""; //�̹�� ���� �ٸ�����ִ���
        _HP.text = player.Max_HP + "";
        _HPRecovery.text = player.RecoveryHP + "";
        _CriticalDamage.text = player.Current_CriticalDamage + "";
        _Criticalprobability.text = player.Current_Criticalprobability + "";
        _MP.text = player.Max_MP + "";
        _MPRecovery.text = player.RecoveryMP + "";


        //�� �� ������ �߻��ϴ°���

        //_CoinGetAmount.text = colleaguePoly[1].Third_stat.ToString();


        //_ExpGetAmount.text = colleaguePoly[0].Third_stat + "";
    }

    public void characterOpen()
    {
        characterUI.localPosition = new Vector3(310, 455, 0);
    }

    public void characterClose()
    {
        characterUI.localPosition = new Vector3(-782, 455, 0);
    }

    private void RefreshWeapon()
    {
        foreach (ItemInstance equippedItem in InventoryManager.instance.equippedItems)
        {
            OnEquipItem(equippedItem.itemInfo);
        }
    }

    private void OnEquipItem(Assets.Item1.ItemInfo itemInfo)
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
            }
        }
    }

}

