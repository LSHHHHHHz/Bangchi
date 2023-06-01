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
        
        //���� Ŭ�������� �Ļ��� ���� Ŭ�������� ���� Ŭ���� Ÿ������ ���ǵ� �������� ����� ������
        PrintName(rigid);
        PrintName(collider);

        Object obj = null;
        obj = rigid; // rigid ������ Ÿ�� Rigidbody2D�� Object�κ��� �Ļ��� Ÿ���̱� ������ Object Ÿ�� ���� obj�� �Ҵ��� �� �ִ�.
        //obj = 1; // 1�� int Ÿ�԰��̸� obj ���� Object Ŭ������ int�� �ƹ��� ���� ���谡 ����. �׷��Ƿ� Ÿ���� �޶� �Ҵ��� �� ����.
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
