using Assets.HeroEditor.InventorySystem.Scripts.Elements;
using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float Weapon_damage;
    public float Current_totalDamage;
    public float rate;
    public CapsuleCollider capsule;
    public RectTransform weaponSlotParent;

    public ItemGrade grade;


    void Start()
    {
        
    }

    private void Update()
    {
        totalDamage(Weapon_damage);
    }


    public float totalDamage(float weaponDamage)
    {
        float totalDamage = Weapon_damage + Player.instance.Current_Attack;
        Current_totalDamage = totalDamage;

        return Current_totalDamage;
    }

  

    //기본데미지가 있고 등급별로 추가 데미지가 있음
    //토탈데미지가 실제 데미지
    
}
