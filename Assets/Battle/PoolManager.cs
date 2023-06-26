using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    //프리펩 보관할 변수
    public GameObject[] monsterPrefabs;
    //풀 담당하는 리스트들
    List<GameObject>[] pools;

    void Awake()
    {
        Instance = this;
        pools = new List<GameObject>[monsterPrefabs.Length];
        for(int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        //선택한 풀의 놀고 있는 게임오브젝트 접근
        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            { //발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        //못찾았으면
        if(select == null)
        {
            //새롭게 생성하고 select 변수에 할당
            select = Instantiate(monsterPrefabs[index], transform); //transform을 넣는 이유는 PollManager에 넣기 위해서임(지저분해지지 않기 위해)
            pools[index].Add(select);

        }

        return select;
    }
}
