using Assets.Battle.Unit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    ParticleSystem particleSystem;
    Monster monster;
    public float elapsedTime;
    public bool condition = false;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        monster = GetComponent<Monster>();
    }
    private void Update()
    {
        particleCondition();
        if (monster._MonsterInfoType == MonsterInfoType.boss && monster._Current_HP<=0)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime>=2f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void particleCondition()
    {
        if(particleSystem.time >=1)
        {
            if(!condition)
            {
                particleSystem.Stop();
                particleSystem.Play();
            }
        }
    }
}
