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

public class PAS2 : BaseSkill
{
    private Coroutine runningSkillCoroutine;

    private bool isSkillExecuted = false;
    public float passiveSpeedIncrease = 0.1f;
    public float addSpeed;
    private float originarSpeed; 

    public SkillSlot skillSlot;


    private void Awake()
    {
        isSkillExecuted = false;
        skillSlot = GetComponent<SkillSlot>();
    }
    private void Start()
    {
        originarSpeed = Player.instance.playerSpeed;
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
            originarSpeed = Player.instance.playerSpeed;
            runningSkillCoroutine = StartCoroutine(SkillCoroutine());
        }
    }

    private IEnumerator SkillCoroutine()
    {
        while (isSkillExecuted && skillCount < skillCollSownMaxCount)
        {
            addSpeed = passiveSpeedIncrease;
            Player.instance.playerSpeed += addSpeed;
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
            Player.instance.playerSpeed = originarSpeed;

            Player.instance.statDataSave();

        }
    }

}
