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

    private void Awake()
    {
    }
    public void Update()
    {
    }
    public override void Execute()
    {
        Player.instance.anim.SetTrigger("doSkill");
        var projectile = Instantiate(projectilePrefab).GetComponent<ACT_Skill1_Projectile>();
        projectile.transform.position = owner.transform.position;
        projectile.owner = owner;
        projectile.direction = Vector3.right;


    }
}
