using Assets.Battle.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACT8prefab : MonoBehaviour
{
    private float lastUpdateTime;

    private void OnEnable()
    {
        lastUpdateTime = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        float timeSinceLastUpdate = Time.realtimeSinceStartup - lastUpdateTime;
        lastUpdateTime = Time.realtimeSinceStartup;

        // 추가적인 업데이트 로직 구현
    }
}
