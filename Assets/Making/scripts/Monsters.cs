using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{
    public int Current_HP;
    public int HP;
    public int MonsterExp = 10;

    Rigidbody2D rigid;
    Collider2D collider;
    Player player;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        player = GetComponent<Player>();
    }
    public void Update()
    {
        if(Current_HP<=0)
        {
            Destroy(gameObject);
            player.Current_Exp += MonsterExp;
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
}
