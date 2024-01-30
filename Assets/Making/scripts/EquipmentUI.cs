
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
        for (int i = 0; i < weaponSlotParent.childCount; ++i) // weaponSlotParent의 자식 개수를 가져오고, 그 개수만큼 for문 반복
        {
            ItemSlot child = weaponSlotParent.GetChild(i).GetComponent<ItemSlot>();
            var button = child.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                EquipAndEnforcePopup.EquipAndEnforce(child);
                InventoryManager.instance.EquipItemInfo(child.itemInfo);
            });

            childList.Add(child); // 자식을 childList에 임시로 넣어둔다.
        }
        weaponSlots = childList.ToArray(); //자식들이 들어있는 childList를 배열 변환로 변환한다.

        List<ItemSlot> shieldChildList = new List<ItemSlot>();
        for (int i = 0; i < shieldSlotParent.childCount; ++i)
        {
            ItemSlot child = shieldSlotParent.GetChild(i).GetComponent<ItemSlot>();
            var button = child.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                EquipAndEnforcePopup.EquipAndEnforce(child);
                InventoryManager.instance.EquipItemInfo(child.itemInfo);
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
    public void EquipOrUnequip(ItemSlot item)
    {
        SetEquipSlot(item.itemInfo);
        //InventoryManager.instance.UnEquip(item.itemInfo); // 장착하고 있던 칼이나 방패등 장착 해제. 12345
        InventoryManager.instance.Equip(item.itemInfo); // 장착.
    }
    //인벤토리 UI 아이템들을 보여줌
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
    private void SetEquipSlot(ItemInfo itemInfo) //캐릭터 상태창 넣기
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
        var sw = System.Diagnostics.Stopwatch.StartNew();
            runGacha = true;
            if (gachaPopup == null)
            {
                var prefab = Resources.Load<GameObject>("GachaPopup");

                gachaPopup = Instantiate(prefab).GetComponent<GachaPopup>();

            }
            Debug.LogError($"Loop 1 : {sw.Elapsed.TotalMilliseconds} ms");
            sw.Restart(); // 초기화 및 시작
            gachaPopup.PanelFadeIn();
            GachaResult gachaResult = GachaCalculator.Calculate(itemDb, count, type);

            // 가챠를 통해 얻은 아이템을 인벤토리에 하나씩 추가
            foreach (var item in gachaResult.items)
            {
                InventoryManager.instance.AddItem(item);
            }

            // 인벤토리에 다 추가했으면 저장
            InventoryManager.instance.Save();
            Debug.LogError($"Loop 2 : {sw.Elapsed.TotalMilliseconds} ms");
            sw.Restart(); // 초기화 및 시작
            // 가챠팝업에서 뽑은 아이템들을 보여줘야 하므로 gachaResult를 넘김.
            gachaPopup.Initialize(gachaResult, oneMoreTime, () => runGacha = false);
            Debug.LogError($"Loop 3 : {sw.Elapsed.TotalMilliseconds} ms");
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

