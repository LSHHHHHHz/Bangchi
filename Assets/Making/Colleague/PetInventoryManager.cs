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
    public List<PetInstance> accumulatePets = new();

    public int maxaccumulatePetsCount = 50;

    
    public event Action OnInventoryChanged;
    public RectTransform PetSlotParent;
    PetSlot[] petSlots;
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
        SetData();
    }

    public void SetData()
    {
        if (accumulatePets.Count < maxaccumulatePetsCount+1)
        {
            for (int i = 0; i < accumulatePets.Count; i++)
            {
                PetSlot slot = petSlots[i];
                slot.SetData(accumulatePets[i]);
                slot.SetActive(true);
            }
        }
        else
        {
            Debug.Log("확장 하세요");
        }
    }

    //myPet에 16개밖에 없네 이걸 무제한으로 만들어야함

    public void accumulatePet(PetInfo petInfo)
    {
        accumulatePets.Add(new PetInstance()
        {
            petInfo = petInfo,
            count = 1,
            upgradeLevel = 1
        });

        OnInventoryChanged?.Invoke();
    }

    public void AddPet(PetInfo petInfo)
    {
        PetInstance existPet = myPets.Find(myPet => myPet.petInfo == petInfo);

        if (existPet != null)
        {
            existPet.count++;
        }
        else
        {
            myPets.Add(new PetInstance()
            {
                petInfo = petInfo,
                count = 1,
                upgradeLevel = 1
            });
        }
        OnInventoryChanged?.Invoke();
       // SetData(); //★여기에 넣어야 되는데 이유 알기
        Save();
    }
    private void OnInventoryChangedCallback()
    {
        SetData();
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

