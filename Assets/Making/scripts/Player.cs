using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float playerSpeed;


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
