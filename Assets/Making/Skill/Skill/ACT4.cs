using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ACT4 : BaseSkill
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

        List<GameObject> effectList = new List<GameObject>();
        List<float> usedPosition = new List<float>();



        Vector3 playerPosition = Player.instance.transform.position;
        Vector3 spawnPosition = playerPosition + new Vector3(2, 5.5f, 0f);

        GameObject effect = Instantiate(effectPrefab, spawnPosition, Quaternion.identity);
        effect.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        effectList.Add(effect);




        yield return new WaitForSeconds(3f); //3초 뒤 스킬 삭제

        for(int i= 0; i< effectList.Count; i++)
        {
            Destroy(effectList[i]);
            yield return new WaitForSeconds(0.05f);
        }

        isSkillEwcuted = false;
    }
}