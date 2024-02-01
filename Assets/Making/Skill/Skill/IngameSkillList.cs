using Assets.Battle;
using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.HeroEditor.Common.Scripts.Common;

public class IngameSkillList : MonoBehaviour
{
    bool isAllskillAutomatic = false;
    public GameObject AutomaticON;
    public GameObject AutomaticOFF;

    public static IngameSkillList instance;

    public Sprite lockedSprite;
    public RectTransform activeSkillSlotParent;
    public RectTransform passiveSkillSlotParent;

    // ActiveSkill용 슬롯과 PassiveSkill용 슬롯을 저장
    public SkillSlot[] activeSkillSlots;
    public SkillSlot[] passiveSkillSlots;
    // 내가 장착한 스킬들의 인스턴스를 가지고 있는다.
    // 가지고 있다가 스킬 버튼이 눌리면 스킬을 실행한다.
    public BaseSkill[] skills;

    private void Awake()
    {
        instance = this;
        activeSkillSlots = GetChildSlots(activeSkillSlotParent);
        AddClickListenersToSkillSlots(activeSkillSlots, 0);

        passiveSkillSlots = GetChildSlots(passiveSkillSlotParent);
        AddClickListenersToSkillSlots(passiveSkillSlots, 4);
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
    void AddClickListenersToSkillSlots(SkillSlot[] slots, int startIndex)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            SkillSlot slot = slots[i];
            Button button = slot.GetComponent<Button>();
            int index = i+ startIndex;
            button.onClick.AddListener(()=>OnSkillButtonClicked(slot, index));
        }
    }
    private void Update()
    {
        if (isAllskillAutomatic)
        {
            allskillAutomatic();
        }
    }

    public void AutomaticOn()
    {
        isAllskillAutomatic = true;
        AutomaticON.SetActive(false);
        AutomaticOFF.SetActive(true);
    }
    public void AutomaticOff()
    {
        isAllskillAutomatic = false;
        AutomaticON.SetActive(true);
        AutomaticOFF.SetActive(false);
    }
    public void allskillAutomatic()
    {
        SkillProcessSlot(activeSkillSlots, 0);
        SkillProcessSlot(passiveSkillSlots, activeSkillSlots.Length);
    }
    void SkillProcessSlot(SkillSlot[] slots, int slotIndex)
    {
        for (int i = 0; i < slots.Length; i++) 
        {
            SkillSlot slot = slots[i];
            if(slot !=null && slot.skillInfo != null)
            {
                OnSkillButtonClicked(slot, slotIndex + i) ;
            }
        }
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

    public void SetSkills(SkillSlot[] skillSlots, List<BaseSkill> skillsList, SkillType skillType)
    {
        var equippedSkillList = skillType  == SkillType.Active ? SkillInventoryManager.instance.equippedActiveSkills
                                                               : SkillInventoryManager.instance.equippedPassiveSkills;

        for (int i = 0; i < skillSlots.Length; ++i)
        {
            SkillSlot slot = skillSlots[i];
            
            SkillInstance equipSkill = i < equippedSkillList.Count ? equippedSkillList[i] : null;
            // 내가 장착한 스킬이 없다면
            if (equipSkill != null)
            {
                if (equipSkill.skillInfo == null)
                {//원래 이쪽으로 들어오면 안됨
                    //Debug.LogError("Skillinfo null" + i);
                    skillsList.Add(null);
                    continue;
                }

                // 장착할 스킬이 있음
                slot.SetData(equipSkill); // 스킬 슬롯에 내가 장착한 스킬을 설정한다.
                //인게임 스킬 창은 필요없는 것들 false 처리
                slot.backGroundImage.SetActive(false);
                slot.countText.SetActive(false);
              
                GameObject skillPrefab = equipSkill.skillInfo.skillPrefab;
                if (skillPrefab == null)
                {
                    if (slot.skillInfo.type == SkillType.Active)
                    {
                        Debug.LogError($"SkillPrefab is null : {equipSkill.skillInfo.name}");
                    }
                    else
                    {
                        Debug.Log("Passive Skill");
                    }
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
            // 장착한 스킬이 있다면
            else
            {
                // 장착할 스킬해제
                slot.SetEmpty(lockedSprite);
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
    private void OnSkillButtonClicked(SkillSlot slot, int index)
    {
        if (slot.IsCooldown || slot.isMaxSkillcount)
        {
            return;
        }
        //스킬
        int skillNumber = slot.skillInfo.Number;
        if (Player.instance.isSkillHit[skillNumber] == false && slot.skillInfo.type == SkillType.Active )
        {
            return;
        }
        if (index >= 0 && index < skills.Length ) 
        {
            BaseSkill skill = skills[index];
            float cooldownSeconds = slot.skillInfo.CooldownSeconds;
            if (cooldownSeconds > 0f)
                slot.StartSkillCooldown(cooldownSeconds);

            if (skill != null)
            {
                skill.Execute();
                //Player.instance.OnUseSkill(skill);
            }
        }
    }
}