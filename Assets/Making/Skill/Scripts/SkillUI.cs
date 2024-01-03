
using Assets.HeroEditor.Common.Scripts.Common;
using Assets.Item1;
using Assets.Making.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class SkillUI : MonoBehaviour
{
    public static SkillUI instance;
    public delegate void ResetSkillDelegate(int value);
    public event ResetSkillDelegate resetSkill;

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
        instance = this;

        List<SkillSlot> childList = new();
        for (int i = 0; i < activeSkillSlotParent.childCount; ++i) 
        {
            SkillSlot child = activeSkillSlotParent.GetChild(i).GetComponent<SkillSlot>();
            var button = child.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                EquipOrUnequip(child);
            });
            childList.Add(child);
        }
        activeSkillSlots = GetChildSlots(activeSkillSlotParent);



        childList = new();
        for (int i = 0; i < passiveSkillSlotParent.childCount; ++i) 
        {
            SkillSlot child = passiveSkillSlotParent.GetChild(i).GetComponent<SkillSlot>();
            var button = child.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                EquipOrUnequip(child);
            });
            childList.Add(child);
        }
        passiveSkillSlots = childList.ToArray(); 

        childList = new();
        for (int i = 0; i < equippedActiveSkillSlotParent.childCount; ++i) 
        {
            SkillSlot child = equippedActiveSkillSlotParent.GetChild(i).GetComponent<SkillSlot>();
            childList.Add(child);
            
        }
        equippedActiveSkillSlots = childList.ToArray();

        childList = new();
        for (int i = 0; i < equippedPassiveSkillSlotParent.childCount; ++i) 
        {
            SkillSlot child = equippedPassiveSkillSlotParent.GetChild(i).GetComponent<SkillSlot>();
            childList.Add(child); 
        }
        equippedPassiveSkillSlots = childList.ToArray();
    }
    private SkillSlot[] GetChildSlots(RectTransform parent)
    {
        List<SkillSlot> childList = new();

        for (int i = 0; i < parent.childCount; ++i)
        {
            SkillSlot child = parent.GetChild(i).GetComponent<SkillSlot>();
            childList.Add(child); 
        }

        return childList.ToArray();
    }
    private void Start()
    {
        SkillInventoryManager.instance.OnSkillInventoryChanged += OnSkillInventoryChangedCallback;
        SetData();
    }

    public void RunGacha(int count)
    {
        if (skillgachaPopup == null)
        {
            var prefab = Resources.Load<GameObject>("SkillPopup"); 
            
            skillgachaPopup = Instantiate(prefab, RootCanvas.Instance.transform).GetComponent<SkillGachaPopup>();

        }

        SkillGachaResult skillgachaResult = SkillGGachaCalculator.Calculate(skillDB, count);

        // 가챠를 통해 얻은 아이템을 인벤토리에 하나씩 추가
        foreach (var item in skillgachaResult.items)
        {
            SkillInventoryManager.instance.AddSkill(item);
        }
        SkillInventoryManager.instance.Save();

        skillgachaPopup.Initialize(skillgachaResult, this.RunGacha);
    }

    private void OnSkillInventoryChangedCallback()
    {
        SetData();
    }

    // 장착 혹은 장착 해제
    private void EquipOrUnequip(SkillSlot slot)
    {
        SetEquipSlot(slot.skillInfo.type, slot.skillInfo, out bool isEquiped);
        List<SkillInfo> equippedSkills = new();
        if (slot.skillInfo.type == SkillType.Active)
        {
            foreach (SkillSlot equippedSlot in equippedActiveSkillSlots)
            {
                equippedSkills.Add(equippedSlot.skillInfo);
            }

            SkillInventoryManager.instance.ChangeEquipSkillSet(SkillType.Active, equippedSkills);
        }
        else if (slot.skillInfo.type == SkillType.Passive)
        {
            foreach (SkillSlot equippedSlot in equippedPassiveSkillSlots)
            {
                equippedSkills.Add(equippedSlot.skillInfo);
            }

            SkillInventoryManager.instance.ChangeEquipSkillSet(SkillType.Passive, equippedSkills);
        }
    }

    // 인벤토리에 있는 아이템들을 UI에 설정하는 기능.
    public void SetData()
    {
        foreach (SkillInstance item in SkillInventoryManager.instance.myItems)
        {
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

        for (int i = 0; i < SkillInventoryManager.instance.equippedActiveSkills.Count; ++i)
        {
            SkillInstance equipSkill = SkillInventoryManager.instance.equippedActiveSkills[i];
            SkillSlot equipSlot = equippedActiveSkillSlots[i];
            if (equipSkill != null)
            {
                equipSlot.SetData(equipSkill);
                equipSlot.countText.SetActive(false);
            }
            else
            {
                equipSlot.SetEmpty(lockedSprite);
            }
        }

        for (int i = 0; i < SkillInventoryManager.instance.equippedPassiveSkills.Count; ++i)
        {
            SkillInstance equipSkill = SkillInventoryManager.instance.equippedPassiveSkills[i];
            SkillSlot equipSlot = equippedPassiveSkillSlots[i];
            if (equipSkill != null)
            {
                equipSlot.SetData(equipSkill);
                equipSlot.countText.SetActive(false);
            }
            else
            {
                equipSlot.SetEmpty(lockedSprite);
            }
        }
    }

    private void SetEquipSlot(SkillType type, SkillInfo skillInfo, out bool isEquiped)
    {
        isEquiped = false;
        if (type == SkillType.Active)
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
                {
                    emptyIndex = i; // 만약 비어있는 칸이 있다면 index 저장                    
                }
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
        else if (type == SkillType.Passive)
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
            }
            else
            {
                // 빈칸이 없다는 뜻.
            }
        }
    }
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

