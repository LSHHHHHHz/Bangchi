using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class ACT5_Sword : BaseSkill
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
        isSkillEwcuted = true;

        GameObject effect = new();
        float skillPos = 1;
        Vector3 playerPosition = Player.instance.transform.position;
        Vector3 spawnPos = playerPosition + new Vector3(skillPos, 0, 0);
        GameObject act3Skill = Instantiate(effectPrefab, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(2f);

        Destroy(act3Skill);
        isSkillEwcuted = false;
    }
}

