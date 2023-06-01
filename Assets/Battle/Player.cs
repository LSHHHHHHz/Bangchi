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

    //Ʈ�������� ���� ����
    public Transform tr;
    //���� ����
    public float distance;
    //�浹 ������ ������ ����ĳ��Ʈ ��Ʈ
    public RaycastHit hit;
    //�浹 ������ ������ ���� ��Ʈ�迭
    public RaycastHit[] hits;
    //���̾� ����ũ�� ������ ����
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

    //����ġ
    public float Exp = 100;
    public float Current_Exp;
    public Image Exp_Bar;
    public Text LV_txt;


    //����
    public int LV = 1;

    //ȸ��
    public int Recovery;
    public int RecoveryHP;
    public float currentDotTime = 0;
    public float dotTime = 1;

    //ũ��Ƽ��
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
        Player_XP(); //����ġ
        _Attack.text = Current_Attack + " �� " + (AttackLevel + Current_Attack);
        _HP.text = Current_HP + " �� " + (HPLevel + Current_HP);
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
            Current_Exp -= Exp; //���� ����ġ - �� ����ġ
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
        _Attack.text = Current_Attack + " �� " + (AttackLevel + Current_Attack);
        _AttackLevel.text = "LV" + AttackLevel;
        _HP.text = Current_HP + " �� " + (HPLevel + Current_HP);
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

        //���� ����
        Ray ray = new Ray();
        //���� ���� ����
        ray.origin = tr.position;
        //���� ����
        ray.direction = tr.right;
        //���� ��� ���(���̿� ����Ǵ� ���� �ִٸ�)

        //RaycastAll�� RaycastHits[] �� ��ȯ�Ѵ�
        hits = Physics.RaycastAll(ray, distance, layerMask);

        if(Physics.Raycast(ray, out hit, distance))
       {
            print(hit.collider.name + "�� �浹ü�� ����");
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
