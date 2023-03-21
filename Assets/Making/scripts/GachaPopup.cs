using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;

public class GachaPopup : MonoBehaviour
{
    public GridLayoutGroup grid;
    public GameObject itemPrefab;

    List<GameObject> children = new List<GameObject>();
    Action oneMoreTime;

    public void Initialize(GachaResult gachaResult, Action oneMoreTime)
    {
        // 이전에 보관해뒀던 아이템 UI들을 파괴
        foreach (GameObject child in children)
        {
            Destroy(child);
        }
        children.Clear();

        this.oneMoreTime = oneMoreTime;
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
    }
        public void Close()
        {
            Destroy(gameObject);
        }

        public void OneMoreTime()
        {
            //Invoke("Close", 3f); <-- 이거랑 전혀 연관없음. 이건 유니티의 함수

            // Action.Invoke() : Action을 호출한다.
            // oneMoreTime();  <-- oneMoreTime이 null이면 에러가 발생.
            // oneMoreTime.Invoke() <-- oneMoreTime이 null이면 에러가 발생.
            // oneMoreTime?.Invoke() <-- 객체에 접근할 때 '?'를 붙이면 객체가 null일 때 접근하지 않고 무시.
            oneMoreTime?.Invoke(); //이것도.. Invoke를 찍었는데 Action이 검색됨
        }
    
}
