
using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    public Player player;
    public GameObject[] WeaponPoint;
    
    public ItemDB itemDb;

    public ItemSlot[] weaponSlots;
    public ItemSlot[] shieldSlots;

    public RectTransform weaponSlotParent;
    public RectTransform shieldSlotParent;

    public RectTransform weaponParent;
    public RectTransform shieldParent;

    public Sprite lockedSprite;

    public CharacterStats characterStats;
    public bool runGacha = false;
    
    GachaPopup gachaPopup;
    GachaResult gachaResult;

    public static EquipmentUI instance;
    private void Awake()
    {
        instance = this;
        List<ItemSlot> childList = new();
        for (int i = 0; i < weaponSlotParent.childCount; ++i) // weaponSlotParent�� �ڽ� ������ ��������, �� ������ŭ for�� �ݺ�
        {
            ItemSlot child = weaponSlotParent.GetChild(i).GetComponent<ItemSlot>();
            var button = child.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                EquipAndEnforcePopup.EquipAndEnforce(child);
            });

            childList.Add(child); // �ڽ��� childList�� �ӽ÷� �־�д�.
        }

        weaponSlots = childList.ToArray(); //�ڽĵ��� ����ִ� childList�� �迭 ��ȯ�� ��ȯ�Ѵ�.


        List<ItemSlot> shieldChildList = new List<ItemSlot>();
        for (int i = 0; i < shieldSlotParent.childCount; ++i)
        {
            ItemSlot child = shieldSlotParent.GetChild(i).GetComponent<ItemSlot>();
            var button = child.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                EquipAndEnforcePopup.EquipAndEnforce(child);
            });

            shieldChildList.Add(child);
        }
        shieldSlots = shieldChildList.ToArray();
    }
    private void Start() 
    {
        InventoryManager.instance.OnInventoryChanged += OnInventoryChangedCallback;

        SetData();
    }


    //------------------------------------------------------------------------------------------------
   
    public void EquipOrUnequip(ItemSlot item)
    {
        SetEquipSlot(item.itemInfo);
        InventoryManager.instance.UnEquip(item.itemInfo); // �����ϰ� �ִ� Į�̳� ���е� ���� ����.
        InventoryManager.instance.Equip(item.itemInfo); // ����.
    }

    //�κ��丮 UI �����۵��� ������
    public void SetData()
    {
        foreach (ItemInstance item in InventoryManager.instance.myItems)
        {
            int number = item.itemInfo.Number;
            if (item.itemInfo.type == ItemType.Sword)
            {
                ItemSlot slot = weaponSlots[number - 1];
                slot.SetData(item);
                
            }
            else if (item.itemInfo.type == ItemType.Shield)
            {
                ItemSlot slot = shieldSlots[number - 1];
                slot.SetData(item);
            }
        }
    }

    private void SetEquipSlot(ItemInfo itemInfo) //ĳ���� ����â �ֱ�
    {
        //characterStats.OnEquipItem(itemInfo);
    }
    private void OnInventoryChangedCallback()
    {
        SetData();
    }
    private void RunGacha(int count, ItemType type, Action<int> oneMoreTime)
    {
        if (!runGacha)
        {
            runGacha = true;
            if (gachaPopup == null)
            {
                var prefab = Resources.Load<GameObject>("GachaPopup");

                gachaPopup = Instantiate(prefab).GetComponent<GachaPopup>();

            }
            gachaPopup.PanelFadeIn();
            GachaResult gachaResult = GachaCalculator.Calculate(itemDb, count, type);

            // ��í�� ���� ���� �������� �κ��丮�� �ϳ��� �߰�
            foreach (var item in gachaResult.items)
            {
                InventoryManager.instance.AddItem(item);
            }

            // �κ��丮�� �� �߰������� ����
            InventoryManager.instance.Save();

            // ��í�˾����� ���� �����۵��� ������� �ϹǷ� gachaResult�� �ѱ�.
            gachaPopup.Initialize(gachaResult, oneMoreTime, () => runGacha = false);
        }
    }
    public void RunGacha_Sword(int count)
    {
        RunGacha(count, ItemType.Sword, RunGacha_Sword);
        Achievement.instance.ItemGachaCount += count;
    }
    public void RunGacha_Shield(int count)
    {
        RunGacha(count, ItemType.Shield, RunGacha_Shield);
        Achievement.instance.ItemGachaCount += count;
    }
    public void weaponInventoryOn()
    {
        weaponParent.localPosition = new Vector3(0, 0, 0);
        shieldParent.localPosition = new Vector3(1000f, 1000f, 1000f);
    }
    public void shieldInventoryOn()
    {
        shieldParent.localPosition = new Vector3(0, 0, 0);
        weaponParent.localPosition = new Vector3(1000f, 1000f, 1000f);
    }
}

