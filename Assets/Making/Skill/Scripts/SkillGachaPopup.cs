using Assets.HeroEditor.Common.Scripts.Common;
using Assets.HeroEditor.InventorySystem.Scripts.Elements;
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
    public GameObject NonegachaEffectPrefab;

    public GridLayoutGroup grid;
    public GridLayoutGroup effectGrid;

    public GameObject itemPrefab;

    private List<GameObject> children = new List<GameObject>();
    private List<GameObject> effectchildren = new List<GameObject>();

    private Action<int> oneMoreTimeAction; // oneMoreTime 전달받은 값을 저장하기 위해 따로 멤버 필드로 가지고 있음.
    private bool isCoroutineDone = true;
    private bool isCoroutineActive = false;


    public void Initialize(SkillGachaResult skillgachaResult, Action<int> oneMoreTime)
    {
        
        // 이전에 보관해뒀던 아이템 UI들을 파괴
        foreach (GameObject child in children)
        {
            Destroy(child); //객체 파괴
        }
        children.Clear(); // 리스트 비움
        foreach (GameObject child in effectchildren)
        {
            Destroy(child); //객체 파괴
        }
        children.Clear();
        this.oneMoreTimeAction = oneMoreTime;

        isCoroutineDone = false;
        StartCoroutine(SetupCoroutine(skillgachaResult));
    }

    private IEnumerator SetupCoroutine(SkillGachaResult skillgachaResult)
    {
        yield return transform.DOLocalMoveX(0, 1f).Play().WaitForCompletion();

        for (int i = 0; i < skillgachaResult.items.Count; ++i)
        {
            SkillInfo skillInfo = skillgachaResult.items[i];
            bool isDelayShowing = (int)skillInfo.grade >= (int)SkillGrade.B;

          
            if(isDelayShowing)
            {
                var effectwait = Instantiate(gachaEffectPrefab, effectGrid.transform);
                yield return new WaitForSeconds(2f);
                Destroy(effectwait);
            }
            // 아이템 슬롯 생성
            SkillSlot skillslot = Instantiate(itemPrefab, grid.transform).GetComponent<SkillSlot>();
            // 아이템 슬롯에 뽑은 아이템 데이터 적용
            skillslot.SetData(skillInfo);

            skillslot.transform.localScale = Vector3.one * 4f;
            skillslot.transform.DOScale(Vector3.one, 0.1f);
            if (isDelayShowing)
            {
                var effect = Instantiate(gachaEffectPrefab, effectGrid.transform);
                ParticleSystem particleSystem = effect.GetComponent<ParticleSystem>();
                var mainModule = particleSystem.main;
                switch (skillInfo.grade)
                {
                    case SkillGrade.B:
                        mainModule.startColor = Color.red; // 빨간색
                        break;
                    case SkillGrade.A:
                        mainModule.startColor = Color.blue; // 파란색
                        break;
                    case SkillGrade.S:
                        mainModule.startColor = Color.yellow; // 금색
                        break;

                }
                effectchildren.Add(effect);
            }
            else
            {
                var noneffect = Instantiate(NonegachaEffectPrefab, effectGrid.transform);
                effectchildren.Add(noneffect);
            }

            skillslot.icon.SetActive(false);
            skillslot.effectImage.SetActive(true);
            skillslot.backGroundImage.SetActive(false);
            skillslot.effectImage.color = new Color(1,1,1,0);
            var sequence = DOTween.Sequence();
            sequence.Append(skillslot.effectImage.DOFade(1, 0.2f));
            sequence.AppendCallback(() => skillslot.icon.SetActive(true));
            sequence.AppendCallback(() => skillslot.backGroundImage.SetActive(true));
            sequence.Append(skillslot.effectImage.DOFade(0, 0.3f));

           // sequence.Append(skillslot.transform.DOScale(1, 0.2f));
            sequence.Play();
            

            skillslot.gameObject.SetActive(true);
            children.Add(skillslot.gameObject);

            yield return skillslot.transform.DOScale(Vector3.one, 0.15f).WaitForCompletion();

            //sequence.WaitForCompletion을 쓰게 되면 위 WaitForSecondes가 의미없음
            //Sequence가 더 시간이 길면 의미가 없다는 말
            // yield return sequence.WaitForCompletion();

        }

        isCoroutineDone = true;
    }
     public void isposibbleCloss()
    {
        if (isCoroutineDone)
        {
            Close();
        }
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
