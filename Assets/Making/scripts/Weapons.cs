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
        float totalDamage = weaponDamage + Player.instance.Current_Attack;
        Current_totalDamage = totalDamage;

        return Current_totalDamage;
    }
}
