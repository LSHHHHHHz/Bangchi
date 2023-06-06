
using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    Player player;
    public ItemDB itemDb;
    public ItemDB_SH itemDB_SH;

    ItemSlot[] weaponSlots;
    ItemSlot[] shieldSlots;

    public RectTransform weaponSlotParent;
    public RectTransform shieldSlotParent;

    public RectTransform weaponParent;
    public RectTransform shieldParent;

    GachaPopup gachaPopup;
    GachaResult gachaResult;

    private void Awake()
    {
        InventoryManager.instance.Load();
        // 무기 슬롯이 16개가 있는데, 그것의 부모 GameObject가 weaponSlotParent이다.
        // 반대로 말하면 weaponSlotParent의 자식들을 가지고 오면 그것들은 무기 슬롯이다.

        List<ItemSlot> childList = new();
        for (int i = 0; i < weaponSlotParent.childCount; ++i) // weaponSlotParent의 자식 개수를 가져오고, 그 개수만큼 for문 반복
        {
            ItemSlot child = weaponSlotParent.GetChild(i).GetComponent<ItemSlot>();

            var button = child.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                ChangeEquip(child);
            });

            childList.Add(child); // 자식을 childList에 임시로 넣어둔다.
        }

        weaponSlots = childList.ToArray(); //자식들이 들어있는 childList를 배열 변환로 변환한다.

    }
    private void Start() //왜 Start에 넣는거지?
    {
        // 인벤토리가 바뀌었을 때 UI를 갱신할 수 있도록 이벤트 콜백(특정 시점에 호출되는 함수) 등록.
        InventoryManager.instance.OnInventoryChanged += OnInventoryChangedCallback;
     
        // 인벤토리가 로드된 이후 데이터를 설정해야 해서 Awake가 아닌 Start에다가 함수 추가
        SetData();
    }

    private void ChangeEquip(ItemSlot itemSlot)
    {
        if (itemSlot.itemInfo.type == ItemType.Sword)
        {
            int emptyIndex = -1;
            for (int i = 0; i < weaponSlots.Length; ++i) 
            {
                ItemSlot equipSlot = weaponSlots[i];
                if(equipSlot.itemInfo == itemSlot.itemInfo)
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
    private void OnInventoryChangedCallback()
    {
        SetData();
    }
    public void RunGacha(int count)
    {
        if (gachaPopup == null)
        {
            //GachaPopup1 GameObject를 불러와서 prefab 변수에 넣는다.
            var prefab = Resources.Load<GameObject>("GachaPopup");

            // Instantiate(prefab) : prefab을 Instantiate 한다.
            // Instantiate 함수 원형 : GameObject Instantiate(GameObject original);
            // prefab을 Instantiate한 후 해당 GameObect를 반환하고 이 GameObject에서 GachaPopup GetComponent를 가져와서 gachaPopup에 넣는다.
            gachaPopup = Instantiate(prefab).GetComponent<GachaPopup>();
        }

        GachaResult gachaResult = GachaCalculator.Calculate(itemDb, count);

        // 가챠를 통해 얻은 아이템을 인벤토리에 하나씩 추가
        foreach (var item in gachaResult.items)
        {
            InventoryManager.instance.AddItem(item);
        }

        // 인벤토리에 다 추가했으면 저장
        InventoryManager.instance.Save();

        // 가챠팝업에서 뽑은 아이템들을 보여줘야 하므로 gachaResult를 넘김.
        gachaPopup.Initialize(gachaResult, this.RunGacha);
    }

    public void SetData()
    {
        // 내가 갖고 있는 아이템들을 foreach문으로 순회한다.
        // 내가 갖고 있는 아이템은 InventoryManager.instance.myItems에 들어있다.
        foreach (ItemInstance item in InventoryManager.instance.myItems)
        {
            // number : 2 /  weaponSlots 배열엔 0~15까지 들어있다. number2에 해당하는 weaponSlots 값은 1이다.(0부터 시작하니까 -1을 해줌).
            // number값  : 1부터 시작, 배열은 0부터 시작하기 때문에 -1을 해줘야 값이 맞음.
            int number = item.itemInfo.Number;
            ItemSlot slot = weaponSlots[number - 1];
            slot.SetData(item);
        }
    }
    
}

