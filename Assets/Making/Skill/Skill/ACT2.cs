using Assets.HeroEditor.Common.Scripts.Common;
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
        if (!isSkillEwcuted)
        {
            StartCoroutine(SkillCoroutine());
        }
    }

    private IEnumerator SkillCoroutine()
    {
        Debug.Log("시작하나");
        isSkillEwcuted = true;


        List<GameObject> effectList = new List<GameObject>();
        List<float> usedPosition = new List<float>();
        float skillPositon;
        while (usedPosition.Count < 10)
        {
            skillPositon = UnityEngine.Random.Range(0, 10);

            if (!usedPosition.Contains(skillPositon))
            {
                Vector3 playerPosition = Player.instance.transform.position;
                Vector3 spawnPosition = playerPosition + new Vector3(skillPositon / 3, 1.2f, 0f);

                GameObject effect = Instantiate(effectPrefab, spawnPosition, Quaternion.identity);
                effect.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                effectList.Add(effect);


                usedPosition.Add(skillPositon);
                yield return new WaitForSeconds(0.05f);
            }
        }


        yield return new WaitForSeconds(0.3f); //0.2초 뒤 스킬 삭제

        for (int i = 0; i < effectList.Count; i++)
        {
            Destroy(effectList[i]);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(5);
        isSkillEwcuted = false;
      
    }
}