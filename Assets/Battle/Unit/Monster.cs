using Assets.Battle;
using Assets.Battle.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : BaseUnit
{
    public int Current_HP;
    public int HP;
    public int MonsterExp = 10;

    Rigidbody2D rigid;
    public Collider collider;

    public GameObject expIconPrefab; //����ġ ������
    public GameObject coinIconPrefab; //��� ������

    public GameObject weaponPrefab;
    public float weaponPrefabProbability;
    public GameObject shieldPrefab;
    public float shieldPrefabProbability;


    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider>();

        // Rigidbody2D : Component : Object
        
        //���� Ŭ�������� �Ļ��� ���� Ŭ�������� ���� Ŭ���� Ÿ������ ���ǵ� �������� ����� ������
        PrintName(rigid);
        PrintName(collider);

        Object obj = null;
        obj = rigid; // rigid ������ Ÿ�� Rigidbody2D�� Object�κ��� �Ļ��� Ÿ���̱� ������ Object Ÿ�� ���� obj�� �Ҵ��� �� �ִ�.
        //obj = 1; // 1�� int Ÿ�԰��̸� obj ���� Object Ŭ������ int�� �ƹ��� ���� ���谡 ����. �׷��Ƿ� Ÿ���� �޶� �Ҵ��� �� ����.

        UnitManager.instance.RegisterMonster(this);
    }

    private void OnDestroy()
    {
        //����ִ� ���͸� UnitManager�� ��ϵǾ� �־�� �ϱ� ������, ���� ���ʹ� UnregisterMonster()�� ���� UnitManager���� ��� �����Ѵ�.
        UnitManager.instance.UnregisterMonster(this); 
    }

    public void PrintName(Object obj)
    {
        Debug.Log(obj);
    }

    public void Update()
    {
        if(Current_HP<=0)
        {
            Destroy(gameObject);
            monsterDeathIcon(expIconPrefab);  //�̰� �� ������ ������ �ȵ���
            monsterDeathIcon(coinIconPrefab); //�̰� �� ������ ������ �ȵ���

            monsterDeathIcon(weaponPrefab, weaponPrefabProbability);
            monsterDeathIcon(shieldPrefab, shieldPrefabProbability);
            //var battleManager = GameObject.   FindObjectOfType<BattleManager>();
            //battleManager.player.Current_Exp += MonsterExp;

            //UnitManager.instance.player.Current_Exp += MonsterExp;


        }
    }

    public void monsterDeathIcon(GameObject whatIcon)
    {
        Vector3 offset = new Vector3(0f,0.5f, 0f);
        float power = 1f;
        GameObject Icon = Instantiate(whatIcon, transform.position + offset, Quaternion.identity);
        Rigidbody IconRigid = Icon.GetComponent<Rigidbody>();

        // ������ + ������ ���ϴ� ���� ���Ѵ�.
        Vector3 powerVector = Vector3.right/*������ ����*/ + Vector3.up * Random.Range(1,2); /*���� ����*/
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

            Current_HP -= weapons.damage;
        }
    }

    IEnumerator Death()
    {
        yield return null;
    }

}
