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
        // ������ �����ص״� ������ UI���� �ı�
        foreach (GameObject child in children)
        {
            Destroy(child);
        }
        children.Clear();

        this.oneMoreTime = oneMoreTime;
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

        public void OneMoreTime()
        {
            //Invoke("Close", 3f); <-- �̰Ŷ� ���� ��������. �̰� ����Ƽ�� �Լ�

            // Action.Invoke() : Action�� ȣ���Ѵ�.
            // oneMoreTime();  <-- oneMoreTime�� null�̸� ������ �߻�.
            // oneMoreTime.Invoke() <-- oneMoreTime�� null�̸� ������ �߻�.
            // oneMoreTime?.Invoke() <-- ��ü�� ������ �� '?'�� ���̸� ��ü�� null�� �� �������� �ʰ� ����.
            oneMoreTime?.Invoke(); //�̰͵�.. Invoke�� ����µ� Action�� �˻���
        }
    
}
