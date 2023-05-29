using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{
    public int Current_HP;
    public int HP;
    public int MonsterExp = 10;

    public GameObject Player;

    Rigidbody2D rigid;
    Collider2D collider;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        // Rigidbody2D : Component : Object
        
        //상위 클래스에서 파생된 하위 클래스들은 상위 클래스 타입으로 정의된 곳에서도 사용이 가능함
        PrintName(rigid);
        PrintName(collider);

        Object obj = null;
        obj = rigid; // rigid 변수의 타입 Rigidbody2D는 Object로부터 파생된 타입이기 때문에 Object 타입 변수 obj에 할당할 수 있다.
        //obj = 1; // 1은 int 타입값이며 obj 변수 Object 클래스와 int는 아무런 연관 관계가 없다. 그러므로 타입이 달라서 할당할 수 없다.
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
            Death();
            //var battleManager = GameObject.FindObjectOfType<BattleManager>();
            //battleManager.player.Current_Exp += MonsterExp;

            BattleManager.instance.player.Current_Exp += MonsterExp;
        }
    }

    public void Death()
    {
        var py = Player.GetComponent<Player>();
        py.Current_Exp += MonsterExp;
        py.LV_UP();
        MonsterExp = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            Weapons weapons = other.GetComponent<Weapons>();

            Current_HP -= weapons.damage;
        }
    }
}
