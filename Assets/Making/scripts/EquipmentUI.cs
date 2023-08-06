
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

    ItemSlot[] weaponSlots;
    ItemSlot[] shieldSlots;

    
    public RectTransform weaponSlotParent;
    public RectTransform shieldSlotParent;

    public RectTransform weaponParent;
    public RectTransform shieldParent;

    public Sprite lockedSprite;
    public RectTransform EquipCharacterSword;
    public RectTransform EquipCharacterShield;

    ItemSlot equipWeaponSlot;
    ItemSlot equipShieldSLot;


    GachaPopup gachaPopup;
    GachaResult gachaResult;


    //GachaPopupShield
    private void Awake()
    {

       
        // ���� ������ 16���� �ִµ�, �װ��� �θ� GameObject�� weaponSlotParent�̴�.
        // �ݴ�� ���ϸ� weaponSlotParent�� �ڽĵ��� ������ ���� �װ͵��� ���� �����̴�.

        List<ItemSlot> childList = new();
        for (int i = 0; i < weaponSlotParent.childCount; ++i) // weaponSlotParent�� �ڽ� ������ ��������, �� ������ŭ for�� �ݺ�
        {
            ItemSlot child = weaponSlotParent.GetChild(i).GetComponent<ItemSlot>();
            var button = child.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                EquipOrUnequip(child);
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
                EquipOrUnequip(child);
            });

            shieldChildList.Add(child);
        }
        shieldSlots = shieldChildList.ToArray();

        childList = new();
        equipWeaponSlot = EquipCharacterSword.GetComponent<ItemSlot>();

        childList = new();
        equipShieldSLot = EquipCharacterShield.GetComponent<ItemSlot>();

    }
    private void Start() //�� ilStart�� �ִ°���?
    {
        // �κ��丮�� �ٲ���� �� UI�� ������ �� �ֵ��� �̺�Ʈ �ݹ�(Ư�� ������ ȣ��Ǵ� �Լ�) ���.
        InventoryManager.instance.OnInventoryChanged += OnInventoryChangedCallback;

        // �κ��丮�� �ε�� ���� �����͸� �����ؾ� �ؼ� Awake�� �ƴ� Start���ٰ� �Լ� �߰�
        SetData();
    }

    private void ChangeEquip(ItemSlot itemSlot)
    {
        // itemSlot�� null�� ��Ȳ�� �����ΰ�?
        // ������. ������ �α�
        // itemInfo�� null�� ��Ȳ��?
        //  - ������ ������ ���� �ȵ� �� - ����ִٴ� ��
        // ����ִ� �������� �����ϴ� ���� �Ұ���.
        if (itemSlot.itemInfo == null)
        {
            // ����ִ� �������� ������ �� ����.
            return;
        }


        if (itemSlot.itemInfo.type == ItemType.Sword)
        {
            int emptyIndex = -1;
            for (int i = 0; i < weaponSlots.Length; ++i)
            {
                ItemSlot equipSlot = weaponSlots[i];
                if (equipSlot.itemInfo == itemSlot.itemInfo)
                {
                    return;
                }
                if (emptyIndex == -1 || equipSlot.itemInfo == null)
                {
                    emptyIndex = i;
                }

            }
        }
    }

    //------------------------------------------------------------------------------------------------
   
    public void EquipOrUnequip(ItemSlot item)
    {
        SetEquipSlot(item.itemInfo);
        InventoryManager.instance.UnEquip(item.itemInfo); // �����ϰ� �ִ� Į�̳� ���е� ���� ����.
        InventoryManager.instance.Equip(item.itemInfo); // ����.
    }


    public void SetData()
    {
        // ���� ���� �ִ� �����۵��� foreach������ ��ȸ�Ѵ�.
        // ���� ���� �ִ� �������� InventoryManager.instance.myItems�� ����ִ�.

        foreach (ItemInstance item in InventoryManager.instance.myItems)
        {
            // number : 2 /  weaponSlots �迭�� 0~15���� ����ִ�. number2�� �ش��ϴ� weaponSlots ���� 1�̴�.(0���� �����ϴϱ� -1�� ����).
            // number��  : 1���� ����, �迭�� 0���� �����ϱ� ������ -1�� ����� ���� ����.
            int number = item.itemInfo.Number;
            // weaponSlots �迭�� [number - 1] ��° ��Ҹ� ������ slot ������ �ִ´�.
            // ���(element) : �迭�ȿ� ����ִ� ����
            // �迭(array) : ���� Ÿ���� ��ҵ��� ���������� ������ ������.
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

    private void SetEquipSlot(ItemInfo iteminfo)
    {
        if(iteminfo.type == ItemType.Sword)
        {
            int emptyIndex = -1;
            ItemSlot equipSlot = equipWeaponSlot;
            if(equipSlot.itemInfo == iteminfo)
            {
                return;
            }
            else
            {
                equipWeaponSlot.SetData(iteminfo);
            }
           
            
        }
    }

    //------------------------------------------------------------------------------------------------
    private void OnInventoryChangedCallback()
    {
        SetData();
    }
    private void RunGacha(int count, ItemType type, Action<int> oneMoreTime)
    {

        if (gachaPopup == null)
        {
            //GachaPopup1 GameObject�� �ҷ��ͼ� prefab ������ �ִ´�.
            var prefab = Resources.Load<GameObject>("GachaPopup");

            // Instantiate(prefab) : prefab�� Instantiate �Ѵ�.
            // Instantiate �Լ� ���� : GameObject Instantiate(GameObject original);
            // prefab�� Instantiate�� �� �ش� GameObect�� ��ȯ�ϰ� �� GameObject���� GachaPopup GetComponent�� �����ͼ� gachaPopup�� �ִ´�.
            gachaPopup = Instantiate(prefab).GetComponent<GachaPopup>();

        }

        GachaResult gachaResult = GachaCalculator.Calculate(itemDb, count, type);

        // ��í�� ���� ���� �������� �κ��丮�� �ϳ��� �߰�
        foreach (var item in gachaResult.items)
        {
            InventoryManager.instance.AddItem(item);
        }

        // �κ��丮�� �� �߰������� ����
        InventoryManager.instance.Save();

        // ��í�˾����� ���� �����۵��� ������� �ϹǷ� gachaResult�� �ѱ�.
        gachaPopup.Initialize(gachaResult, oneMoreTime);
    }

    public void RunGacha_Sword(int count)
    {
        RunGacha(count, ItemType.Sword, RunGacha_Sword);
    }

    public void RunGacha_Shield(int count)
    {
        RunGacha(count, ItemType.Shield, RunGacha_Shield);
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

