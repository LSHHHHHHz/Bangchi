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
   

    private void Awake()
    {
        slot = GetComponent<PetSlot>();
        
    }

    //버튼을 누르면 닫힘 -> 선택하는 창 보여지게 함
    //펫 장착은 펫슬롯으로 하게 되면 안됨
    //새로운 스크립트 만든 후 동행을 눌렀을 때 데이터를 저장하고 버튼을 누르면 그 데이터가 들어가면서 기존 데이터 삭제
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
