using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 내 장비들을 보여주는 UI.
public class Equips : MonoBehaviour
{
    public RectTransform uiGroup;
    public RectTransform weaponselectuiGroup;
    public RectTransform talkText;
    public Player enterPlayer;

    public RectTransform weaponSlotParent;

  
    public void EnterEquip()
    {
        uiGroup.anchoredPosition = new Vector3(0, 0, 0);
        Debug.Log("클릭");
    }

    public void ExitEquip()
    {
        uiGroup.anchoredPosition = new Vector3(2049, -1607, 0);
    }

    public void EnterWeaponSelect()
    {
        weaponselectuiGroup.anchoredPosition = new Vector3(0, 625, 0);
    }

    public void ExitWeaponSelect()
    {
        weaponselectuiGroup.anchoredPosition = new Vector3(-1687, 625, 0);
    }

    // 인벤토리에 있는 아이템들을 UI에 설정하는 기능.
    
}
