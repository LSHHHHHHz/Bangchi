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
        Vector3 playerPosition = Player.instance.transform.position;
        Vector3 spawnPos = playerPosition + new Vector3(1, 0.1f, 0); 
        GameObject act3Skill = Instantiate(effectPrefab, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(3f);

    }
}

