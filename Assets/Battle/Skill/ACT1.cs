using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ACT1 : BaseSkill
{
    public GameObject effectPrefab;

    public override void Execute()
    {
        base.Execute();
        // ACT 1 : 5번 공격, 마지막 공격은 더 크게 공격z

        StartCoroutine(SkillCoroutine());
    }

    private IEnumerator SkillCoroutine()
    {
        List<GameObject> effectList = new List<GameObject>();
        for (int i = 0; i < 5; ++i)
        {
            GameObject effect = Instantiate(effectPrefab);
            effectList.Add(effect);

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);
        GameObject lastEffect = Instantiate(effectPrefab);
        lastEffect.transform.localScale = Vector3.one * 2f; // 2배 더 크게
        effectList.Add(lastEffect);

        yield return new WaitForSeconds(3f);

        foreach (GameObject effect in effectList)
        {
            Destroy(effect);
        }
    }
}