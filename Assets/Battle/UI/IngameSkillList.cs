using Assets.Battle;
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
    public static IngameSkillList instance;

    public Sprite lockedSprite;
    public RectTransform activeSkillSlotParent;
    public RectTransform passiveSkillSlotParent;

    // ActiveSkill용 슬롯과 PassiveSkill용 슬롯을 저장해둔다.
    private SkillSlot[] activeSkillSlots;
    private SkillSlot[] passiveSkillSlots;
    // 내가 장착한 스킬들의 인스턴스를 가지고 있는다.
    // 가지고 있다가 스킬 버튼이 눌리면 스킬을 실행한다.
    public BaseSkill[] skills;

    private void Awake()
    {
        instance = this;
        // 슬롯을 얻어오는 과정.
        // activeSkillSlotParent, passiveSkillSlotParent를 가지고 슬롯을 얻어옴.
        // Parent의 자식들을 순회하며 SkillSlot을 가져오고 Button 클릭시 처리도 연결한다.
        activeSkillSlots = GetChildSlots(activeSkillSlotParent);
        int skillIndex = 0;
        for (int i = 0; i < activeSkillSlots.Length; ++i)
        {
            SkillSlot activeSkillSlot = activeSkillSlots[i];
            var button = activeSkillSlot.GetComponent<Button>();
            int index = skillIndex;
            button.onClick.AddListener(() => OnSkillButtonClicked(index));
            ++skillIndex;
        }

        passiveSkillSlots = GetChildSlots(passiveSkillSlotParent);
        for (int i = 0; i < passiveSkillSlots.Length; ++i)
        {
            SkillSlot passiveSkillSlot = passiveSkillSlots[i];
            var button = passiveSkillSlot.GetComponent<Button>();
            int index = skillIndex;
            button.onClick.AddListener(() => OnSkillButtonClicked(index));
            ++skillIndex;
        }
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
        // 최초 1회만 이벤트에 콜백을 등록함.
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

    public void SetSkills(SkillSlot[] skillSlots, List<BaseSkill> skillsList, SkillType skillType)
    {
        var equippedSkillList = skillType  == SkillType.Active ? SkillInventoryManager.instance.equippedActiveSkills : SkillInventoryManager.instance.equippedPassiveSkills;

        // skillSlots : 4개, equippedSkills : 2개
        // i : 3                             / 0, 1
        for (int i = 0; i < skillSlots.Length; ++i)
        {
            SkillSlot slot = skillSlots[i];
            // i < equipSkills.Count
            // 0 < 2 ==> true
            // 1 < 2 ==> true
            // 2 < 2 ==> false

            // equippedSkillList가 기본적으로는 4개, 처음 게임을 시작했을 때 / 인벤토리에서 장착을 아직 안했을 때 
            // i가 equippedSkillList보다 크다면 내가 대응할 수 있는 범위가 아니니까 어차피 장착하지 않은 것이므로 null 값을 사용.
            SkillInstance equipSkill = i < equippedSkillList.Count ? equippedSkillList[i] : null;
            if (equipSkill != null)
            {
                // 장착할 스킬이 있음
                slot.SetData(equipSkill); // 스킬 슬롯에 내가 장착한 스킬을 설정한다.

                GameObject skillPrefab = equipSkill.skillInfo.skillPrefab;
                if (skillPrefab == null)
                {
                    Debug.LogError($"SkillPrefab is null : {equipSkill.skillInfo.name}");
                    skillsList.Add(null);
                }
                else
                {
                    GameObject skillObject = Instantiate(skillPrefab);
                    var skill = skillObject.GetComponent<BaseSkill>();
                    skill.owner = UnitManager.instance.player; // 플레이어로 스킬 주인 설정
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
            Player.instance.OnUseSkill(skill);
        }
    }
}