
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
    public RectTransform activeSkillSlotParent; //�κ��丮���� ��Ƽ�� ���Ե��� �θ� 
    public RectTransform passiveSkillSlotParent; //�κ��丮���� �нú� ���Ե��� �θ�
    public RectTransform equippedActiveSkillSlotParent; //��Ƽ�� ��ų ����
    public RectTransform equippedPassiveSkillSlotParent; //�нú� ��ų ����

    public RectTransform passiveSkillUI;
    public RectTransform activeSkillUI;

    SkillGachaPopup skillgachaPopup;

    SkillSlot[] activeSkillSlots;
    SkillSlot[] passiveSkillSlots;

    SkillSlot[] equippedActiveSkillSlots; // 4ĭ
    SkillSlot[] equippedPassiveSkillSlots; // 4ĭ

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

        // ��í�� ���� ���� �������� �κ��丮�� �ϳ��� �߰�
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

    // ���� Ȥ�� ���� ����
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

    // �κ��丮�� �ִ� �����۵��� UI�� �����ϴ� ���.
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
                    // �������ִٴ� ��, ���� ����.
                    equipSlot.SetEmpty(lockedSprite);
                    equipSlot.skillInfo = null;
                    isEquiped = false;


                    return;
                }
                if (emptyIndex == -1 && equipSlot.skillInfo == null) //slot�� �κ��丮, equipslot�� ��������
                                                                     //    if (equipSlot.skillInfo == null) //slot�� �κ��丮, equipslot�� ��������
                {
                    emptyIndex = i; // ���� ����ִ� ĭ�� �ִٸ� index ����                    
                }
            }
            // ����� ����ִ� ĭ�� �ִٸ� ���
            if (emptyIndex != -1)
            {
                equippedActiveSkillSlots[emptyIndex].SetData(skillInfo);
                isEquiped = true;
            }
            else
            {
                // ��ĭ�� ���ٴ� ��.
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
                    // �������ִٴ� ��, ���� ����.
                    equipSlot.SetEmpty(lockedSprite);
                    equipSlot.skillInfo = null;
                    isEquiped = false;
                    return;
                }

                if (emptyIndex == -1 && equipSlot.skillInfo == null) //slot�� �κ��丮, equipslot�� ��������
                                                                     //if (equipSlot.skillInfo == null)
                    emptyIndex = i; // ���� ����ִ� ĭ�� �ִٸ� index ����
            }

            // ����� ����ִ� ĭ�� �ִٸ� ���
            if (emptyIndex != -1)
            {
                SkillSlot emptySlot = equippedPassiveSkillSlots[emptyIndex];
                emptySlot.SetData(skillInfo);
                isEquiped = true;
            }
            else
            {
                // ��ĭ�� ���ٴ� ��.
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

