using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class IngameSkillList : MonoBehaviour
{
    public Sprite lockedSprite;
    public RectTransform activeSkillSlotParent;
    public RectTransform passiveSkillSlotParent;

    private SkillSlot[] activeSkillSlots;
    private SkillSlot[] passiveSkillSlots;
    private BaseSkill[] skills;

    private void Awake()
    {
        activeSkillSlots = GetChildSlots(activeSkillSlotParent);
        passiveSkillSlots = GetChildSlots(passiveSkillSlotParent);
    }

    private SkillSlot[] GetChildSlots(RectTransform parent) //스킬 슬롯에 저장하게 하는 매서드??
    {
        List<SkillSlot> childList = new();

        for (int i = 0; i < parent.childCount; ++i) // weaponSlotParent의 자식 개수를 가져오고, 그 개수만큼 for문 반복
        {
            SkillSlot child = parent.GetChild(i).GetComponent<SkillSlot>();
            childList.Add(child); // SkillSlot이 있는 자식을 childList에 임시로 넣어둔다.
        }

        return childList.ToArray();
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
        ClearSkills(); // 기존에 가지고 있던 스킬 오브젝트 삭제
        List<BaseSkill> skills = new();
        SetSkills(activeSkillSlots, skills, SkillType.Active);
        SetSkills(passiveSkillSlots, skills, SkillType.Passive);
        this.skills = skills.ToArray();
    }

    private void SetSkills(SkillSlot[] skillSlots, List<BaseSkill> skillsList, SkillType skillType)
    {
        List<SkillInstance> equippedSkills = new();
        foreach (SkillInstance skillInstance in SkillInventoryManager.instance.equippedSkills)
        {
            if (skillInstance.skillInfo.type == skillType)
            {
                equippedSkills.Add(skillInstance);
            }
        }

        for (int i = 0; i < skillSlots.Length; ++i)
        {
            SkillSlot slot = skillSlots[i];
            if (i < equippedSkills.Count)
            {
                // 장착할 스킬이 있음
                SkillInstance skillInstance = equippedSkills[i];
                slot.SetData(skillInstance);

                GameObject skillPrefab = skillInstance.skillInfo.skillPrefab;
                if (skillPrefab == null)
                {
                    Debug.LogError($"SkillPrefab is null : {skillInstance.skillInfo.name}");
                }
                else
                {
                    GameObject skillObject = Instantiate(skillPrefab);
                    var skill = skillObject.GetComponent<BaseSkill>();
                    skillsList.Add(skill);
                }
            }
            else
            {
                // 장착할 스킬이 없음
                slot.icon.sprite = lockedSprite;
                skillsList.Add(null);
            }
        }
    }

    private void ClearSkills()
    {
        if (skills == null)
            return;

        foreach (BaseSkill skill in skills)
        {
            if (skill == null)
                continue;

            Destroy(skill.gameObject);
        }

        skills = null;
    }

    private void OnSkillButtonClicked(int index)
    {
        BaseSkill skill = skills[index];
        if (skill != null)
        {
            skill.Execute();
        }
    }
}