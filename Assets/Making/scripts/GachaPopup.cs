using Assets.HeroEditor.InventorySystem.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;

public class GachaPopup : MonoBehaviour //���� ����� �����ִ� UI
{
    public Action runGacha1Action;
    public Action runGacha11Action;


    public GridLayoutGroup grid;
    public GameObject itemPrefab;

    private List<GameObject> children = new List<GameObject>();

    private Action<int> oneMoreTimeAction; // oneMoreTime ���޹��� ���� �����ϱ� ���� ���� ��� �ʵ�� ������ ����.

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
        for (int i = 0; i < gachaResult.items.Count; ++i)
        {
            // ������ ���� ����
            ItemSlot itemSlot = Instantiate(itemPrefab, grid.transform).GetComponent<ItemSlot>();

            // ������ ���Կ� ���� ������ ������ ����
            itemSlot.SetData(gachaResult.items[i]);

            // ������ �������� ���� Active:false������ �̰͵� false�� ����. true�� �ٲ㼭 ���̰� �Ѵ�.
            itemSlot.gameObject.SetActive(true);

            // ���߿� �����ؾߵǴϱ� children�� �־ ����
            children.Add(itemSlot.gameObject);
        }

        //����
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
