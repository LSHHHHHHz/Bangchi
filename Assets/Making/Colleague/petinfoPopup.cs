using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petinfoPopup : MonoBehaviour
{
    List<PetInfo> petinfoList = new List<PetInfo>();

    public GameObject petinfo;
    public RectTransform transparencyimage;
    PetSlot slot;

    public Text petinfoAttack;
    public Text petinfoHP;
    public Text petinfoExp;
    public Text petinfoCoin;

    private void Awake()
    {
        slot = GetComponent<PetSlot>();
    }
    private void Update()
    {
        Petinfos();
    }

    public void Petinfos()
    {
        petinfoAttack.text = "추가 공격력 +" + slot.petInfo.petAttack.ToString();
        petinfoHP.text = "추가 체력 +" + slot.petInfo.petHP.ToString();
        petinfoExp.text = "추가 경험치 +"+ slot.petInfo.petExp.ToString();
        petinfoCoin.text = "추가 코인 +" + slot.petInfo.petCoin.ToString();
    }

    public void petinfoActiveFalse()
    {
        petinfo.SetActive(false);
        transparencyimage.SetActive(true);
        slot.icon.SetActive(false);
    }

    public void PetInfoClose()
    {
        Destroy(this.gameObject);

        PetInventoryManager.Instance.equipPets.Clear();
        PetInventoryManager.Instance.petinfoPopup.Clear();
    }

}
