using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACT_Skill2_ThunderStrikeSkill : BaseSkillLaunch
{
    public BoxCollider boxcollider;
    private void Start()
    {
        damage = Player.instance.Current_Attack * 0.5f;
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        skill2Status();
    }
    public void skill2Status()
    {
        if (boxcollider != null)
        {
            if (elapsedTime > 0.3f)
            {
                boxcollider.enabled = true;
            }
        }
    }
}
