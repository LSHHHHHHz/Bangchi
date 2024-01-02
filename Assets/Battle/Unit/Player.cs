using Assets.Battle;
using Assets.Battle.Unit;
using Assets.HeroEditor.InventorySystem.Scripts.Elements;
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
    public int weponsIndex;

    Monster monsters;
    Rigidbody rigid;
    DropItem prefabs;
    public Animator anim;

    //트랜스폼을 담을 변수
    public Transform tr;
    //레이 길이
    public float raycastDistance;
    //충돌 정보를 가져올 레이캐스트 히트
    public RaycastHit hit;
    //충돌 정보를 여려개 담을 히트배열
    public RaycastHit[] hits;
    //레이어 마스크를 지정할 변수
    public LayerMask layerMask = -1;


    //능력 창
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

    //경험치
    public int Exp = 100;
    public int Current_Exp;
    //public Image Exp_Bar;
    public UnityEngine.UI.Image Exp_Bar; //이렇게 해야 에러가 안생김
    public Text LV_txt;


    //레벨
    public int LV = 1;

    //회복
    public float currentDotTime = 0;
    public float dotTime = 1;

    public int Coin;
    public int PetCoin;
    public int ColleageCoinWater;
    public int ColleageCoinSoil;
    public int ColleageCoinWind;
    public int ColleageCoinFire;
    public int Diamond;
    public int enforceCoin;

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

    //기본 스텟 창 골드, 강화석, 다이아
    public Text _TopGold;
    public Text _TopEnforceCoin;
    public Text _TopDia;

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
        Player_XP(); //경험치
        _Attack.text = Current_Attack + " → " + (AttackLevel + Current_Attack);
        _HP.text = Max_HP + " → " + (HPLevel + Max_HP);

        RefreshWeapon(); //이걸 빼면 캐릭터에 무기가 장착되어있지 않음
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
        TopStatus();
    }

    private void TopStatus()
    {
        _TopGold.text = Coin.ToString("N0");
        _TopEnforceCoin.text = enforceCoin.ToString("N0");
        _TopDia.text = Diamond.ToString("N0");
    }

    private ItemInstance previousEquippedWeapon = null;
    private ItemInstance previousEquippedShield = null;
    bool isrestarts = false;

    private void RefreshWeapon()
    {
        foreach (ItemInstance equippedItem in InventoryManager.instance.equippedItems)
        {
            if (equippedItem.itemInfo.type == ItemType.Sword)
            {
                int weaponIndex = equippedItem.itemInfo.Number - 1;

                //아이템을 장착하지 않았거나, 기존 장착 아이템 정보와 새로운 아이템 정보가 다를때만 실행
                if (previousEquippedWeapon == null || previousEquippedWeapon.itemInfo != equippedItem.itemInfo)
                {
                    // 이전에 장착된 아이템의 공격력 제거
                    if (previousEquippedWeapon != null)
                    {
                        Current_Attack -= previousEquippedWeapon.itemInfo.Attack;
                    }

                    // 새 아이템 장착
                    for (int i = 0; i < weapons.Length; ++i)
                    {
                        weapons[i].SetActive(i == weaponIndex);
                    }
                    // 새 아이템 공격력 추가

                    Current_Attack += equippedItem.itemInfo.Attack;
                                     
                    previousEquippedWeapon = equippedItem;
                }
            }
            // 방패도 여기서 끼기
            if (equippedItem.itemInfo.type == ItemType.Shield)
            {
                int shiledIndex = equippedItem.itemInfo.Number - 1;
                //아이템을 장착하지 않았거나, 기존 장착 아이템 정보와 새로운 아이템 정보가 다를때만 실행
                if(previousEquippedShield == null || previousEquippedShield.itemInfo != equippedItem.itemInfo)
                {
                    if(previousEquippedShield != null)
                    {
                        Max_HP -= previousEquippedShield.itemInfo.HP;
                        RecoveryHP -= previousEquippedShield.itemInfo.HP_recovery;
                    }
                    for (int i = 0; i < shiled.Length; ++i) 
                    {
                        shiled[i].SetActive(i == shiledIndex);
                    }
                    Max_HP += equippedItem.itemInfo.HP;
                    RecoveryHP += equippedItem.itemInfo.HP_recovery;

                    previousEquippedShield = equippedItem;
                }
            }
        }
    }
                //for (int i = 0; i < shiled.Length; ++i)
                //{
                //    if (i == shiledIndex)
                //    {
                //        shiled[i].SetActive(true);
                //    }
                //    else
                //    {
                //        shiled[i].SetActive(false);
                //    }
                //}

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

        // PlayerPrefs에 저장된 값을 디스크에 기록
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

        _HP.text = Max_HP + " → " + (HPLevel + Max_HP);
        _HPLevel.text = "LV" + HPLevel;

        _Recovery.text = RecoveryHP + " → " + (RecoveryLevel + RecoveryHP);
        _RecoveryLevel.text = "LV" + RecoveryLevel;

        _CriticalDamage.text = $"{Current_CriticalDamage:F1} → {(0.1f + Current_CriticalDamage):F1}";
        _CriticalDamageLevel.text = "LV" + CriticalDamageLevel;

        _Criticalprobability.text = Current_Criticalprobability + " → " + (CriticalprobabilityLevel + Current_Criticalprobability);
        _CriticalprobabilityLevel.text = "LV" + CriticalprobabilityLevel;

        statDataSave();
    }



    void Move()
    {
        if (isFighting || isSkillCasting)
            return;

        Vector2 moveVec = new Vector2(playerSpeed, 0);
        //rigid.velocity = moveVec * playerSpeed * Time.deltaTime; // 물리적인 힘으로 움직이게끔 하는 것, 이 플레이어는 그럴 필요가 없어서 Translate로 강제로 이동시키기.
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

        // 내가 전투가 끝난지 아닌지 1초마다 검사.
        if (Time.time - fightStartTime > 1f)
        {
            isFighting = NeedToFight();

            // 전투가 끝난 것.
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

    // 내가 전투해야 하는지, 아닌지 검사하는 함수.
    private bool NeedToFight()
    {
        //레이 세팅
        Ray ray = new Ray();
        //레이 시작 지점
        ray.origin = tr.position;
        //방향 설정
        ray.direction = tr.right;
        //레이 사용 방법(레이에 검출되는 것이 있다면)

        //RaycastAll은 RaycastHits[] 를 반환한다
        // Physics.Raycast <-- 1개만 반환, RayCastAll은 여러개를 반환.
        hits = Physics.RaycastAll(ray, raycastDistance, layerMask);
        for (int i = 0; i < hits.Length; ++i)
        {
            RaycastHit hit = hits[i];
            print(hit.collider.name + "를 충돌체로 검출");

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
    }
}
