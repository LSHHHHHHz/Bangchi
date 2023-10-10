using Assets.Battle;
using Assets.Battle.Unit;
using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : BaseUnit
{

    public float playerSpeed;

    public GameObject[] weapons;
    public GameObject[] shiled;

    Monster monsters;
    Rigidbody rigid;
    DropItem prefabs;
    public Animator anim;

    //Ʈ�������� ���� ����
    public Transform tr;
    //���� ����
    public float raycastDistance;
    //�浹 ������ ������ ����ĳ��Ʈ ��Ʈ
    public RaycastHit hit;
    //�浹 ������ ������ ���� ��Ʈ�迭
    public RaycastHit[] hits;
    //���̾� ����ũ�� ������ ����
    public LayerMask layerMask = -1;


    //�ɷ� â
    public float Current_Attack;
    public int AttackLevel;

    public int Max_HP;
    public int Current_HP;
    public int HPLevel;

    public int RecoveryHP;
    public int RecoveryLevel;

    public float Current_CriticalDamage;
    public int CriticalDamageLevel;

    public float Current_Criticalprobability;
    public int CriticalprobabilityLevel;

    public int Max_MP;
    public int Current_MP;
    public int MPLevel;

    public int RecoveryMP;
    public int RecoveryMPLevel;

    //����ġ
    public int Exp = 100;
    public int Current_Exp;
    //public Image Exp_Bar;
    public UnityEngine.UI.Image Exp_Bar; //�̷��� �ؾ� ������ �Ȼ���
    public Text LV_txt;


    //����
    public int LV = 1;

    //ȸ��
    public float currentDotTime = 0;
    public float dotTime = 1;

    public int Coin;
    public int PetCoin;
    public int ColleageCoinWater;
    public int ColleageCoinSoil;
    public int ColleageCoinWind;
    public int ColleageCoinFire;
    public int Diemond;
    public int enforceCoin;

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
    private bool isSkillCasting;
    private float skillCastTime;

    Weapons weapons1;
    public Ability ability;

    public static Player instance;

    void Awake()
    {
        instance = this;
        prefabs = new DropItem();
        rigid = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        statDataLoad();
    }


    private void Start()
    {
        Player_XP(); //����ġ
        _Attack.text = Current_Attack + " �� " + (AttackLevel + Current_Attack);
        _HP.text = Max_HP + " �� " + (HPLevel + Max_HP);

        RefreshWeapon();
        InventoryManager.instance.OnEquippedItemChanged += RefreshWeapon;
    }


    // Update is called once per frame
    void Update()
    {
        ablityUpdate();
        LV_txt.text = "LV" + LV;
        Exp_Bar.fillAmount = Exp == 0 ? 0 : (float)Current_Exp / Exp;
        Move();
        rayCast();
        Fighting();
        SkillCasting();
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
            if(equippedItem.itemInfo.type == ItemType.Shield)
            {
                int shiledIndex = equippedItem.itemInfo.Number - 1;
                for (int i = 0; i < shiled.Length; ++i)
                {
                    if (i == shiledIndex)
                    {
                        shiled[i].SetActive(true);
                    }
                    else
                    {
                        shiled[i].SetActive(false);
                    }
                }
            }
        }
    }

    public void statDataSave()
    {
        PlayerPrefs.SetFloat(nameof(Current_Attack), Current_Attack);
        PlayerPrefs.SetInt(nameof(AttackLevel), AttackLevel);

        PlayerPrefs.SetInt(nameof(Current_HP), Max_HP);
        PlayerPrefs.SetInt(nameof(HPLevel), HPLevel);

        PlayerPrefs.SetInt(nameof(RecoveryHP), RecoveryHP);
        PlayerPrefs.SetInt(nameof(RecoveryLevel), RecoveryLevel);

        PlayerPrefs.SetFloat(nameof(Current_CriticalDamage), Current_CriticalDamage);
        PlayerPrefs.SetInt(nameof(CriticalDamageLevel), CriticalDamageLevel);

        PlayerPrefs.SetFloat(nameof(Current_Criticalprobability), Current_Criticalprobability);
        PlayerPrefs.SetInt(nameof(CriticalprobabilityLevel), CriticalprobabilityLevel);

        PlayerPrefs.SetInt(nameof(LV), LV);
        PlayerPrefs.SetInt(nameof(Exp), Exp);
        PlayerPrefs.SetInt(nameof(Current_Exp), Current_Exp);

        PlayerPrefs.SetInt(nameof(Coin), Coin);

        int abliltyCoinLengh = ability.ablityPrice.Length;
        PlayerPrefs.SetInt("abliltyCoinLengh", abliltyCoinLengh);
        for (int i = 0; i < abliltyCoinLengh; i++)
        {
            string key = "abliltyCoin_" + i;
            int value = ability.ablityPrice[i];
            PlayerPrefs.SetInt(key, value);
        }

        PlayerPrefs.SetInt(nameof(ColleageCoinFire), ColleageCoinFire);
        PlayerPrefs.SetInt(nameof(ColleageCoinSoil), ColleageCoinSoil);
        PlayerPrefs.SetInt(nameof(ColleageCoinWater), ColleageCoinWater);
        PlayerPrefs.SetInt(nameof(ColleageCoinWind), ColleageCoinWind);

        // PlayerPrefs�� ����� ���� ��ũ�� ���
        PlayerPrefs.Save();
    }

    public void statDataLoad()
    {
        Current_Attack = PlayerPrefs.GetFloat(nameof(Current_Attack), 0);
        AttackLevel = PlayerPrefs.GetInt(nameof(AttackLevel), 0);

        Max_HP = PlayerPrefs.GetInt(nameof(Current_HP), 0);
        HPLevel = PlayerPrefs.GetInt(nameof(HPLevel), 0);

        RecoveryHP = PlayerPrefs.GetInt(nameof(RecoveryHP), 0);
        RecoveryLevel = PlayerPrefs.GetInt(nameof(RecoveryLevel), 0);

        Current_CriticalDamage = PlayerPrefs.GetFloat(nameof(Current_CriticalDamage), 0);
        CriticalDamageLevel = PlayerPrefs.GetInt(nameof(CriticalDamageLevel), 0);

        Current_Criticalprobability = PlayerPrefs.GetFloat(nameof(Current_Criticalprobability), 0);
        CriticalprobabilityLevel = PlayerPrefs.GetInt(nameof(CriticalprobabilityLevel), 0);

        LV = PlayerPrefs.GetInt(nameof(LV), 0);
        Exp = PlayerPrefs.GetInt(nameof(Exp), 0);
        Current_Exp = PlayerPrefs.GetInt(nameof(Current_Exp), 0);


        Coin = PlayerPrefs.GetInt(nameof(Coin), 0);

        if (PlayerPrefs.HasKey("abliltyCoinLengh"))
        {
            int abliltyCoinLengh = PlayerPrefs.GetInt(nameof(abliltyCoinLengh), 0);
            ability.ablityPrice = new int[abliltyCoinLengh];
            for (int i = 0; i < abliltyCoinLengh; i++)
            {
                string key = "abliltyCoin_" + i;
                int value = PlayerPrefs.GetInt(key, 0);
                ability.ablityPrice[i] = value;
            }
        }

        if (PlayerPrefs.HasKey(nameof(ColleageCoinFire)))
            ColleageCoinFire = PlayerPrefs.GetInt(nameof(ColleageCoinFire));
        if (PlayerPrefs.HasKey(nameof(ColleageCoinSoil)))
            ColleageCoinSoil = PlayerPrefs.GetInt(nameof(ColleageCoinSoil));
        if (PlayerPrefs.HasKey(nameof(ColleageCoinWater)))
            ColleageCoinWater = PlayerPrefs.GetInt(nameof(ColleageCoinWater));
        if (PlayerPrefs.HasKey(nameof(ColleageCoinWind)))
            ColleageCoinWind = PlayerPrefs.GetInt(nameof(ColleageCoinWind));

    }


    public void Player_XP()
    {
        Exp = LV * 100;
    }

    public void LV_UP()
    {
        if (Current_Exp >= Exp)
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

        _HP.text = Max_HP + " �� " + (HPLevel + Max_HP);
        _HPLevel.text = "LV" + HPLevel;

        _Recovery.text = RecoveryHP + " �� " + (RecoveryLevel + RecoveryHP);
        _RecoveryLevel.text = "LV" + RecoveryLevel;

        _CriticalDamage.text = $"{Current_CriticalDamage:F1} �� {(0.1f + Current_CriticalDamage):F1}";
        _CriticalDamageLevel.text = "LV" + CriticalDamageLevel;

        _Criticalprobability.text = Current_Criticalprobability + " �� " + (CriticalprobabilityLevel + Current_Criticalprobability);
        _CriticalprobabilityLevel.text = "LV" + CriticalprobabilityLevel;

        statDataSave();
    }



    void Move()
    {
        if (isFighting || isSkillCasting)
            return;

        Vector2 moveVec = new Vector2(playerSpeed, 0);
        //rigid.velocity = moveVec * playerSpeed * Time.deltaTime; // �������� ������ �����̰Բ� �ϴ� ��, �� �÷��̾�� �׷� �ʿ䰡 ��� Translate�� ������ �̵���Ű��.
        transform.Translate(moveVec * Time.deltaTime);
    }

    void rayCast()
    {
        if (isFighting)
        {
            return;
        }

        isFighting = NeedToFight();
    }
    public void Fighting()
    {
        if (isFighting == false)
            return;

        // ���� ������ ������ �ƴ��� 1�ʸ��� �˻�.
        if (Time.time - fightStartTime > 1f)
        {
            isFighting = NeedToFight();

            // ������ ���� ��.
            if (isFighting == false)
            {
                anim.SetTrigger("battleEnd");
            }
        }
    }

    public void SkillCasting()
    {
        if (isSkillCasting == false)
            return;

        if (Time.time - skillCastTime > 0.5f)
        {
            isSkillCasting = false;
        }
    }

    // ���� �����ؾ� �ϴ���, �ƴ��� �˻��ϴ� �Լ�.
    private bool NeedToFight()
    {
        //���� ����
        Ray ray = new Ray();
        //���� ���� ����
        ray.origin = tr.position;
        //���� ����
        ray.direction = tr.right;
        //���� ��� ���(���̿� ����Ǵ� ���� �ִٸ�)

        //RaycastAll�� RaycastHits[] �� ��ȯ�Ѵ�
        // Physics.Raycast <-- 1���� ��ȯ, RayCastAll�� �������� ��ȯ.
        hits = Physics.RaycastAll(ray, raycastDistance, layerMask);
        for (int i = 0; i < hits.Length; ++i)
        {
            RaycastHit hit = hits[i];
            print(hit.collider.name + "�� �浹ü�� ����");

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("enemy"))
            {
                anim.SetTrigger("doSwing");
                fightStartTime = Time.time;
                return true;
            }
        }

        return false;
    }

    public void OnUseSkill(BaseSkill skill)
    {
        isSkillCasting = true;
        skillCastTime = Time.time;

    }

    private void OnTriggerEnter(Collider collision)
    {
        DropItem prefab = collision.GetComponent<DropItem>();
        if (collision.CompareTag("ExpIcon"))
        {
            //Prefabs prefab = new Prefabs();
            Current_Exp += prefab.exp;
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("CoinIcon"))
        {
            Coin += prefab.coin;
            Destroy(collision.gameObject);
        }
    }

}
