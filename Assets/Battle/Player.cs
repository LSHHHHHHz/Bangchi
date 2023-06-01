using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public float playerSpeed;

    Monsters monsters;
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

    public int Current_Attack;
    public int Attack;
    public int AttackLevel;
    public int Current_HP;
    public int HP;
    public int MaxHP;
    public int HPLevel;
    public int Current_MP;
    public int MP;
    public int MPLevel;

    //경험치
    public float Exp = 100;
    public float Current_Exp;
    public Image Exp_Bar;
    public Text LV_txt;


    //레벨
    public int LV = 1;

    //회복
    public int Recovery;
    public int RecoveryHP;
    public float currentDotTime = 0;
    public float dotTime = 1;

    //크리티컬
    public int Critical_value;
    public int Critical_Damage;

    public int Coin;
    public int Diemond;

    public Text _Attack;
    public Text _AttackLevel;
    public Text _HP;
    public Text _HPLevel;
    public Text _HPCoin;

    public Text _MP;

    bool isFireReady;
    public float fireDelay;

    private bool isFighting;
    private float fightStartTime;

    Weapons weapons;
    Ability ability;
    

    private void Start()
    {
        Player_XP(); //경험치
        _Attack.text = Current_Attack + " → " + (AttackLevel + Current_Attack);
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
        LV_txt.text = "LV" + LV;
        Exp_Bar.fillAmount = Current_Exp / Exp;
        Move();
        rayCast();
        Fighting();
        ablityUpdate();
    }

    public void Player_XP()
    {
        Exp = LV * 100;
    }

    public void LV_UP()
    {
        if(Current_Exp>=Exp)
        {
            Current_Exp -= Exp; //현재 경험치 - 총 경험치
            LV++;
            Player_XP();

        }
    }
    void Attack_weapon()
    {
        fireDelay += Time.deltaTime;
        isFireReady = weapons.rate < fireDelay;
    }

    void ablityUpdate()
    {
        _Attack.text = Current_Attack + " → " + (AttackLevel + Current_Attack);
        _AttackLevel.text = "LV" + AttackLevel;
        _HP.text = Current_HP + " → " + (HPLevel + Current_HP);
        _HPLevel.text = "LV" + HPLevel;
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
