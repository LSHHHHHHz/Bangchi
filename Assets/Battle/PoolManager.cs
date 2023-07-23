using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    //������ ������ ����
    public GameObject[] monsterPrefabs;
    //Ǯ ����ϴ� ����Ʈ��
    List<GameObject>[] pools;

    private void Awake()
    {
        Instance = this;
        //Ǯ���� �ʱ�ȭ �ؾ���
        pools = new List<GameObject>[monsterPrefabs.Length];

        //Ǯ �ȿ� �ִ� �͵鵵 �ʱ�ȭ �ؾ���
        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }

    }



    public GameObject Get(int index)
    {
        GameObject select = null;
        //������ Ǯ�� ��� �ִ� ���ӿ�����Ʈ ����
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            { //�߰��ϸ� select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //��ã������
        if (select == null)
        {
            //���Ӱ� �����ϰ� select ������ �Ҵ�
            select = Instantiate(monsterPrefabs[index], transform); //transform�� �ִ� ������ PollManager�� �ֱ� ���ؼ���(������������ �ʱ� ����)
            pools[index].Add(select);

        }
        return select;
    
    }
}
