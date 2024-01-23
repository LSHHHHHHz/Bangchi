using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ACT3_Boom : BaseSkill
{
    public GameObject effectPrefab;
    private bool isSkillEwcuted = false;

    public override void Execute()
    {
        if (!isSkillEwcuted)
        {
            StartCoroutine(SkillCoroutine());

        }
    }

    private IEnumerator SkillCoroutine()
    {
        if (Player.instance.skillhits[2].Length > 0) 
        {      //Vector3 playerPosition = Player.instance.transform.position;
            Vector3 spawnPos = Player.instance.skillhits[2][0].transform.position;
            GameObject act3Skill = Instantiate(effectPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
        else
        {
            Debug.LogError("적 없음");
        }
    }
}

