using Assets.Battle;
using Assets.HeroEditor.Common.Scripts.Common;
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
    private Image myImage;
    private DropItem dropitem;


    public int originAttack = 0;
    public int originHp = 0;
    public int originplusExp = 0;
    public int originplusCoin = 0;
    private void Awake()
    {
        dropitem = FindObjectOfType<DropItem>();

        myImage = GetComponent<Image>();
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
    public void isEquipOrUnEquip()
    {
        var equippedPetInfo = PetInventoryManager.Instance.equipPets.FirstOrDefault()?.petInfo;
        if (equippedPetInfo == null || pets.Any(p => p.petinfo == equippedPetInfo))
        {
            popupClose();
            return;
        }
        else
        {
            equipPet();
        }
    }

    public void popupClose()
    {
        if (PetInventoryManager.Instance.petinfoPopup.Count > 0)
        {
            petinfoPopup Popup = PetInventoryManager.Instance.petinfoPopup[0];
            PetInventoryManager.Instance.DestroyPetInfoPopup(Popup);
          
        }
        PetInventoryManager.Instance.equipPets.Clear();
    }
    public void equipPet()
    {
        this.petinfo = PetInventoryManager.Instance.equipPets[0].petInfo;
        SetData(petinfo);
        PetInventoryManager.Instance.AddEquipPetInfo(petinfo);

        for (int i = 0; i < PetInventoryManager.Instance.petEquipInfos.Count; i++)
        {
            if (petinfo.petType == PetInventoryManager.Instance.petEquipInfos[i].petType && petinfo.petgrade == PetInventoryManager.Instance.petEquipInfos[i].petgrade )
            {
                Player.instance.Current_Attack += PetInventoryManager.Instance.petEquipInfos[i].petAttack - originAttack;
                Player.instance.Max_HP += PetInventoryManager.Instance.petEquipInfos[i].petHP - originHp;
                Player.instance.AddExp += PetInventoryManager.Instance.petEquipInfos[i].petExp - originplusExp;
                Player.instance.AddCoin += PetInventoryManager.Instance.petEquipInfos[i].petCoin - originplusCoin;

                originAttack = PetInventoryManager.Instance.petEquipInfos[i].petAttack;
                originHp = PetInventoryManager.Instance.petEquipInfos[i].petHP;
                originplusExp = PetInventoryManager.Instance.petEquipInfos[i].petExp;
                originplusCoin = PetInventoryManager.Instance.petEquipInfos[i].petCoin;
               
            }
        }
        PetInventoryManager.Instance.equipPets.Clear();

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
        myImage.color = new Color(1, 1, 1, 1);
    }
    public void SetEmpty()
    {
        this.petinfo = null;
        icon.sprite = null;
    }
}