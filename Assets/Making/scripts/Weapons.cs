using Assets.HeroEditor.InventorySystem.Scripts.Elements;
using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int damage;
    public float rate;
    public CapsuleCollider capsule;
    public RectTransform weaponSlotParent;

    public ItemGrade grade;


    void Start()
    {
        totalDamage(damage);
        Debug.Log(totalDamage(damage));
        
    }
    public int AddDamage(ItemGrade grade)
    {
        int addDamage = 0;

        switch (grade)
        {
            case ItemGrade.FFF:
                addDamage = 10;
                break;
            case ItemGrade.EEE:
                addDamage = 20;
                break;
            case ItemGrade.CCC:
                addDamage = 30;
                break;
            case ItemGrade.BBB:
                addDamage = 40;
                break;
            case ItemGrade.AAA:
                addDamage = 50;
                break;
            case ItemGrade.SSS:
                addDamage = 60;
                break;
        }

        return addDamage;
    }

    public int totalDamage(int baseDamage)
    {
        int addDamage = AddDamage(grade);
        int totalDamage = baseDamage + addDamage;
            
        return totalDamage;
    }

  

    //기본데미지가 있고 등급별로 추가 데미지가 있음
    //토탈데미지가 실제 데미지
    
}
