using Assets.Battle.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACT8: BaseSkill
{
    private float realTimeDuration = 4.0f; // 프리팹이 활성화되어 있을 시간
    private float startTime;

    public GameObject effectPrefab;
    void OnEnable()
    {
    }
    private void Update()
    {
    }
    public override void Execute()
    {
        StartCoroutine(SkillCoroutine());
    }

    private IEnumerator SkillCoroutine()
    {
        Vector3 playerPos = Player.instance.transform.position;
        GameObject prefab = Instantiate(effectPrefab, playerPos + new Vector3(1.2f, 1, 0), Quaternion.identity);

        yield return new WaitForSeconds(4f);
        Destroy(prefab);
    }

    
}
