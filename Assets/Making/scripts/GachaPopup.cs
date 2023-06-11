using Assets.HeroEditor.InventorySystem.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;

public class GachaPopup : MonoBehaviour //가차 결과를 보여주는 UI
{
    public Action runGacha1Action;
    public Action runGacha11Action;


    public GridLayoutGroup grid;
    public GameObject itemPrefab;

    private List<GameObject> children = new List<GameObject>();

    private Action<int> oneMoreTimeAction; // oneMoreTime 전달받은 값을 저장하기 위해 따로 멤버 필드로 가지고 있음.

    public void Initialize(GachaResult gachaResult, Action<int> oneMoreTime)
    {
        // 이전에 보관해뒀던 아이템 UI들을 파괴
        foreach (GameObject child in children)
        {
            Destroy(child); //객체 파괴
        }
        children.Clear(); // 리스트 비우는것

        // 나중에 다시 뽑기 버튼 누르면 호출하기 위해 클래스의 멤버 필드인 oneMoreTimeAction에 값을 저장한다.
        this.oneMoreTimeAction = oneMoreTime;
        for (int i = 0; i < gachaResult.items.Count; ++i)
        {
            // 아이템 슬롯 생성
            ItemSlot itemSlot = Instantiate(itemPrefab, grid.transform).GetComponent<ItemSlot>();

            // 아이템 슬롯에 뽑은 아이템 데이터 적용
            itemSlot.SetData(gachaResult.items[i]);

            // 아이템 프리팹이 원래 Active:false였으니 이것도 false인 상태. true로 바꿔서 보이게 한다.
            itemSlot.gameObject.SetActive(true);

            // 나중에 삭제해야되니까 children에 넣어서 관리
            children.Add(itemSlot.gameObject);
        }

        //방패
        for (int i = 0; i < gachaResult.itemsSH.Count; ++i)
        {
            ItemSlot itemSlot = Instantiate(itemPrefab, grid.transform).GetComponent<ItemSlot>();

            itemSlot.SetData(gachaResult.itemsSH[i]);

            itemSlot.gameObject.SetActive(true);
            children.Add(itemSlot.gameObject);
        }
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    // 다시 뽑기 버튼 누르면 호출됨
    public void OneMoreTime1()
    {
        oneMoreTimeAction?.Invoke(1);
    }

    public void OneMoreTime11()
    {
        oneMoreTimeAction?.Invoke(11);
    }

}
