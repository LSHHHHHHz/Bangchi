using Assets.Battle.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkillLaunch : MonoBehaviour
{
    public BaseUnit owner;
    public float damage;
    public float elapsedTime;
    private void OnTriggerEnter(Collider other)
    {
        if (owner is Player)
        {
            // 몬스터 공격
            var monster = other.gameObject.GetComponent<Monster>();
            if (monster != null)
            {
                monster._Current_HP -= damage;

            }
        }
        else
        {
            // 플레이어 공격
        }
    }
}
