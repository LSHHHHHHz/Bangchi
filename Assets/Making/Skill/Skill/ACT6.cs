using Assets.Battle.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACT6 : BaseSkill
{
    public GameObject projectilePrefab;
    public bool isSkill = false;
    public float TimePass = 0;
    public float CoolTime = 2;

    public static ACT6 instance;
    private void Awake()
    {
        instance = this;
    }
    public void Update()
    {
        waitSkillCoolTime();
    }
    public override void Execute()
    {
        if (!isSkill)
        {
            Player.instance.anim.SetTrigger("doSkill");
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
