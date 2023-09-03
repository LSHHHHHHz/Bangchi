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

    public GameObject expIconPrefab; //����ġ ������
    public GameObject coinIconPrefab; //��� ������
    public GameObject enforceCoinPrefab; //��ȭ�� ������

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
        obj = rigid; // rigid ������ Ÿ�� Rigidbody2D�� Object�κ��� �Ļ��� Ÿ���̱� ������ Object Ÿ�� ���� obj�� �Ҵ� ����

        UnitManager.instance.RegisterMonster(this);
    }

    private void OnDestroy()
    {
        //����ִ� ���͸� UnitManager�� ��ϵǾ� �־�� �ϱ� ������, ���� ���ʹ� UnregisterMonster()�� ���� UnitManager���� ��� ����
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
