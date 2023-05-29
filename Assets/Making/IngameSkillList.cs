using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class IngameSkillList : MonoBehaviour
{
    public Sprite lockedSprite;
    public RectTransform weaponSlotParent;

    private SkillSlot[] skillSlots;

    private void Awake()
    {
        List<SkillSlot> childList = new();
        for (int i = 0; i < weaponSlotParent.childCount; ++i) // weaponSlotParent의 자식 개수를 가져오고, 그 개수만큼 for문 반복
        {
            SkillSlot child = weaponSlotParent.GetChild(i).GetComponent<SkillSlot>();
            childList.Add(child); // 자식을 childList에 임시로 넣어둔다.
        }

        skillSlots = childList.ToArray(); //자식들이 들어있는 childList를 배열 변환로 변환한다.
    }

    private void Start()
    {
        SkillInventoryManager.instance.OnEquippedSkillsChanged += OnEquippedSkillsChanged;
        Refresh();
    }

    private void OnEquippedSkillsChanged()
    {
        Refresh();
    }
        
    private void Refresh()
    {
        for (int i = 0; i < skillSlots.Length; ++i)
        {
            SkillSlot slot = skillSlots[i];
            if (i < SkillInventoryManager.instance.equippedSkills.Count)
            {
                // 장착할 스킬이 있음
                SkillInstance skillInstance = SkillInventoryManager.instance.equippedSkills[i];
                slot.SetData(skillInstance);
            }
            else
            {
                // 장착할 스킬이 없음
                slot.icon.sprite = lockedSprite;
            }
        }
    }
}