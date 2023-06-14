using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using System;

[Serializable] // Ŭ������ json�� �����ͷ� ������ �� [Serializable]�� �ٿ���� ��.
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
    public List<SkillInstance> equippedSkills = new(); //���� ��ų ����Ʈ
    public const int MaxEquipCount = 8;

    public void Awake()
    {
        instance = this;
        Load();
    }


    public void AddSkill(SkillInfo skillInfo)//�����ʿ�................................
    {
        SkillInstance existItem = myItems.Find(item => item.skillInfo == skillInfo); //�����ʿ�..................
        // �����.ã�´�(�� ��� => �� ���.�̸� == "ȫ�浿"
        // myItems ����Ʈ�� ����ִ� �͵� �� item�� skillInfo�� �Ű����� skillInfo�� ������ ã�� �� SkillInstance Ÿ���� existItem������ �ִ´�


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
            }); // �̰� ����..?
        }

        OnSkillInventoryChanged?.Invoke();
    }

    //item�� skillInfo�� skillInfo�� ���ٸ�  SkillInstance Ÿ���� existItem�� ����
    //existItem�� ������� �ʴٸ� count�� �ø���.
    //�׷��� �ʴٸ� 


    public void EquipSkill(SkillInfo skillInfo)
    {
        SkillInstance existItem = myItems.Find(item => item.skillInfo == skillInfo); //
        if (existItem == null)
        {
            // ��ų�� ������ ���� �ʴٴ� ��!
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
            // ������ ���ߴٴ� ���
            return;
        }

        equippedSkills.Remove(existItem);
        OnEquippedSkillsChanged?.Invoke();

        Save();
    }

    // ������ ������ ��, ������ ȹ��� �������ָ� ��
    public void Save()
    {
        var skillInventoryData = new SkillInventoryData();
        skillInventoryData.myItems = myItems;
        skillInventoryData.equippedSkills = equippedSkills;

        string json = JsonUtility.ToJson(skillInventoryData);

        // PlayerPrefs : �����͸� �����ϰ� �ҷ����µ� ���� Ŭ����
        PlayerPrefs.SetString("SkillInventoryData", json);
        PlayerPrefs.Save();
    }

    // ������ ó���� ���� �� �� �����۵� �ҷ�����
    private void Load()
    {
        string json = PlayerPrefs.GetString("SkillInventoryData");
        // ������ ó���� ���� ����� �����Ͱ� ���� �� ������
        // ���ڿ��� ������� ���� ������ ó��
        if (string.IsNullOrEmpty(json) == false)
        {
            // json�� ���� ����ִٴ� ��
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
