using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    
    public float playerSpeed;

    public GameObject[] weapons;
    public bool[] hasWeapons;

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
    

    //�ɷ� â
    public float Current_Attack;
    public int AttackLevel;

    public float Current_HP;
    public int HPLevel;

    public float Current_Recovery;
    public int RecoveryLevel;

    public float Current_CriticalDamage;
    public int CriticalDamageLevel;

    public float Current_Criticalprobability;
    public int CriticalprobabilityLevel;


    public int Current_MP;
    public int MP;
    public int MaxHP;
    public int MPLevel;

    //����ġ
    public float Exp = 100;
    public float Current_Exp;
    public Image Exp_Bar;
    public Text LV_txt;


    //����
    public int LV = 1;

    //ȸ��
    public int RecoveryHP;
    public float currentDotTime = 0;
    public float dotTime = 1;

    public int Coin;
    public int Diemond;


    //Ablity ��� ���� ����â
    public Text _Attack;
    public Text _AttackLevel;
    public Text _HP;
    public Text _HPLevel;
    public Text _HPCoin;
    public Text _Recovery;
    public Text _RecoveryLevel;
    public Text _CriticalDamage;
    public Text _CriticalDamageLevel;
    public Text _Criticalprobability;
    public Text _CriticalprobabilityLevel;

    public Text _MP;

    bool isFireReady;
    public float fireDelay;

    private bool isFighting;
    private float fightStartTime;

    Weapons weapons1;
    Ability ability;
    

    private void Start()
    {
        Player_XP(); //����ġ
        _Attack.text = Current_Attack + " �� " + (AttackLevel + Current_Attack);
        _HP.text = Current_HP + " �� " + (HPLevel + Current_HP);

        RefreshWeapon();
        InventoryManager.instance.OnEquippedItemChanged += RefreshWeapon;
    }


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        ablityUpdate();
        LV_txt.text = "LV" + LV;
        Exp_Bar.fillAmount = Current_Exp / Exp;
        Move();
        rayCast();
        Fighting();
    }

    private void RefreshWeapon()
    {
        foreach (ItemInstance equippedItem in InventoryManager.instance.equippedItems)
        {
            if (equippedItem.itemInfo.type == ItemType.Sword)
            {
                int weaponIndex = equippedItem.itemInfo.Number - 1;

                for (int i = 0; i < weapons.Length; ++i)
                {
                    if (i == weaponIndex)
                    {
                        weapons[i].SetActive(true);
                    }
                    else
                    {
                        weapons[i].SetActive(false);
                    }
                }
            }
            // ���е� ���⼭ ����
        }
    }

    private void changeWeapon()
    {

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
        isFireReady = weapons1.rate < fireDelay;
    }

    void ablityUpdate()
    {
        _Attack.text = Current_Attack + " �� " + (AttackLevel + Current_Attack);
        _AttackLevel.text = "LV" + AttackLevel;

        _HP.text = Current_HP + " �� " + (HPLevel + Current_HP);
        _HPLevel.text = "LV" + HPLevel;

        _Recovery.text = Current_Recovery + " �� " + (RecoveryLevel + Current_Recovery);
        _RecoveryLevel.text = "LV" + RecoveryLevel;

        _CriticalDamage.text = $"{Current_CriticalDamage:F1} �� {(0.1f + Current_CriticalDamage):F1}";
        _CriticalDamageLevel.text = "LV" + CriticalDamageLevel;

        _Criticalprobability.text = Current_Criticalprobability + " �� " + (CriticalprobabilityLevel + Current_Criticalprobability);
        _CriticalprobabilityLevel.text = "LV" + CriticalprobabilityLevel;


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
