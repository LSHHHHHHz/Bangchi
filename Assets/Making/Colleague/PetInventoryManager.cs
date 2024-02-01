using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using System;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;
using Unity.VisualScripting;
using UnityEngine.Profiling.Memory.Experimental;
using Assets.HeroEditor.Common.Scripts.Common;
using System.Linq;

[Serializable] // 클래스를 json등 데이터로 저장할 때 [Serializable]을 붙여줘야 함.
public class PetInventoryData
{
    public List<PetInstance> myPets = new();
    public List<PetInstance> equipPets = new();

    public int petCount;
}
public class PetInventoryManager : MonoBehaviour
{
    public static PetInventoryManager Instance;
    public List<PetInstance> myPets = new();
    public List<PetInstance> equipPets = new();

    public int maxaccumulatePetsCount = 50;
    public int petCount;

    public event Action OnEquippedPetChanged;
    public event Action OnInventoryChanged;

    //장착 할 펫 정보
    public List<petinfoPopup> petinfoPopup = new();
    //장착 펫 정보 관리
    public List<PetInfo> petEquipInfos;
    public void Awake()
    {
        Instance = this;
        petEquipInfos = new List<PetInfo>(3);
        
        Load();
    }
    private void Start()
    {
        OnInventoryChanged += OnInventoryChangedCallback;
        PetUI.instance.SetData();
        PetUI.instance.SortSlots();
    }
    public void DestroyPetInfoPopup(petinfoPopup popupInstance)
    {
        if (petinfoPopup.Contains(popupInstance))
        {
            Destroy(popupInstance.gameObject);
            petinfoPopup.Remove(popupInstance);
        }
    }
    
    public void AddPet(PetInfo petInfo)
    {
        myPets.Add(new PetInstance()
        {
            petInfo = petInfo,
            count = 1,
            upgradeLevel = 1
        });

        OnInventoryChanged?.Invoke();
    }

    public void EquipPet(PetInfo petinfo)
    {
        PetInstance existItem = myPets.Find(item => item.petInfo == petinfo);
        if (existItem == null)
        {
            // 아이템을 가지고 있지 않다는 것
            throw new Exception($"Item not found : {petinfo.name}");
        }
        equipPets.Add(existItem);
        OnEquippedPetChanged?.Invoke();

        Save();
    }

    public void AddEquipPetInfo(PetInfo petInfo)
    {
        for (int i = 0; i < petEquipInfos.Count; i++)
        {
            if (petEquipInfos[i] == petInfo)
            {
                return;
            }
        }
        petEquipInfos.Add(petInfo);

    }
    private void OnInventoryChangedCallback()
    {
        PetUI.instance.SetData();
        PetUI.instance.SortSlots();
    }
    public void Save()
    {
        PetInventoryData petInventoryData = new PetInventoryData();
        petInventoryData.myPets = myPets;
        petInventoryData.equipPets = equipPets;
        petInventoryData.petCount = petCount;

        string json = JsonUtility.ToJson(petInventoryData);

        PlayerPrefs.SetString("PetInventoryData", json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        string json = PlayerPrefs.GetString("PetInventoryData");
        if (string.IsNullOrEmpty(json) == false)
        {
            PetInventoryData data = JsonUtility.FromJson<PetInventoryData>(json);
            for (int i = 0; i < data.myPets.Count; i++)
            {
                PetInstance pet = data.myPets[i];
                if (pet.petInfo == null)
                {
                    continue;
                }
                myPets.Add(pet);
            }
            for (int i = 0; i < data.equipPets.Count; i++)
            {
                PetInstance equippet = data.equipPets[i];
                if (equippet.petInfo == null)
                {
                    continue;
                }
                equipPets.Add(equippet);
            }
            petCount = data.petCount;

        }
    }
}

