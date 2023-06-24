using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int Current_HP;
    public int HP;
    public int MonsterExp = 10;

    Rigidbody2D rigid;
    public Collider collider;

    public GameObject expIconPrefab; //경험치 아이콘
    public GameObject coinIconPrefab; //골드 아이콘
    public GameObject weaponPrefab;
    public float weaponPrefabProbability;
    public GameObject shieldPrefab;
    public float shieldPrefabProbability;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider>();

        // Rigidbody2D : Component : Object
        
        //상위 클래스에서 파생된 하위 클래스들은 상위 클래스 타입으로 정의된 곳에서도 사용이 가능함
        PrintName(rigid);
        PrintName(collider);

        Object obj = null;
        obj = rigid; // rigid 변수의 타입 Rigidbody2D는 Object로부터 파생된 타입이기 때문에 Object 타입 변수 obj에 할당할 수 있다.
        //obj = 1; // 1은 int 타입값이며 obj 변수 Object 클래스와 int는 아무런 연관 관계가 없다. 그러므로 타입이 달라서 할당할 수 없다.

        UnitManager.instance.RegisterMonster(this);
    }

    private void OnDestroy()
    {
        //살아있는 몬스터만 UnitManager에 등록되어 있어야 하기 때문에, 죽은 몬스터는 UnregisterMonster()를 통해 UnitManager에서 등록 해제한다.
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
            monsterDeathIcon(expIconPrefab);
            monsterDeathIcon(coinIconPrefab);
            //var battleManager = GameObject.   FindObjectOfType<BattleManager>();
            //battleManager.player.Current_Exp += MonsterExp;

            //UnitManager.instance.player.Current_Exp += MonsterExp;


        }
    }

    public void monsterDeathIcon(GameObject Icon)
    {
        Vector3 offset = new Vector3(0f,0.5f, 0f);
        GameObject expIcon = Instantiate(Icon, transform.position + offset, Quaternion.identity);
        Rigidbody IconRigid = expIcon.GetComponent<Rigidbody>();
        Vector3 IconVec = transform.right * Random.Range(1,1) + Vector3.up * Random.Range(1,2);
        IconRigid.AddForce(IconVec, ForceMode.Impulse);
        IconRigid.AddTorque(Vector3.forward * 0.2f, ForceMode.Impulse);

    }
    public void monsterDeathEquip(GameObject Icon)
    {

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
