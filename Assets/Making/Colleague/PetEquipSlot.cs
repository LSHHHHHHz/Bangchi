using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;


public class PetEquipSlot : MonoBehaviour
{
    public PetInfo petinfo;
    public Image icon;
    public Image effecticon;
    public int Num;
    public GridLayoutGroup grid;
    public List<PetEquipSlot> pets;
   
    private void Awake()
    {
        pets = new List<PetEquipSlot>(3);
        for(int i = 0; i< grid.transform.childCount; i++)
        {
            var petinfo = grid.transform.GetChild(i).GetComponent<PetEquipSlot>();
            pets.Add(petinfo);
            if (petinfo == null)
            {
                pets.Add(null);
            }

        }
    
    }
    private void Start()
    {
      
    }
    //펫이 장착되어 있을 경우
    //펫이 장착되어 있지 않을 경우

    //0번이면 1번 2번이랑
    public void isEquipOrUnEquip()
    {
        if (PetInventoryManager.Instance.equipPets.Count == 0)
        {
            return;
        }
        if (Num == 0)
        {
            if (pets[1].petinfo == PetInventoryManager.Instance.equipPets[0].petInfo || pets[2].petinfo == PetInventoryManager.Instance.equipPets[0].petInfo)
            {
                popupClose();
                return;
            }
        }
        if (Num == 1)
        {
            if (pets[0].petinfo == PetInventoryManager.Instance.equipPets[0].petInfo || pets[2].petinfo == PetInventoryManager.Instance.equipPets[0].petInfo)
            {
                popupClose();
                return;
            }
        }
        if (Num == 2)
        {
            if (pets[1].petinfo == PetInventoryManager.Instance.equipPets[0].petInfo || pets[0].petinfo == PetInventoryManager.Instance.equipPets[0].petInfo)
            {
                popupClose();
                return;
            }
        }
        if ((petinfo == null && PetInventoryManager.Instance.equipPets != null)||(petinfo != null && PetInventoryManager.Instance.equipPets != null))
        {
            equipPet();
        }
        else if(petinfo != null)
        {
            SetEmpty(petinfo);
        }
    }

    public void popupClose()
    {
        if (PetInventoryManager.Instance.petinfoPopup.Count > 0)
        {
            petinfoPopup Popup = PetInventoryManager.Instance.petinfoPopup[0];
            PetInventoryManager.Instance.DestroyPetInfoPopup(Popup);
          
        }
    }
    public void equipPet()
    {
        this.petinfo = PetInventoryManager.Instance.equipPets[0].petInfo;
        SetData(petinfo);
        PetInventoryManager.Instance.equipPets.Clear();

        //어짜피 순서에 상관 없으니 First 사용해서 같은 것이 있는지 확인
        //장착했을 때 없어지면 안됨 나중에 합성할 때 참고
       /* var petSlotReset = PetInventoryManager.Instance.petSlots.First(slot => slot.petInfo == petinfo);
        var petInstance = PetInventoryManager.Instance.myPets.First(s => s.petInfo == petinfo);
        if (petSlotReset != null)
        {
            petSlotReset.ResetData();
            PetInventoryManager.Instance.myPets.Remove(petInstance);
            PetInventoryManager.Instance.SortSlots();
        }*/


        //PetInfoPopup 파괴
        if (PetInventoryManager.Instance.petinfoPopup.Count > 0)
        {
            petinfoPopup Popup = PetInventoryManager.Instance.petinfoPopup[0];
            PetInventoryManager.Instance.DestroyPetInfoPopup(Popup);
        }

    }
    public void SetData(PetInfo petInfo)
    {
        this.petinfo = petInfo;
        icon.sprite = Resources.Load<Sprite>(petInfo.iconPath);
    }
    public void SetEmpty(PetInfo petInfo)
    {
        this.petinfo = null;
        icon.sprite = null;
    }
}