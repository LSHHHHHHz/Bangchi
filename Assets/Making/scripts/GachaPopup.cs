﻿using Assets.HeroEditor.InventorySystem.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Item1;
using Unity.VisualScripting;

public class GachaPopup : MonoBehaviour //가차 결과를 보여주는 UI
{
    public GridLayoutGroup grid;
    public GameObject itemPrefab;

    private List<GameObject> children = new List<GameObject>();

    private Action<int> oneMoreTimeAction; // oneMoreTime 전달받은 값을 저장하기 위해 따로 멤버 필드로 가지고 있음.
    private Action onDoneAction;


    private bool isCoroutineDone = true;

    public float fadeTime = 1f;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;

    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, fadeTime);
    }

    public void Initialize(GachaResult gachaResult, Action<int> oneMoreTime, Action onDone) //Action onDone 연출(코루틴)이 끝났을 때 호출되는 함수
    {
        // 이전에 보관해뒀던 아이템 UI들을 파괴
        foreach (GameObject child in children)
        {
            Destroy(child); //객체 파괴
        }
        children.Clear(); // 리스트 비우는것

        // 나중에 다시 뽑기 버튼 누르면 호출하기 위해 클래스의 멤버 필드인 oneMoreTimeAction에 값을 저장한다.
        this.oneMoreTimeAction = oneMoreTime;
        this.onDoneAction = onDone;

        isCoroutineDone = false;
        StartCoroutine(SetupCoroutine(gachaResult));
        isCoroutineDone = true;
    }
    private IEnumerator SetupCoroutine(GachaResult gachaResult)
    {
        yield return transform.DOLocalMoveX(0, 2f).Play().WaitForCompletion();

        for (int i = 0; i < gachaResult.items.Count; ++i)
        {
            ItemInfo itemInfo = gachaResult.items[i];
            bool isHigeGrade = (int)itemInfo.grade >= (int)ItemGrade.C;
            
            // 아이템 슬롯 생성
            ItemSlot itemSlot = Instantiate(itemPrefab, grid.transform).GetComponent<ItemSlot>();

            // 아이템 슬롯에 뽑은 아이템 데이터 적용
            itemSlot.SetData(gachaResult.items[i]);

            var sequence = DOTween.Sequence();
            itemSlot.transform.localScale = Vector3.one * 3;
            
            if (isHigeGrade)
            {
                sequence.Append(itemSlot.transform.DOScale(1.5f, 0.2f).SetLoops(4));
                sequence.Append(itemSlot.transform.DOScale(1f, 0.2f));
            }

            sequence.Play();

            // 아이템 프리팹이 원래 Active:false였으니 이것도 false인 상태. true로 바꿔서 보이게 한다.
            itemSlot.gameObject.SetActive(true);

            // 나중에 삭제해야되니까 children에 넣어서 관리
            children.Add(itemSlot.gameObject);
            yield return itemSlot.transform.DOScale(Vector3.one, 0.1f).WaitForCompletion();

            yield return sequence.WaitForCompletion();

        }
        isCoroutineDone = true;
        onDoneAction?.Invoke();
    }
    public void Close()
    {
        StopAllCoroutines(); // 돌아가고 있는 코루틴이 있다면 멈춘다.
        if (isCoroutineDone == false)
        {
            isCoroutineDone = true;
            onDoneAction?.Invoke();
        }

        Destroy(gameObject);
    }
    public void OneMoreTime1()
    {
        oneMoreTimeAction?.Invoke(1);
    }
    public void OneMoreTime11()
    {
        oneMoreTimeAction?.Invoke(11);
    }
}
