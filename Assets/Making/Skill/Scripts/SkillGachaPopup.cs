using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGachaPopup : MonoBehaviour
{
    public Action runGacha1Action;
    public Action runGacha11Action;


    public GridLayoutGroup grid;
    public GameObject itemPrefab;

    private List<GameObject> children = new List<GameObject>();

    private Action<int> oneMoreTimeAction; // oneMoreTime ���޹��� ���� �����ϱ� ���� ���� ��� �ʵ�� ������ ����.

    public void Initialize(SkillGachaResult skillgachaResult, Action<int> oneMoreTime)
    {
        // ������ �����ص״� ������ UI���� �ı�
        foreach (GameObject child in children)
        {
            Destroy(child); //��ü �ı�
        }
        children.Clear(); // ����Ʈ ���°�

        // ���߿� �ٽ� �̱� ��ư ������ ȣ���ϱ� ���� Ŭ������ ��� �ʵ��� oneMoreTimeAction�� ���� �����Ѵ�.
        this.oneMoreTimeAction = oneMoreTime;
        for (int i = 0; i < skillgachaResult.items.Count; ++i)
        {
            // ������ ���� ����
            SkillSlot skillitemSlot = Instantiate(itemPrefab, grid.transform).GetComponent<SkillSlot>();

            // ������ ���Կ� ���� ������ ������ ����
            skillitemSlot.SetData(skillgachaResult.items[i]);

            // ������ �������� ���� Active:false������ �̰͵� false�� ����. true�� �ٲ㼭 ���̰� �Ѵ�.
            skillitemSlot.gameObject.SetActive(true);

            // ���߿� �����ؾߵǴϱ� children�� �־ ����
            children.Add(skillitemSlot.gameObject);
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
