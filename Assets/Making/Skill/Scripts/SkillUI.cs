
using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public SkillDB skillDB;
    public Sprite lockedSprite;
    public RectTransform activeSkillSlotParent; //인벤토리에서 엑티브 슬롯들의 부모 
    public RectTransform passiveSkillSlotParent; //인벤토리에서 패시브 슬롯들의 부모
    public RectTransform equippedActiveSkillSlotParent; //엑티브 스킬 슬롯
    public RectTransform equippedPassiveSkillSlotParent; //패시브 스킬 슬롯

    public RectTransform passiveSkillUI;
    public RectTransform activeSkillUI;

    SkillGachaPopup skillgachaPopup;

    SkillSlot[] activeSkillSlots;
    SkillSlot[] passiveSkillSlots;

    SkillSlot[] equippedActiveSkillSlots; // 4칸
    SkillSlot[] equippedPassiveSkillSlots; // 4칸


    private void Awake()
    {
        // 무기 슬롯이 16개가 있는데, 그것의 부모 GameObject가 weaponSlotParent이다.
        // 반대로 말하면 weaponSlotParent의 자식들을 가지고 오면 그것들은 무기 슬롯이다.

        List<SkillSlot> childList = new();
        for (int i = 0; i < activeSkillSlotParent.childCount; ++i) // weaponSlotParent의 자식 개수를 가져오고, 그 개수만큼 for문 반복
        {
            SkillSlot child = activeSkillSlotParent.GetChild(i).GetComponent<SkillSlot>();
            var button = child.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                EquipOrUnequip(child);
            });
            childList.Add(child); // 자식을 childList에 임시로 넣어둔다.
        }
        activeSkillSlots = GetChildSlots(activeSkillSlotParent);
        /*
        //SkillSlot타입을 갖는 리스트를 childList변수에 넣음.
        //activeSkillSlotParent의 child 수만큼 for문을 돌림
        //activeSkillSlotParent의 child의 SkillSlot 컴포넌트를 갖고온 후 child 변수에 넣음(0~childCount)
        //child의 Button 컴포넌트를 button변수에 넣음
        //button이 호출될 때 EquipOrUnequip(child)가 호출되어 child는 SkillSlot타입을 갖는 slot변수에 넣어짐
        //childList에 child를 추가함
        */



        childList = new();
        for (int i = 0; i < passiveSkillSlotParent.childCount; ++i) // weaponSlotParent의 자식 개수를 가져오고, 그 개수만큼 for문 반복
        {
            SkillSlot child = passiveSkillSlotParent.GetChild(i).GetComponent<SkillSlot>();
            var button = child.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                EquipOrUnequip(child);
            });
            childList.Add(child); // 자식을 childList에 임시로 넣어둔다.
        }
        passiveSkillSlots = childList.ToArray(); //자식들이 들어있는 childList를 배열 변환로 변환한다.

        childList = new();
        for (int i = 0; i < equippedActiveSkillSlotParent.childCount; ++i) // weaponSlotParent의 자식 개수를 가져오고, 그 개수만큼 for문 반복
        {
            SkillSlot child = equippedActiveSkillSlotParent.GetChild(i).GetComponent<SkillSlot>();
            childList.Add(child); // 자식을 childList에 임시로 넣어둔다.
        }
        equippedActiveSkillSlots = childList.ToArray(); //자식들이 들어있는 childList를 배열 변환로 변환한다.

        childList = new();
        for (int i = 0; i < equippedPassiveSkillSlotParent.childCount; ++i) // weaponSlotParent의 자식 개수를 가져오고, 그 개수만큼 for문 반복
        {
            SkillSlot child = equippedPassiveSkillSlotParent.GetChild(i).GetComponent<SkillSlot>();
            childList.Add(child); // 자식을 childList에 임시로 넣어둔다.
        }
        equippedPassiveSkillSlots = childList.ToArray(); //자식들이 들어있는 childList를 배열 변환로 변환한다.

        //activeSkillSlots = GetChildSlots(activeSkillSlotParent);
        //passiveSkillSlots = GetChildSlots(passiveSkillSlotParent);
        //equippedActiveSkillSlots = GetChildSlots(equippedActiveSkillSlotParent);
        //equippedPassiveSkillSlots = GetChildSlots(equippedPassiveSkillSlotParent);
    }
    private SkillSlot[] GetChildSlots(RectTransform parent)
    {
        List<SkillSlot> childList = new();

        for (int i = 0; i < parent.childCount; ++i) // weaponSlotParent의 자식 개수를 가져오고, 그 개수만큼 for문 반복
        {
            SkillSlot child = parent.GetChild(i).GetComponent<SkillSlot>();
            childList.Add(child); // 자식을 childList에 임시로 넣어둔다.
        }

        return childList.ToArray();
    }
    /*
    //SkillSlot 배열 타입을 갖는 GetChildSlots 매서드
    //SkillSlot 타입의 리스트 객체인 childList
    //RectTransform parent 매개변수 안에 변수를 넣음?
    //넣어진 변수(부모)의 자식 개수를 for으로 돌림
    //parent의 GetChild(i)의 SkillSlot 컴포넌트를 갖고와서 child 변수에 넣음
    //childList에 child를 넣음
    //넣어진 child는 childList에서 관리하며 이걸 배열 형식으로 변환시켜 반환함
    */

    private void Start()
    {
        // 4-16
        // 인벤토리가 바뀌었을 때 UI를 갱신할 수 있도록 이벤트 콜백(특정 시점에 호출되는 함수) 등록.
        SkillInventoryManager.instance.OnSkillInventoryChanged += OnSkillInventoryChangedCallback;
        // 4-16

        // 인벤토리가 로드된 이후 데이터를 설정해야 해서 Awake가 아닌 Start에다가 함수 추가
        SetData();
    }

    public void RunGacha(int count)
    {
        if (skillgachaPopup == null)
        {
            var prefab = Resources.Load<GameObject>("SkillPopup"); //Resources 말고 다른곳에서 저장하는방법
            
            skillgachaPopup = Instantiate(prefab).GetComponent<SkillGachaPopup>();

        }

        SkillGachaResult skillgachaResult = SkillGGachaCalculator.Calculate(skillDB, count);

        // 가챠를 통해 얻은 아이템을 인벤토리에 하나씩 추가
        foreach (var item in skillgachaResult.items)
        {
            SkillInventoryManager.instance.AddSkill(item);
        }

        // 인벤토리에 다 추가했으면 저장
        SkillInventoryManager.instance.Save();

        // 가챠팝업에서 뽑은 아이템들을 보여줘야 하므로 gachaResult를 넘김.
        skillgachaPopup.Initialize(skillgachaResult, this.RunGacha);
    }

    private void OnSkillInventoryChangedCallback()
    {
        SetData();
    }

    // 장착 혹은 장착 해제
    private void EquipOrUnequip(SkillSlot slot)
    {
        SetEquipSlot(slot.skillInfo, out bool isEquiped);
        if (isEquiped)
        {
            SkillInventoryManager.instance.EquipSkill(slot.skillInfo);
        }
        else
        {
            SkillInventoryManager.instance.UnEquipSkill(slot.skillInfo);
        }
    }

    // 인벤토리에 있는 아이템들을 UI에 설정하는 기능.
    public void SetData()
    {
        // 내가 갖고 있는 아이템들을 foreach문으로 순회한다.
        // 내가 갖고 있는 아이템은 InventoryManager.instance.myItems에 들어있다.
        foreach (SkillInstance item in SkillInventoryManager.instance.myItems)
        {
            // number : 2 /  weaponSlots 배열엔 0~15까지 들어있다. number2에 해당하는 weaponSlots 값은 1이다.(0부터 시작하니까 -1을 해줌).
            // number값  : 1부터 시작, 배열은 0부터 시작하기 때문에 -1을 해줘야 값이 맞음.
            int number = item.skillInfo.Number;
            if (item.skillInfo.type == SkillType.Active)
            {
                SkillSlot slot = activeSkillSlots[number - 1];
                slot.SetData(item);
            }
            else if (item.skillInfo.type == SkillType.Passive)
            {
                SkillSlot slot = passiveSkillSlots[number - 1];
                slot.SetData(item);
            }
        }

        foreach (SkillInstance equipedItem in SkillInventoryManager.instance.equippedSkills)
        {
            SetEquipSlot(equipedItem.skillInfo, out bool isEquiped);
        }
    }

    private void SetEquipSlot(SkillInfo skillInfo, out bool isEquiped)
    {
        isEquiped = false;
        if (skillInfo.type == SkillType.Active)
        {
            int emptyIndex = -1;
            for (int i = 0; i < equippedActiveSkillSlots.Length; ++i)
            {
                SkillSlot equipSlot = equippedActiveSkillSlots[i];
                if (equipSlot.skillInfo == skillInfo)
                {
                    // 장착돼있다는 뜻, 장착 해제.
                    equipSlot.SetEmpty(lockedSprite);
                    equipSlot.skillInfo = null;
                    isEquiped = false;
                    return;
                }
                if (emptyIndex == -1 && equipSlot.skillInfo == null) //slot는 인벤토리, equipslot는 장착슬롯
                                                                     //    if (equipSlot.skillInfo == null) //slot는 인벤토리, equipslot는 장착슬롯
                    emptyIndex = i; // 만약 비어있는 칸이 있다면 index 저장
            }
            // 저장된 비어있는 칸이 있다면 사용
            if (emptyIndex != -1)
            {
                equippedActiveSkillSlots[emptyIndex].SetData(skillInfo);
                isEquiped = true;
            }
            else
            {
                // 빈칸이 없다는 뜻.
            }
        }
        else if (skillInfo.type == SkillType.Passive)
        {
            int emptyIndex = -1;
            for (int i = 0; i < equippedPassiveSkillSlots.Length; ++i)
            {
                SkillSlot equipSlot = equippedPassiveSkillSlots[i];
                if (equipSlot.skillInfo == skillInfo)
                {
                    // 장착돼있다는 뜻, 장착 해제.
                    equipSlot.SetEmpty(lockedSprite);
                    equipSlot.skillInfo = null;
                    isEquiped = false;
                    return;
                }

                if (emptyIndex == -1 && equipSlot.skillInfo == null) //slot는 인벤토리, equipslot는 장착슬롯
                                                                     //if (equipSlot.skillInfo == null)
                    emptyIndex = i; // 만약 비어있는 칸이 있다면 index 저장
            }

            // 저장된 비어있는 칸이 있다면 사용
            if (emptyIndex != -1)
            {
                SkillSlot emptySlot = equippedPassiveSkillSlots[emptyIndex];
                emptySlot.SetData(skillInfo);
                isEquiped = true;
                //equippedPassiveSkillSlots[emptyIndex].SetData(slot.skillInfo);
            }
            else
            {
                // 빈칸이 없다는 뜻.
            }
        }
    }

    /*
    //SkillInventoryManager에 들어있는 myItems들을 foreach문을 돌려 SkillInstance 타입의 item 변수에 넣는다.
    //number변수에 item의 스킬정보 Number를 넣는다.
    //만약 SkillType가 Active라면 SkillSlot 타입의 slot변수에 activeSkillSlots에 들어있는 childList에 들어있는 것을 넣는다.
    //item은 SkillInstance로 받았으며, SkillSlot에 있는 SetData(SkillInstance skillInstance) 매개변수가 호출되어 사용된다.
    */

    public void PassiveSkillOn()
    {
        passiveSkillUI.localPosition = new Vector3(0, 0, 0);
        activeSkillUI.localPosition = new Vector3(1000f, 1000f, 1000f);
    }
    public void ActiveSkillOn()
    {
        passiveSkillUI.localPosition = new Vector3(1000f, 1000f, 1000f);
        activeSkillUI.localPosition = new Vector3(0, 0, 0);
    }
}

