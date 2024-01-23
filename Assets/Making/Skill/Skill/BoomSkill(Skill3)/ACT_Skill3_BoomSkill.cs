using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACT_Skill3_BoomSkill : BaseSkillLaunch
{
    public BoxCollider boxcollider;
    bool isSkill3Activated = false;
    private void Start()
    {
        damage = Player.instance.Current_Attack * 1;
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        skill3Status();
    }
    public void skill3Status()
    {
        if (elapsedTime > 2 && isSkill3Activated == false)
        {
            boxcollider.isTrigger = false;
            boxcollider.size = new Vector3(3, 3, 3);
            boxcollider.isTrigger = true;
            damage *= 2;
            isSkill3Activated = true;
        }
        if (elapsedTime > 3)
        {
            Destroy(gameObject);
        }

    }
}