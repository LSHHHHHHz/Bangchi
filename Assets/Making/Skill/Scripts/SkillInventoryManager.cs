using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using System;

[Serializable] // 클래스를 json등 데이터로 저장할 때 [Serializable]을 붙여줘야 함.
public class SkillInventoryData 
{
    public List<SkillInstance> myItems = new();
    public List<SkillInstance> equippedSkills = new();
}
public class SkillInventoryManager : MonoBehaviour
{
    public event Action OnSkillInventoryChanged;
    public event Action OnEquippedSkillsChanged;

    public static SkillInventoryManager instance;
    public List<SkillInstance> myItems = new();
    public List<SkillInstance> equippedSkills = new(); //장착 스킬 리스트
    public const int MaxEquipCount = 8;

    public void Awake()
    {
        instance = this;
        Load();
    }


    public void AddSkill(SkillInfo skillInfo)//설명필요................................
    {
        SkillInstance existItem = myItems.Find(item => item.skillInfo == skillInfo); //설명필요..................
        // 사람들.찾는다(이 사람 => 이 사람.이름 == "홍길동"
        // myItems 리스트에 들어있는 것들 중 item의 skillInfo와 매개변수 skillInfo와 같은지 찾은 후 SkillInstance 타입의 existItem변수에 넣는다


        if (existItem != null)
        {
            existItem.count++;
        }
        else
        {
            myItems.Add(new SkillInstance()
            {
                skillInfo = skillInfo,
                count = 1,
                upgradeLevel = 1
            }); // 이건 뭐지..?
        }

        OnSkillInventoryChanged?.Invoke();
    }

    //item의 skillInfo가 skillInfo와 같다면  SkillInstance 타입의 existItem에 넣음
    //existItem이 비어있지 않다면 count를 늘린다.
    //그렇지 않다면 


    public void EquipSkill(SkillInfo skillInfo)
    {
        SkillInstance existItem = myItems.Find(item => item.skillInfo == skillInfo); //
        if (existItem == null)
        {
            // 스킬을 가지고 있지 않다는 것!
            throw new Exception($"Skill not found : {skillInfo.name}");
        }

        if (equippedSkills.Count == MaxEquipCount)
        {
            throw new Exception($"Max Equip count reached!");
        }

        equippedSkills.Add(existItem);
        OnEquippedSkillsChanged?.Invoke();

        Save();
    }

    public void UnEquipSkill(SkillInfo skillInfo)
    {
        SkillInstance existItem = equippedSkills.Find(item => item.skillInfo == skillInfo);
        if (existItem == null)
        {
            // 장착을 안했다는 얘기
            return;
        }

        equippedSkills.Remove(existItem);
        OnEquippedSkillsChanged?.Invoke();

        Save();
    }

    // 게임을 저장할 때, 아이템 획득시 저장해주면 됨
    public void Save()
    {
        var skillInventoryData = new SkillInventoryData();
        skillInventoryData.myItems = myItems;
        skillInventoryData.equippedSkills = equippedSkills;

        string json = JsonUtility.ToJson(skillInventoryData);

        // PlayerPrefs : 데이터를 저장하고 불러오는데 쓰는 클래스
        PlayerPrefs.SetString("SkillInventoryData", json);
        PlayerPrefs.Save();
    }

    // 게임을 처음에 켰을 때 내 아이템들 불러오기
    private void Load()
    {
        string json = PlayerPrefs.GetString("SkillInventoryData");
        // 게임을 처음할 때는 저장된 데이터가 없을 수 있으니
        // 문자열이 비어있지 않을 때에만 처리
        if (string.IsNullOrEmpty(json) == false)
        {
            // json이 값이 들어있다는 뜻
            var skillInventoryData = JsonUtility.FromJson< SkillInventoryData>(json);
            for (int i = 0; i < skillInventoryData.myItems.Count; ++i)
            {
                var item = skillInventoryData.myItems[i];
                if (item.skillInfo == null)
                    continue;

                myItems.Add(item);
            }

            for (int i = 0; i < skillInventoryData.equippedSkills.Count; ++i)
            {
                var item = skillInventoryData.equippedSkills[i];
                if (item.skillInfo == null)
                    continue;

                equippedSkills.Add(item);
            }

            //myItems = inventoryData.myItems;
        }

        OnEquippedSkillsChanged?.Invoke();
    }
}
