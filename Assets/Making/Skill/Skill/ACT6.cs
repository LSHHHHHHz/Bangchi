using Assets.Battle.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACT6 : BaseSkill
{
    public GameObject projectilePrefab;
    bool isSkill = false;
    public float TimePass = 0;
    public float CoolTime = 2;

    public void Update()
    {
        waitSkillCoolTime();
    }
    public override void Execute()
    {
        if (!isSkill)
        {
            isSkill = true;
            var projectile = Instantiate(projectilePrefab).GetComponent<BaseProjectile>();
            projectile.transform.position = owner.transform.position;
            projectile.owner = owner;
            projectile.direction = Vector3.right;
        }

    }
    public void waitSkillCoolTime()
    {
        if(isSkill)
        {
            TimePass += Time.deltaTime;
            if(TimePass > CoolTime)
            {
                isSkill = false;
                TimePass = 0;
            }
        }
    }


    //ÀÌÀü
    public void waitSkillTime()
    {
        isSkill = true;
        TimePass = Time.deltaTime;
        if(TimePass > CoolTime)
        {
            isSkill = false;
            TimePass = 0;
        }
    }
}
