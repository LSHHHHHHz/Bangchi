
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
        // ���� ������ 16���� �ִµ�, �װ��� �θ� GameObject�� weaponSlotParent�̴�.
        // �ݴ�� ���ϸ� weaponSlotParent�� �ڽĵ��� ������ ���� �װ͵��� ���� �����̴�.

        List<SkillSlot> childList = new();
        for (int i = 0; i < activeSkillSlotParent.childCount; ++i) // weaponSlotParent�� �ڽ� ������ ��������, �� ������ŭ for�� �ݺ�
        {
            SkillSlot child = activeSkillSlotParent.GetChild(i).GetComponent<SkillSlot>();
            var button = child.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                EquipOrUnequip(child);
            });
            childList.Add(child); // �ڽ��� childList�� �ӽ÷� �־�д�.
        }
        activeSkillSlots = GetChildSlots(activeSkillSlotParent);
        /*
        //SkillSlotŸ���� ���� ����Ʈ�� childList������ ����.
        //activeSkillSlotParent�� child ����ŭ for���� ����
        //activeSkillSlotParent�� child�� SkillSlot ������Ʈ�� ����� �� child ������ ����(0~childCount)
        //child�� Button ������Ʈ�� button������ ����
        //button�� ȣ��� �� EquipOrUnequip(child)�� ȣ��Ǿ� child�� SkillSlotŸ���� ���� slot������ �־���
        //childList�� child�� �߰���
        */



        childList = new();
        for (int i = 0; i < passiveSkillSlotParent.childCount; ++i) // weaponSlotParent�� �ڽ� ������ ��������, �� ������ŭ for�� �ݺ�
        {
            SkillSlot child = passiveSkillSlotParent.GetChild(i).GetComponent<SkillSlot>();
            var button = child.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                EquipOrUnequip(child);
            });
            childList.Add(child); // �ڽ��� childList�� �ӽ÷� �־�д�.
        }
        passiveSkillSlots = childList.ToArray(); //�ڽĵ��� ����ִ� childList�� �迭 ��ȯ�� ��ȯ�Ѵ�.

        childList = new();
        for (int i = 0; i < equippedActiveSkillSlotParent.childCount; ++i) // weaponSlotParent�� �ڽ� ������ ��������, �� ������ŭ for�� �ݺ�
        {
            SkillSlot child = equippedActiveSkillSlotParent.GetChild(i).GetComponent<SkillSlot>();
            childList.Add(child); // �ڽ��� childList�� �ӽ÷� �־�д�.
        }
        equippedActiveSkillSlots = childList.ToArray(); //�ڽĵ��� ����ִ� childList�� �迭 ��ȯ�� ��ȯ�Ѵ�.

        childList = new();
        for (int i = 0; i < equippedPassiveSkillSlotParent.childCount; ++i) // weaponSlotParent�� �ڽ� ������ ��������, �� ������ŭ for�� �ݺ�
        {
            SkillSlot child = equippedPassiveSkillSlotParent.GetChild(i).GetComponent<SkillSlot>();
            childList.Add(child); // �ڽ��� childList�� �ӽ÷� �־�д�.
        }
        equippedPassiveSkillSlots = childList.ToArray(); //�ڽĵ��� ����ִ� childList�� �迭 ��ȯ�� ��ȯ�Ѵ�.

        //activeSkillSlots = GetChildSlots(activeSkillSlotParent);
        //passiveSkillSlots = GetChildSlots(passiveSkillSlotParent);
        //equippedActiveSkillSlots = GetChildSlots(equippedActiveSkillSlotParent);
        //equippedPassiveSkillSlots = GetChildSlots(equippedPassiveSkillSlotParent);
    }
    private SkillSlot[] GetChildSlots(RectTransform parent)
    {
        List<SkillSlot> childList = new();

        for (int i = 0; i < parent.childCount; ++i) // weaponSlotParent�� �ڽ� ������ ��������, �� ������ŭ for�� �ݺ�
        {
            SkillSlot child = parent.GetChild(i).GetComponent<SkillSlot>();
            childList.Add(child); // �ڽ��� childList�� �ӽ÷� �־�д�.
        }

        return childList.ToArray();
    }
    /*
    //SkillSlot �迭 Ÿ���� ���� GetChildSlots �ż���
    //SkillSlot Ÿ���� ����Ʈ ��ü�� childList
    //RectTransform parent �Ű����� �ȿ� ������ ����?
    //�־��� ����(�θ�)�� �ڽ� ������ for���� ����
    //parent�� GetChild(i)�� SkillSlot ������Ʈ�� ����ͼ� child ������ ����
    //childList�� child�� ����
    //�־��� child�� childList���� �����ϸ� �̰� �迭 �������� ��ȯ���� ��ȯ��
    */

    private void Start()
    {
        // 4-16
        // �κ��丮�� �ٲ���� �� UI�� ������ �� �ֵ��� �̺�Ʈ �ݹ�(Ư�� ������ ȣ��Ǵ� �Լ�) ���.
        SkillInventoryManager.instance.OnSkillInventoryChanged += OnSkillInventoryChangedCallback;
        // 4-16

        // �κ��丮�� �ε�� ���� �����͸� �����ؾ� �ؼ� Awake�� �ƴ� Start���ٰ� �Լ� �߰�
        SetData();
    }

    public void RunGacha(int count)
    {
        if (skillgachaPopup == null)
        {
            var prefab = Resources.Load<GameObject>("SkillPopup"); //Resources ���� �ٸ������� �����ϴ¹��
            
            skillgachaPopup = Instantiate(prefab).GetComponent<SkillGachaPopup>();

        }

        SkillGachaResult skillgachaResult = SkillGGachaCalculator.Calculate(skillDB, count);

        // ��í�� ���� ���� �������� �κ��丮�� �ϳ��� �߰�
        foreach (var item in skillgachaResult.items)
        {
            SkillInventoryManager.instance.AddSkill(item);
        }

        // �κ��丮�� �� �߰������� ����
        SkillInventoryManager.instance.Save();

        // ��í�˾����� ���� �����۵��� ������� �ϹǷ� gachaResult�� �ѱ�.
        skillgachaPopup.Initialize(skillgachaResult, this.RunGacha);
    }

    private void OnSkillInventoryChangedCallback()
    {
        SetData();
    }

    // ���� Ȥ�� ���� ����
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

    // �κ��丮�� �ִ� �����۵��� UI�� �����ϴ� ���.
    public void SetData()
    {
        // ���� ���� �ִ� �����۵��� foreach������ ��ȸ�Ѵ�.
        // ���� ���� �ִ� �������� InventoryManager.instance.myItems�� ����ִ�.
        foreach (SkillInstance item in SkillInventoryManager.instance.myItems)
        {
            // number : 2 /  weaponSlots �迭�� 0~15���� ����ִ�. number2�� �ش��ϴ� weaponSlots ���� 1�̴�.(0���� �����ϴϱ� -1�� ����).
            // number��  : 1���� ����, �迭�� 0���� �����ϱ� ������ -1�� ����� ���� ����.
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
                    // �������ִٴ� ��, ���� ����.
                    equipSlot.SetEmpty(lockedSprite);
                    equipSlot.skillInfo = null;
                    isEquiped = false;
                    return;
                }
                if (emptyIndex == -1 && equipSlot.skillInfo == null) //slot�� �κ��丮, equipslot�� ��������
                                                                     //    if (equipSlot.skillInfo == null) //slot�� �κ��丮, equipslot�� ��������
                    emptyIndex = i; // ���� ����ִ� ĭ�� �ִٸ� index ����
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
        else if (skillInfo.type == SkillType.Passive)
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
                //equippedPassiveSkillSlots[emptyIndex].SetData(slot.skillInfo);
            }
            else
            {
                // ��ĭ�� ���ٴ� ��.
            }
        }
    }

    /*
    //SkillInventoryManager�� ����ִ� myItems���� foreach���� ���� SkillInstance Ÿ���� item ������ �ִ´�.
    //number������ item�� ��ų���� Number�� �ִ´�.
    //���� SkillType�� Active��� SkillSlot Ÿ���� slot������ activeSkillSlots�� ����ִ� childList�� ����ִ� ���� �ִ´�.
    //item�� SkillInstance�� �޾�����, SkillSlot�� �ִ� SetData(SkillInstance skillInstance) �Ű������� ȣ��Ǿ� ���ȴ�.
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

