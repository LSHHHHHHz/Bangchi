using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    public float playerSpeed;

    public GameObject[] weapons;
    public bool[] hasWeapons;

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


    //능력 창
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

    //경험치
    public float Exp = 100;
    public float Current_Exp;
    //public Image Exp_Bar;
    public UnityEngine.UI.Image Exp_Bar; //이렇게 해야 에러가 안생김
    public Text LV_txt;


    //레벨
    public int LV = 1;

    //회복
    public int RecoveryHP;
    public float currentDotTime = 0;
    public float dotTime = 1;

    public int Coin;
    public int Diemond;


    //Ablity 골드 스텟 상태창
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
    public Ability ability;


    private void Start()
    {
        Player_XP(); //경험치
        _Attack.text = Current_Attack + " → " + (AttackLevel + Current_Attack);
        _HP.text = Current_HP + " → " + (HPLevel + Current_HP);

        RefreshWeapon();
        InventoryManager.instance.OnEquippedItemChanged += RefreshWeapon;
    }


    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
        statDataLoad();
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
            // 방패도 여기서 끼기
        }
    }

    public void statDataSave()
    {
        PlayerPrefs.SetFloat("Current_Attack", Current_Attack);
        PlayerPrefs.SetInt("AttackLevel", AttackLevel);

        PlayerPrefs.SetFloat("Current_HP", Current_HP);
        PlayerPrefs.SetInt("HPLevel", HPLevel);

        PlayerPrefs.SetFloat("Current_Recovery", Current_Recovery);
        PlayerPrefs.SetInt("RecoveryLevel", RecoveryLevel);

        PlayerPrefs.SetFloat("Current_CriticalDamage", Current_CriticalDamage);
        PlayerPrefs.SetInt("CriticalDamageLevel", CriticalDamageLevel);

        PlayerPrefs.SetFloat("Current_Criticalprobability", Current_Criticalprobability);
        PlayerPrefs.SetInt("CriticalprobabilityLevel", CriticalprobabilityLevel);

        PlayerPrefs.SetInt("LV", LV);
        PlayerPrefs.SetFloat("Exp", Exp);
        PlayerPrefs.SetFloat("Current_Exp", Current_Exp);

        PlayerPrefs.SetInt("Coin", Coin);

        int abliltyCoinLengh = ability.ablityPrice.Length;
        PlayerPrefs.SetInt("abliltyCoinLengh", abliltyCoinLengh);
        for (int i = 0; i < abliltyCoinLengh; i++)
        {
            string key = "abliltyCoin_" + i;
            int value = ability.ablityPrice[i];
            PlayerPrefs.SetInt(key, value);
        }
       
        // PlayerPrefs에 저장된 값을 디스크에 기록
        PlayerPrefs.Save();
    }

    public void statDataLoad()
    {
        Current_Attack = PlayerPrefs.GetFloat("Current_Attack", 0);
        AttackLevel = PlayerPrefs.GetInt("AttackLevel", 0);

        Current_HP = PlayerPrefs.GetFloat("Current_HP", 0);
        HPLevel = PlayerPrefs.GetInt("HPLevel", 0);

        Current_Recovery = PlayerPrefs.GetFloat("Current_Recovery", 0);
        RecoveryLevel = PlayerPrefs.GetInt("RecoveryLevel", 0);

        Current_CriticalDamage = PlayerPrefs.GetFloat("Current_CriticalDamage", 0);
        CriticalDamageLevel = PlayerPrefs.GetInt("CriticalDamageLevel", 0);

        Current_Criticalprobability = PlayerPrefs.GetFloat("Current_Criticalprobability", 0);
        CriticalprobabilityLevel = PlayerPrefs.GetInt("CriticalprobabilityLevel", 0);

        LV = PlayerPrefs.GetInt("LV", 0);
        Exp = PlayerPrefs.GetFloat("Exp", 0);
        Current_Exp = PlayerPrefs.GetFloat("Current_Exp", 0);


        Coin = PlayerPrefs.GetInt("Coin", 0);

        if (PlayerPrefs.HasKey("abliltyCoinLengh"))
        {
            int abliltyCoinLengh = PlayerPrefs.GetInt("abliltyCoinLengh", 0);
            ability.ablityPrice = new int[abliltyCoinLengh];
            for (int i = 0; i < abliltyCoinLengh; i++)
            {
                string key = "abliltyCoin_" + i;
                int value = PlayerPrefs.GetInt(key, 0);
                ability.ablityPrice[i] = value;
            }
        }
        
    }


    public void Player_XP()
    {
        Exp = LV * 100;
    }

    public void LV_UP()
    {
        if (Current_Exp >= Exp)
        {
            Current_Exp -= Exp; //현재 경험치 - 총 경험치
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
        _Attack.text = Current_Attack + " → " + (AttackLevel + Current_Attack);
        _AttackLevel.text = "LV" + AttackLevel;

        _HP.text = Current_HP + " → " + (HPLevel + Current_HP);
        _HPLevel.text = "LV" + HPLevel;

        _Recovery.text = Current_Recovery + " → " + (RecoveryLevel + Current_Recovery);
        _RecoveryLevel.text = "LV" + RecoveryLevel;

        _CriticalDamage.text = $"{Current_CriticalDamage:F1} → {(0.1f + Current_CriticalDamage):F1}";
        _CriticalDamageLevel.text = "LV" + CriticalDamageLevel;

        _Criticalprobability.text = Current_Criticalprobability + " → " + (CriticalprobabilityLevel + Current_Criticalprobability);
        _CriticalprobabilityLevel.text = "LV" + CriticalprobabilityLevel;

        statDataSave();
    }



    void Move()
    {
        Vector2 moveVec = new Vector2(playerSpeed, 0);
        rigid.velocity = moveVec * playerSpeed * Time.deltaTime;
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

        if (Physics.Raycast(ray, out hit, distance))
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

        if (Time.time - fightStartTime > 1f)
        {
            isFighting = false;
            anim.SetTrigger("battleEnd");
        }
    }


}
