using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Item1;
using System;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;

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
    public List<GameObject> petchildren = new List<GameObject>();
    
    public GridLayoutGroup grid;
    public void Awake()
    {
        Instance = this; 
    }
 
    public void AddInventoryPets()
    {
        foreach (GameObject child in petchildren)
        {
            var inventorypet = Instantiate(child, grid.transform);
            var imageComponent = inventorypet.GetComponent<Image>();
            imageComponent.enabled = true; //이걸 넣어야만 이미지가 보임 foreach (GameObject child in children)
            
        }
    }

    public void AddPet(PetInfo petInfo)
    {
        PetInstance existPet = myPets.Find(myPet => myPet.petInfo == petInfo);

        if(existPet != null)
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

