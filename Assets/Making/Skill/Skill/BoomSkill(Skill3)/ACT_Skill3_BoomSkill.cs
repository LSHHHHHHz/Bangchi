using Assets.Battle;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ACT_Skill3_BoomSkill : BaseSkillLaunch
{
    public BoxCollider boxcollider;
    bool isSkill3Activated = false;
    private void Start()
    {
        damage = Player.instance.Current_Attack * 1;
        BattleManager.instance.stageDoneSkillDestory += skillDestory;
        FadeInOutStageProcessor.instance.stageClickForSkillDestory += skillDestory;
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        skill3Status();
    }
    void skillDestory()
    {
        Destroy(gameObject);
    }
    void OnDestroy()
    {
        FadeInOutStageProcessor.instance.stageClickForSkillDestory -= skillDestory;
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