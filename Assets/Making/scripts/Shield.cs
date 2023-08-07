using Assets.HeroEditor.InventorySystem.Scripts.Elements;
using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float Shield_HP;
    public float Current_totalHP;
    public CapsuleCollider capsule;
    public RectTransform weaponSlotParent;

    public ItemGrade grade;

    private void Update()
    {
        totalHP(Shield_HP);
    }

    public float totalHP(float shieldHP)
    {
        float totalHP = shieldHP + Player.instance.Current_HP;
        this.Current_totalHP = totalHP;
        return Current_totalHP;
    }
}
