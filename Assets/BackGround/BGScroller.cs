using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BGScroller : MonoBehaviour
{
    //플레이어가 x축으로 5 이동하면 원하는 위치에 배경 생성

    public Player player;
    public GameObject backgroundPrefab;
    public Image backgroundImage;
    public float distance = 5f; // 플레이어가 이동해야 할 거리
    private float lastSpawnX; // 마지막으로 프리팹을 생성한 x 위치
    int count = 1; //몇번 만들어졌는지
    BGScroller BGPopup;
    List<BGScroller> bg = new List<BGScroller>();
    private void Awake()
    {
        player = GameObject.Find("Human").GetComponent<Player>();
        
    }
    void Start()
    {
        lastSpawnX = player.transform.position.x;
    }

    void Update()
    {
        if (player.transform.position.x >= lastSpawnX + distance) 
        {
            var prefab = Resources.Load<GameObject>("backgroundPopup");

            BGPopup = Instantiate(prefab, new Vector3(2.2f + 11.5f*count, 3.37f, 0), Quaternion.identity).GetComponent<BGScroller>();
            lastSpawnX = player.transform.position.x;
            count++;
            bg.Add(BGPopup);
        }
    }
    public void RunBGPoopup()
    {
        if(BGPopup == null)
        {
            var prefab = Resources.Load<GameObject>("backgroundPopup");
            BGPopup = Instantiate(prefab, new Vector3(2.2f, 3.37f, 0), Quaternion.identity).GetComponent<BGScroller>();
        }
    }
}

