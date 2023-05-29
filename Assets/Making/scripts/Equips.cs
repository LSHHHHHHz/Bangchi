using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 내 장비들을 보여주는 UI.
public class Equips : MonoBehaviour
{
    public RectTransform uiGroup;
    public RectTransform weaponselectuiGroup;
    public RectTransform talkText;
    public Player enterPlayer;

    public RectTransform weaponSlotParent;

    private ItemSlot[] weaponSlots;


    //부모 게임오브젝트 만들고 아래 16개 자식 오브젝트 만듬
    //
    private void Awake()
    {
        // 무기 슬롯이 16개가 있는데, 그것의 부모 GameObject가 weaponSlotParent이다.
        // 반대로 말하면 weaponSlotParent의 자식들을 가지고 오면 그것들은 무기 슬롯이다.

        List<ItemSlot> childList = new();
        for (int i = 0; i < weaponSlotParent.childCount; ++i) // weaponSlotParent의 자식 개수를 가져오고, 그 개수만큼 for문 반복
        {
            ItemSlot child = weaponSlotParent.GetChild(i).GetComponent<ItemSlot>();
            childList.Add(child); // 자식을 childList에 임시로 넣어둔다.
        }

        weaponSlots = childList.ToArray(); //자식들이 들어있는 childList를 배열 변환로 변환한다.

    }

    private void Start() //왜 Start에 넣는거지?
    {
        // 4-16
        // 인벤토리가 바뀌었을 때 UI를 갱신할 수 있도록 이벤트 콜백(특정 시점에 호출되는 함수) 등록.
        InventoryManager.instance.OnInventoryChanged += OnInventoryChangedCallback;
        // 4-16


        // 인벤토리가 로드된 이후 데이터를 설정해야 해서 Awake가 아닌 Start에다가 함수 추가
        SetData();
    }

    // 4-16
    private void OnInventoryChangedCallback()
    {
        SetData();
    }

    public void EnterEquip()
    {
        uiGroup.anchoredPosition = new Vector3(0, 1670, 0);
        Debug.Log("클릭");
    }

    public void ExitEquip()
    {
        uiGroup.anchoredPosition = new Vector3(2049, -1607, 0);
    }

    public void EnterWeaponSelect()
    {
        weaponselectuiGroup.anchoredPosition = new Vector3(0, 625, 0);
    }

    public void ExitWeaponSelect()
    {
        weaponselectuiGroup.anchoredPosition = new Vector3(-1687, 625, 0);
    }

    // 인벤토리에 있는 아이템들을 UI에 설정하는 기능.
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
