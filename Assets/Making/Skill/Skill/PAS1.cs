using Assets.Battle;
using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PAS1 : BaseSkill
{
    //코루틴 중복 방지 위해 만듬
    //실제 실행 중인 코루틴의 정확한 참조를 유지하고, 그것을 중지하려는 의도를 명확히 하기 위함임
    private Coroutine runningSkillCoroutine;

    private bool isSkillExecuted = false;
    public float passiveAttackIncrease = 0.1f; // 공격력이 증가할 비율
    public float addAttack; //추가 공격력
    private float originalAttack; // 원래 공격력 값 저장
    public float timetime;

    public SkillSlot skillSlot;

   
    private void Awake()
    {
        isSkillExecuted = false;
        skillSlot = GetComponent<SkillSlot>();
    }
    private void Start()
    {
        originalAttack = Player.instance.Current_Attack;
        BattleManager.instance.OnStageRestart += ResetSkill;
        IngameSkillList.instance.setempty1 += ResetSkill;
    }
    private void Update()
    {
    }
    private void OnApplicationQuit()
    {
        ResetSkill();
        Debug.Log("꺼졌나");
    }
    public override void Execute()
    {
        if (!isSkillExecuted)
        {
            isSkillExecuted = true;
            //StartSkillCooldown(4f); // 쿨다운 시작
            runningSkillCoroutine = StartCoroutine(SkillCoroutine());
        }
    }

    private IEnumerator SkillCoroutine()
    {
        while (isSkillExecuted && skillCount<8)
        {
            addAttack = originalAttack * passiveAttackIncrease;
            Player.instance.Current_Attack += addAttack;
            Debug.Log($"Current Attack: {Player.instance.Current_Attack}");
            yield return new WaitForSeconds(4f);
            skillCount++;
        }
        //EndSkillCooldown(); // 쿨다운 종료
    }

    public void ResetSkill()
    {
        isSkillExecuted = false;
      
        if (runningSkillCoroutine != null)
        {
            StopCoroutine(runningSkillCoroutine);
            Player.instance.Current_Attack = originalAttack; // 공격력 초기화
            Player.instance.statDataSave();
            skillCount = 0;

        }
    }
   
}
