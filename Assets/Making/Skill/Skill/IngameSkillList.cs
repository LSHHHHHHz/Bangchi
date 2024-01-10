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

    public event Action setempty1;

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

    private bool isPassiveOn = false;

    private void Awake()
    {
        instance = this;
        // 슬롯을 얻어오는 과정.
        // activeSkillSlotParent, passiveSkillSlotParent를 가지고 슬롯을 얻어옴
        // Parent의 자식들을 순회하며 SkillSlot을 가져오고 Button 클릭시 처리도 연결
        activeSkillSlots = GetChildSlots(activeSkillSlotParent);
        int skillIndex = 0;
        for (int i = 0; i < activeSkillSlots.Length; ++i)
        {
            SkillSlot activeSkillSlot = activeSkillSlots[i];
            var button = activeSkillSlot.GetComponent<Button>();
            int index = skillIndex;
            button.onClick.AddListener(() => OnSkillButtonClicked(activeSkillSlot, index));
            ++skillIndex;
        }

        passiveSkillSlots = GetChildSlots(passiveSkillSlotParent);
        for (int i = 0; i < passiveSkillSlots.Length; ++i)
        {
            SkillSlot passiveSkillSlot = passiveSkillSlots[i];
            var button = passiveSkillSlot.GetComponent<Button>();
            int index = skillIndex;
            button.onClick.AddListener(() => OnSkillButtonClicked(passiveSkillSlot, index));
            ++skillIndex;
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
        // 엑티브 스킬 처리
        for (int i = 0; i < activeSkillSlots.Length; i++)
        {
            SkillSlot slot = activeSkillSlots[i];
            if (slot != null && slot.skillInfo != null)
            {
                OnSkillButtonClicked(slot, i);
            }
        }

        for (int i = 0; i < passiveSkillSlots.Length; i++)
        {
            SkillSlot slot = passiveSkillSlots[i];
            if (slot != null && slot.skillInfo != null)
            {
                OnSkillButtonClicked(slot, i + activeSkillSlots.Length);
            }
        }
    }

    public void test()
    {//패시브만
        for (int i = 0; i < passiveSkillSlots.Length; i++)
        {
            int passcount = 4;

            SkillSlot slot = passiveSkillSlots[i];
            if (slot != null)
            {
                OnSkillButtonClicked(slot, passcount);
            }
            passcount++;
            if (passcount == 7)
            {
                passcount = 4;
            }
        }
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
                    Debug.LogError("Skillinfo null" + i);
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
                //★★★
                /*for(int p = 0; p<8; p++)
                {
                    if(slot.skillInfo.Number == p && slot.skillInfo.type == SkillType.Passive)
                    {
                        setempty1?.Invoke();
                    }
                }

                if(slot.skillInfo.Number==1)
                {
                    setempty1?.Invoke();
                }*/


                //이부분 저장이 안됨 껏다 켰을 때

                // 장착할 스킬해제
                slot.SetEmpty(lockedSprite);
                //slot.icon.sprite = lockedSprite;
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
        if (slot.IsCooldown)
            return;

        if (index >= 0 && index < skills.Length) //length가 잘못도미
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