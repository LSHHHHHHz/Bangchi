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
    Action oneMoreTimeAction; // oneMoreTime ���޹��� ���� �����ϱ� ���� ���� ��� �ʵ�� ������ ����.

    public void Initialize(GachaResult gachaResult, Action oneMoreTime, Action staticMethod)
    {
        staticMethod();
        // ������ �����ص״� ������ UI���� �ı�
        foreach (GameObject child in children)
        {
            Destroy(child);
        }
        children.Clear();

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
    }
        public void Close()
        {
            Destroy(gameObject);
        }

    // �ٽ� �̱� ��ư ������ ȣ���
        public void OneMoreTime()
        {
            //Invoke("Close", 3f); <-- �̰Ŷ� ���� ��������. �̰� ����Ƽ�� �Լ�

            // Action.Invoke() : Action�� ȣ���Ѵ�.
            // oneMoreTime();  <-- oneMoreTime�� null�̸� ������ �߻�.
            // oneMoreTime.Invoke() <-- oneMoreTime�� null�̸� ������ �߻�.
            // oneMoreTime?.Invoke() <-- ��ü�� ������ �� '?'�� ���̸� ��ü�� null�� �� �������� �ʰ� ����.
            oneMoreTimeAction?.Invoke(); //�̰͵�.. Invoke�� ����µ� Action�� �˻���
        //  oneMoreTimeAction();
        }
    
}
