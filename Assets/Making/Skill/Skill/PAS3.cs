using Assets.Battle;
using Assets.HeroEditor.Common.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PAS3 : BaseSkill
{
    private Coroutine runningSkillCoroutine;

    private bool isSkillExecuted = false;
    public float passiveAttackIncrease = 0.1f;
    public float addAttack;
    private float originarAttack; 

    public SkillSlot skillSlot;


    private void Awake()
    {
        isSkillExecuted = false;
        skillSlot = GetComponent<SkillSlot>();
    }
    private void Start()
    {
        originarAttack = Player.instance.Current_Attack;
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
            originarAttack = Player.instance.Current_Attack;
            runningSkillCoroutine = StartCoroutine(SkillCoroutine());
        }
    }

    private IEnumerator SkillCoroutine()
    {
        while (isSkillExecuted && skillCount < 4)
        {
            addAttack = originarAttack * passiveAttackIncrease;
            Player.instance.Current_Attack += addAttack;
            yield return new WaitForSeconds(6f);
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
            runningSkillCoroutine = null; //
            Player.instance.Current_Attack = originarAttack; // 공격력 초기화
            Player.instance.statDataSave();

        }
    }

}
