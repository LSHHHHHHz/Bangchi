using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using System;

[Serializable] // 클래스를 json등 데이터로 저장할 때 [Serializable]을 붙여줘야 함.
public class PetInventoryData
{
    public List<PetInstance> myPets = new();
    public List<PetInstance> equipPets = new();
}
public class PetInventoryManager : MonoBehaviour
{
    public static PetInventoryManager Instance;
    public List <PetInstance> myPets = new();
    public List <PetInstance> equipPets = new();

    public void Awake()
    {
        Instance = this; 
    }

    public void AddPet(PetInfo petInfo)
    {
        PetInstance exitPet = myPets.Find(myPet => myPet.petInfo == petInfo);

        if(exitPet != null)
        {
            exitPet.count++;
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
        Save();
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
            for(int i=0; i<data.myPets.Count; i++)
            {
                PetInstance pet = data.myPets[i];
                if(pet.petInfo == null)
                {
                    continue;
                }
                myPets.Add(pet);
            }
            for(int i=0; i<data.equipPets.Count; i++)
            {
                PetInstance equippet = data.equipPets[i];
                if(equippet.petInfo == null)
                {
                    continue;
                }
                equipPets.Add(equippet);
            }
        }

    }
    
}

