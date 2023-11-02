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

[Serializable] // 클래스를 json등 데이터로 저장할 때 [Serializable]을 붙여줘야 함.
public class PetInventoryData
{
    public List<PetInstance> myPets = new();
    public List<PetInstance> equipPets = new();

    public List<GameObject> inventoryChildren = new List<GameObject>();
}
public class PetInventoryManager : MonoBehaviour
{
    public static PetInventoryManager Instance;
    public List<PetInstance> myPets = new();
    public List<PetInstance> equipPets = new();

    public int maxaccumulatePetsCount = 50;

    
    public event Action OnInventoryChanged;
    public RectTransform PetSlotParent;
    PetSlot[] petSlots;
    List<PetSlot> petSlotsList = new();
    public void Awake()
    {
        Instance = this;

        List<PetSlot> childList = new();
        for (int i = 0; i < PetSlotParent.childCount; i++)
        {
            PetSlot child = PetSlotParent.GetChild(i).GetComponent<PetSlot>();
            childList.Add(child);
        }
        petSlots = childList.ToArray();
    }
    private void Start()
    {
        OnInventoryChanged += OnInventoryChangedCallback;
        Load();
        SetData();
        SortSlots();
       
    }
    private void Update()
    {
       
    }
    private void SortSlots()
    {
        bool isActive = Array.Exists(petSlots, x => x.petInfo != null);
        petSlotsList.Clear();
        foreach (PetSlot slot in petSlots)
        {
            if(slot.petInfo != null)
            {
                petSlotsList.Add(slot);
            }
        }

        if (isActive)
        {
            petSlotsList.Sort((x,y)=> x.petInfo.Number.CompareTo(y.petInfo.Number));
            //petSlots = petSlotsList.ToArray();
            for (int i = 0; i < petSlotsList.Count; ++i)
            {
                PetSlot slot = petSlotsList[i];
                slot.transform.SetSiblingIndex(i);
            }
        }
    }

    public void SetData()
    {
            /*for (int i = 0; i < accumulatePets.Count; i++)
            {
                PetSlot slot = petSlots[i];
                slot.SetData(accumulatePets[i]);
                slot.SetActive(true);
            }*/ //수정 전


        int slotCount = petSlots.Length; // 

        for (int i = 0; i < myPets.Count; i++)
        {
            if (i >= slotCount) // 이 조건을 추가하여 i가 petSlots의 범위를 초과하는지 확인
            {
                Debug.LogWarning("확장필요");
                break; 
            }

            PetSlot slot = petSlots[i];
            slot.SetData(myPets[i]);
            slot.SetActive(true);
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


    private void OnInventoryChangedCallback()
    {
        SetData();
        SortSlots();
    }
    public void Save()
    {
        PetInventoryData petInventoryData = new PetInventoryData();
        petInventoryData.myPets = myPets;
        petInventoryData.equipPets = equipPets;

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
        }
    }
}

