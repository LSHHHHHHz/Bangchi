using Assets.Item1;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGachaPopup : MonoBehaviour
{
    public Action runGacha1Action;
    public Action runGacha11Action;


    public GameObject gachaEffectPrefab;
    public GridLayoutGroup grid;
    public GameObject itemPrefab;

    private List<GameObject> children = new List<GameObject>();

    private Action<int> oneMoreTimeAction; // oneMoreTime 전달받은 값을 저장하기 위해 따로 멤버 필드로 가지고 있음.
    private bool isCoroutineDone = true;


    public void Initialize(SkillGachaResult skillgachaResult, Action<int> oneMoreTime)
    {
        transform.position = new Vector3(360,-500 ,0);
        
        // 이전에 보관해뒀던 아이템 UI들을 파괴
        foreach (GameObject child in children)
        {
            Destroy(child); //객체 파괴
        }
        children.Clear(); // 리스트 비움

        this.oneMoreTimeAction = oneMoreTime;

        isCoroutineDone = false;
        StartCoroutine(SetupCoroutine(skillgachaResult));
    }

    private IEnumerator SetupCoroutine(SkillGachaResult skillgachaResult)
    {
        yield return transform.DOLocalMoveY(0, 2f).Play().WaitForCompletion();
        
        for (int i = 0; i < skillgachaResult.items.Count; ++i)
        {
            SkillInfo skillInfo = skillgachaResult.items[i];
            bool isDelayShowing = (int)skillInfo.grade >= (int)SkillGrade.B;

            // 아이템 슬롯 생성
            SkillSlot skillitemSlot = Instantiate(itemPrefab, grid.transform).GetComponent<SkillSlot>();
            // 아이템 슬롯에 뽑은 아이템 데이터 적용
            skillitemSlot.SetData(skillInfo);
            

            var sequence = DOTween.Sequence();
            skillitemSlot.transform.localScale = Vector3.one * 3f;
            sequence.Append(skillitemSlot.transform.DOScale(1, 0.5f).SetEase(Ease.OutQuad));
            if (isDelayShowing)
            {
                sequence.Append(skillitemSlot.transform.DOScale(1.5f, 0.2f).SetLoops(4));
                sequence.Append(skillitemSlot.transform.DOScale(1f, 0.2f));
            }

            sequence.Play();
            

            skillitemSlot.gameObject.SetActive(true);

            children.Add(skillitemSlot.gameObject);

            yield return new WaitForSeconds(0.1f);

            yield return sequence.WaitForCompletion();

        }

        isCoroutineDone = true;
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    // 다시 뽑기 버튼 누르면 호출됨
    public void OneMoreTime1()
    {
        if (isCoroutineDone == false)
            return;

        oneMoreTimeAction?.Invoke(1);
    }

    public void OneMoreTime11()
    {
        if (isCoroutineDone == false)
            return;

        oneMoreTimeAction?.Invoke(11);
    }

}
