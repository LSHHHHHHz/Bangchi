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

    public List<GameObject> inventoryChildren = new List<GameObject>();

    public int petCount;
}
public class PetInventoryManager : MonoBehaviour
{
    public static PetInventoryManager Instance;
    public List<PetInstance> myPets = new();
    public List<PetInstance> equipPets = new();

    public int maxaccumulatePetsCount = 50;
    public int petCount;

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
        Load();
    }
    private void Start()
    {
        OnInventoryChanged += OnInventoryChangedCallback;
        
        SetData();
        SortSlots();
        maxaccumulatePetsCount += petCount; //펫 확장

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
            if (slot.petInfo != null)
            {
                petSlotsList.Add(slot);
            }
        }

        if (isActive)
        {
            // number만 가지고 정렬
            //petSlotsList.Sort((x, y) => x.petInfo.Number.CompareTo(y.petInfo.Number));
            //petSlotsList.Sort((x, y) => x.petInfo.petgrade.CompareTo(y.petInfo.petgrade));
            //petSlotsList.Sort((x, y) =>
            //{
            //    int petTypeCompare = x.petInfo.petType.CompareTo(y.petInfo.petType);
            //    if (petTypeCompare != 0)
            //        return petTypeCompare;

            //    return x.petInfo.Number.CompareTo(y.petInfo.Number);
            //});

            var strings = new List<string>() { "abvasd", "dwwqeq2333", "cvvxv" };
            strings.Sort((x, y) => x.Length.CompareTo(y.Length));


            petSlotsList.Sort((x, y) =>
            {
                int petNumberCompare = y.petInfo.Number.CompareTo(x.petInfo.Number);
                if(petNumberCompare!= 0) return petNumberCompare;

                return x.petInfo.petType.CompareTo(y.petInfo.petType);
            });


            for (int i = 0; i < petSlotsList.Count; ++i)
            {
                PetSlot slot = petSlotsList[i];
                slot.transform.SetSiblingIndex(i);
            }
            // grade로 먼저 정렬, 같은 grade라면 number로 정렬한다.

            //List<PetSlot> num1 = sortNumber(petSlotsList, 1);
            //List<PetSlot> num2 = sortNumber(petSlotsList, 2);
            //List<PetSlot> num3 = sortNumber(petSlotsList, 3);
            //List<PetSlot> num4 = sortNumber(petSlotsList, 4);

            //num1.Sort((x, y) => ((int)x.petInfo.petType).CompareTo((int)y.petInfo.petType));
            //num2.Sort((x, y) => ((int)x.petInfo.petType).CompareTo((int)y.petInfo.petType));
            //num3.Sort((x, y) => ((int)x.petInfo.petType).CompareTo((int)y.petInfo.petType));
            //num4.Sort((x, y) => ((int)x.petInfo.petType).CompareTo((int)y.petInfo.petType));


            //for (int i = 0; i < num1.Count; ++i)
            //{
            //    PetSlot slot = num1[i];
            //    slot.transform.SetSiblingIndex(i);
            //}
            //for (int i = 0; i < num2.Count; ++i)
            //{
            //    PetSlot slot = num2[i];
            //    slot.transform.SetSiblingIndex(i);
            //}
            //for (int i = 0; i < num3.Count; ++i)
            //{
            //    PetSlot slot = num3[i];
            //    slot.transform.SetSiblingIndex(i);
            //}
            //for (int i = 0; i < num4.Count; ++i)
            //{
            //    PetSlot slot = num4[i];
            //    slot.transform.SetSiblingIndex(i);
            //}

            //petSlots = petSlotsList.ToArray();
            /*for (int i = 0; i < petSlotsList.Count; ++i)
            {
                PetSlot slot = petSlotsList[i];
                slot.transform.SetSiblingIndex(i);
            }*/
        }
    }


    private List<PetSlot> sortNumber(List<PetSlot> petinfo, int number)
    {
        var list = new List<PetSlot>();

        foreach(var pet in petinfo)
        {
            if(pet.petInfo.Number ==number)
            {
                list.Add(pet);
            }
        }

        return list;
    }

    public void SetData()
    {
        int slotCount = petSlots.Length; // 

        for (int i = 0; i < myPets.Count; i++)
        {
            if (i >= slotCount) // i가 petSlots의 범위를 초과하는지 확인
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

