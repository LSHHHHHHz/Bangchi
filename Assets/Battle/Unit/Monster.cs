using Assets.Battle;
using Assets.Battle.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : BaseUnit
{
    public GameObject hudDamageText;

    public int MonsterExp = 10;

    Rigidbody2D rigid;
    public Collider collider;

    public GameObject expIconPrefab; //경험치 아이콘
    public GameObject coinIconPrefab; //골드 아이콘
    public GameObject enforceCoinPrefab; //강화석 아이콘

    public GameObject weaponPrefab;
    public float weaponPrefabProbability;
    public GameObject shieldPrefab;
    public float shieldPrefabProbability;


    public void Awake()
    {
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
    }

    public void PrintName(Object obj)
    {
        Debug.Log(obj);
    }

    public void Update()
    {
        if (_Current_HP <= 0)
        {
            Destroy(gameObject);
            monsterDeathIcon(expIconPrefab);
            monsterDeathIcon(coinIconPrefab);
            monsterDeathIcon(enforceCoinPrefab);

            monsterDeathIcon(weaponPrefab, weaponPrefabProbability);
            monsterDeathIcon(shieldPrefab, shieldPrefabProbability);
        }
    }

    public void monsterDeathIcon(GameObject whatIcon)
    {
        Vector3 offset = new Vector3(0f, 0.5f, 0f);
        float power = 1f;
        GameObject Icon = Instantiate(whatIcon, transform.position + offset, Quaternion.identity);
        Rigidbody IconRigid = Icon.GetComponent<Rigidbody>();

        Vector3 powerVector = Vector3.right + Vector3.up * Random.Range(1, 2);
        powerVector *= power;
        IconRigid.AddForce(powerVector, ForceMode.Impulse);
    }

    public void monsterDeathIcon(GameObject whatIcon, float probability)
    {
        float randomValue = Random.value;
        if (randomValue < probability)
        {
            Vector3 offset = new Vector3(0f, 0.5f, 0f);
            GameObject Icon = Instantiate(whatIcon, transform.position + offset, Quaternion.identity);
            Rigidbody IconRigid = Icon.GetComponent<Rigidbody>();
            Vector3 IconVec = transform.right * Random.Range(1, 1) + Vector3.up * Random.Range(1, 2);
            IconRigid.AddForce(IconVec, ForceMode.Impulse);
            IconRigid.AddTorque(Vector3.forward * 0.2f, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            Weapons weapons = other.GetComponent<Weapons>();

            _Current_HP -= weapons.Current_totalDamage;

            GameObject hudeText = Instantiate(hudDamageText);
            hudeText.transform.position = transform.position + new Vector3(0, 1, 0);
            hudeText.GetComponent<DamageText>().damage = (int)weapons.Current_totalDamage;
        }
    }

    IEnumerator Death()
    {
        yield return null;
    }
}
