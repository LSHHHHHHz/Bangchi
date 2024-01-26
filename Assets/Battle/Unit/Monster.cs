using Assets.Battle;
using Assets.Battle.Projectile;
using Assets.Battle.Unit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : BaseUnit
{
    public GameObject hudDamageText;
    GameObject hudTextRoot;
    GameObject DropItemRoot;
    Rigidbody2D rigid;
    public Collider collider;

    public GameObject expIconPrefab; 
    public GameObject coinIconPrefab; 
    public GameObject enforceCoinPrefab; 

    public GameObject weaponPrefab;
    public float weaponPrefabProbability;
    public GameObject shieldPrefab;
    public float shieldPrefabProbability;

    private float elapsedTime;
    public void Awake()
    {
        hudTextRoot = GameObject.Find("hudTextRoot");
        DropItemRoot = GameObject.Find("DropItemRoot");
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider>();

        PrintName(rigid);
        PrintName(collider);

        Object obj = null;
        obj = rigid; // rigid 변수의 타입 Rigidbody2D는 Object로부터 파생된 타입이기 때문에 Object 타입 변수 obj에 할당 가능

        UnitManager.instance.RegisterMonster(this);
    }

    private void OnDestroy()
    {
        //살아있는 몬스터만 UnitManager에 등록되어 있어야 하기 때문에, 죽은 몬스터는 UnregisterMonster()를 통해 UnitManager에서 등록 해제
        UnitManager.instance.UnregisterMonster(this);
        if(this._MonsterInfoType == MonsterInfoType.boss)
        {
            BattleManager.instance.isrestartNomarStage = true;
        }
    }

    public void PrintName(Object obj)
    {
        Debug.Log(obj);
    }

    public void Update()
    {
        if (_Current_HP <= 0)
        {
            AudioManager.instance.PlaySound("MonsterDie");
            elapsedTime += Time.deltaTime;
            if (_MonsterInfoType == MonsterInfoType.normar)
            {
                Destroy(gameObject);

                monsterDeathIcon(expIconPrefab);
                monsterDeathIcon(coinIconPrefab);
                monsterDeathIcon(enforceCoinPrefab);

                monsterDeathIcon(weaponPrefab, weaponPrefabProbability);
                monsterDeathIcon(shieldPrefab, shieldPrefabProbability);
            }
        }
    }

    public void monsterDeathIcon(GameObject whatIcon)
    {
        Vector3 offset = new Vector3(0f, 0.5f, 0f);
        GameObject Icon = Instantiate(whatIcon, transform.position + offset, Quaternion.identity, DropItemRoot.transform);
        Rigidbody IconRigid = Icon.GetComponent<Rigidbody>();

        Vector3 powerVector = Vector3.right + Vector3.up * Random.Range(1, 2);
        IconRigid.AddForce(powerVector, ForceMode.Impulse);
    }

    public void monsterDeathIcon(GameObject whatIcon, float probability)
    {
        float randomValue = Random.value;
        if (randomValue < probability)
        {
            Vector3 offset = new Vector3(0f, 0.5f, 0f);
            GameObject Icon = Instantiate(whatIcon, transform.position + offset, Quaternion.identity, DropItemRoot.transform);
            Rigidbody IconRigid = Icon.GetComponent<Rigidbody>();
            Vector3 IconVec = transform.right * Random.Range(1, 1) + Vector3.up * Random.Range(1, 2);
            IconRigid.AddForce(IconVec, ForceMode.Impulse);
            IconRigid.AddTorque(Vector3.forward * 0.2f, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        bool isCriticalHit = UnityEngine.Random.value < Player.instance.Current_Criticalprobability / 100;
        if (other.tag == "Melee")
        {
           
            int damageAmount = isCriticalHit ? (int)Player.instance.Current_CriticalDamage + (int)Player.instance.Current_Attack
                                             : (int)Player.instance.Current_Attack;

            GameObject hudText = Instantiate(hudDamageText, hudTextRoot.transform);
            hudText.transform.position = transform.position + new Vector3(0, 1, 0);

            DamageText tmp = hudText.GetComponent<DamageText>();
            tmp.text.text = damageAmount.ToString();
            tmp.text.color = isCriticalHit ? Color.red : Color.blue;

            _Current_HP -= damageAmount;
        }
        if (other.tag == "Skill")
        {
            BaseSkillLaunch skill = other.GetComponent<BaseSkillLaunch>();
            int damageAmount = isCriticalHit ? (int)skill.damage + (int)Player.instance.Current_CriticalDamage
                                             : (int)skill.damage;

            _Current_HP -= damageAmount;
            GameObject hudText = Instantiate(hudDamageText, hudTextRoot.transform);
            hudText.transform.position = transform.position + new Vector3(0, 1, 0);

            DamageText tmp = hudText.GetComponent<DamageText>();
            tmp.text.text = damageAmount.ToString();
            tmp.text.color = isCriticalHit ? Color.red : Color.blue;

        }
    }
}
