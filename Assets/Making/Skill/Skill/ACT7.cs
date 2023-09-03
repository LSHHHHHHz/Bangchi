using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ACT7 : BaseSkill
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
       
        for (int i = 0; i < 5; ++i)
        {
            Vector3 playerPosition = Player.instance.transform.position;
            Vector3 spawnPosition = playerPosition + new Vector3(2f, 0f, 0f);

            GameObject effect = Instantiate(effectPrefab, spawnPosition, Quaternion.identity);
            effectList.Add(effect);

            yield return new WaitForSeconds(0.5f);
        }
        Vector3 playerPosition2 = Player.instance.transform.position;
        Vector3 spawnPosition2 = playerPosition2 + new Vector3(2f, 0f, 0f);

        yield return new WaitForSeconds(1f);
        GameObject lastEffect = Instantiate(effectPrefab, spawnPosition2, Quaternion.identity);
        lastEffect.transform.localScale = Vector3.one * 2f; // 2배 더 크게
        effectList.Add(lastEffect);

        yield return new WaitForSeconds(3f); //이걸로 인해서 마지막 공격이 끝난 후로 3초뒤에 스킬 발동시킬 수 있음

        foreach (GameObject effect in effectList)
        {
            Destroy(effect);
        }
        isSkillEwcuted = false;
    }
}