using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float playerSpeed;


    Rigidbody rigid;
    public Animator anim;

    //트랜스폼을 담을 변수
    public Transform tr;
    //레이 길이
    public float distance;
    //충돌 정보를 가져올 레이캐스트 히트
    public RaycastHit hit;
    //충돌 정보를 여려개 담을 히트배열
    public RaycastHit[] hits;
    //레이어 마스크를 지정할 변수
    public LayerMask layerMask = -1;

    public int Current_HP;
    public int HP;
    public int HPLevel;
    public int Current_MP;
    public int MP;
    public int MPLevel;
    public int Attack;
    public int AttackLevel;
    public int Critical;
    public int CriticalLevel;
    public int CriticalPro;
    public int CriticalProLevel;


    public int Coin;
    public int Amond;

    public Text _HP;
    public Text _MP;
    public Text _Attack;

    private bool isFighting;
    private float fightStartTime;

    private void Start()
    {
        _HP.text = Current_HP + " → " + (HPLevel + Current_HP);
    }


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        rayCast();
        Fighting();
        HPCost();
    }

    void HPCost()
    {
        Current_HP += HPLevel;
    }

    void Move()
    {
        Vector2 moveVec = new Vector2(playerSpeed, 0);
        rigid.velocity = moveVec*playerSpeed*Time.deltaTime;
    }

    void rayCast()
    {
        if (isFighting)
        {
            return;
        }

        //레이 세팅
        Ray ray = new Ray();
        //레이 시작 지점
        ray.origin = tr.position;
        //방향 설정
        ray.direction = tr.right;
        //레이 사용 방법(레이에 검출되는 것이 있다면)

        //RaycastAll은 RaycastHits[] 를 반환한다
        hits = Physics.RaycastAll(ray, distance, layerMask);
        
        if(Physics.Raycast(ray, out hit, distance))
        {
            print(hit.collider.name + "를 충돌체로 검출");
            anim.SetTrigger("doSwing");
            isFighting = true;
            fightStartTime = Time.time;
        }
    }
    public void Fighting()
    {
        if (isFighting == false)
            return;

        if (Time.time  - fightStartTime > 1f)
        {
            isFighting = false;
            anim.SetTrigger("battleEnd");
        }
    }
}
