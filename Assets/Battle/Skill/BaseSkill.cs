using Assets.Battle.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class BaseSkill : MonoBehaviour
{
    // 스킬을 실행하는 함수
    public BaseUnit owner;
    public int damage;
    public abstract void Execute();

    private void OnTriggerEnter(Collider other)
    {
        if (owner is Player)
        {
            // 몬스터 공격
            var monster = other.gameObject.GetComponent<Monster>();
            if (monster != null)
            {
                monster._Current_HP -= damage;
                // 
            }
        }
        else
        {
            // 플레이어 공격
        }
    }

    
}