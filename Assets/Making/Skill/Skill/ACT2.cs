using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ACT2 : BaseSkill
{
    public GameObject effectPrefab;
    private bool isSkillEwcuted = false;

    public override void Execute()
    {
        // ACT 1 : 5번 공격, 마지막 공격은 더 크게 공격z
        if (!isSkillEwcuted)
        {
            StartCoroutine(SkillCoroutine());
            // isSkillEwcuted = true; (1111)

        }
    }

    private IEnumerator SkillCoroutine()
    {
        Debug.Log("시작하나");
        isSkillEwcuted = true;

        List<GameObject> effectList = new List<GameObject>();
        List<float> usedPosition = new List<float>();


        float skillPositon;
        while(usedPosition.Count < 10)
        {
            skillPositon = UnityEngine.Random.Range(0, 10);

            if (!usedPosition.Contains(skillPositon))
            {
                Vector3 playerPosition = Player.instance.transform.position;
                Vector3 spawnPosition = playerPosition + new Vector3(skillPositon/3, 1.2f, 0f);

                GameObject effect = Instantiate(effectPrefab, spawnPosition, Quaternion.identity);
                effect.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                effectList.Add(effect);


                usedPosition.Add(skillPositon);
                yield return new WaitForSeconds(0.05f);
            }
        }


        yield return new WaitForSeconds(3f); //3초 뒤 스킬 삭제

        for(int i= 0; i< effectList.Count; i++)
        {
            Destroy(effectList[i]);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(10);
        isSkillEwcuted = false;
    }
}