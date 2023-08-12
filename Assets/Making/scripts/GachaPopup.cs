using Assets.HeroEditor.InventorySystem.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Item1;
using Unity.VisualScripting;

public class GachaPopup : MonoBehaviour //���� ����� �����ִ� UI
{
    public GridLayoutGroup grid;
    public GameObject itemPrefab;

    private List<GameObject> children = new List<GameObject>();

    private Action<int> oneMoreTimeAction; // oneMoreTime ���޹��� ���� �����ϱ� ���� ���� ��� �ʵ�� ������ ����.

    private bool isCoroutineDone = true;

    public void Initialize(GachaResult gachaResult, Action<int> oneMoreTime)
    {
        // ������ �����ص״� ������ UI���� �ı�
        foreach (GameObject child in children)
        {
            Destroy(child); //��ü �ı�
        }
        children.Clear(); // ����Ʈ ���°�

        // ���߿� �ٽ� �̱� ��ư ������ ȣ���ϱ� ���� Ŭ������ ��� �ʵ��� oneMoreTimeAction�� ���� �����Ѵ�.
        this.oneMoreTimeAction = oneMoreTime;

        isCoroutineDone = false;
        StartCoroutine(SetupCoroutine(gachaResult));
    }
    private IEnumerator SetupCoroutine(GachaResult gachaResult)
    {
        yield return transform.DOLocalMoveX(0, 2f).Play().WaitForCompletion();

        for (int i = 0; i < gachaResult.items.Count; ++i)
        {
            ItemInfo itemInfo = gachaResult.items[i];
            bool isHigeGrade = (int)itemInfo.grade >= (int)ItemGrade.C;

            // ������ ���� ����
            ItemSlot itemSlot = Instantiate(itemPrefab, grid.transform).GetComponent<ItemSlot>();

            // ������ ���Կ� ���� ������ ������ ����
            itemSlot.SetData(gachaResult.items[i]);

            var sequence = DOTween.Sequence();
            itemSlot.transform.localScale = Vector3.one * 3;
            
            if (isHigeGrade)
            {
                sequence.Append(itemSlot.transform.DOScale(1.5f, 0.2f).SetLoops(4));
                sequence.Append(itemSlot.transform.DOScale(1f, 0.2f));
            }

            sequence.Play();

            // ������ �������� ���� Active:false������ �̰͵� false�� ����. true�� �ٲ㼭 ���̰� �Ѵ�.
            itemSlot.gameObject.SetActive(true);

            // ���߿� �����ؾߵǴϱ� children�� �־ ����
            children.Add(itemSlot.gameObject);
            yield return itemSlot.transform.DOScale(Vector3.one, 0.1f).WaitForCompletion();

            yield return sequence.WaitForCompletion();

        }
        isCoroutineDone = true;
    }
    public void Close()
    {
        Destroy(gameObject);
    }

    // �ٽ� �̱� ��ư ������ ȣ���
    public void OneMoreTime1()
    {
        oneMoreTimeAction?.Invoke(1);
    }

    public void OneMoreTime11()
    {
        oneMoreTimeAction?.Invoke(11);
    }

}
