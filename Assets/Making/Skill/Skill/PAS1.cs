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
    public float passiveHPIncrease = 0.1f; // 체력이 증가할 비율
    public float addHP; //추가 체력
    private float originarHP; // 원래 체력 값 저장
    public float timetime;

    public SkillSlot skillSlot;

   
    private void Awake()
    {
        isSkillExecuted = false;
        skillSlot = GetComponent<SkillSlot>();
    }
    private void Start()
    {
        originarHP = Player.instance.Max_HP;
        BattleManager.instance.OnStageRestart += ResetSkill;
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
            originarHP = Player.instance.Max_HP;
            runningSkillCoroutine = StartCoroutine(SkillCoroutine());
        }
    }

    private IEnumerator SkillCoroutine()
    {
        while (isSkillExecuted && skillCount<4)
        {
            addHP = originarHP * passiveHPIncrease;
            Player.instance.Max_HP += addHP;
            Debug.Log($"Current Attack: {Player.instance.Current_HP}");
            yield return new WaitForSeconds(4f);
            skillCount++;
        }
    }

    public void ResetSkill()
    {
        isSkillExecuted = false;
        skillCount = 0;
        if (runningSkillCoroutine != null)
        {
            StopCoroutine(runningSkillCoroutine);
            runningSkillCoroutine = null;
            Player.instance.Max_HP = originarHP;

            Player.instance.statDataSave();

        }
    }
   
}
