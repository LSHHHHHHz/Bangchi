using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Making.Stage
{
    public static class StageInfoUtility
    {
        public static void PrepareStage(GameObject stageRoot, StageInfo stageInfo)
        {
            // 이미 소환되어 있는 몬스터를 모두 파괴
            while (stageRoot.transform.childCount > 0) //testObjectRoot의 자식 개수가 0보다 크면 계속 실행
            {
                UnityEngine.Object.DestroyImmediate(stageRoot.transform.GetChild(0).gameObject); //계속 파괴
            }

            if (stageInfo== null)
                return;

            // 현재 스테이지 정보의 몬스터 소환 정보를 바탕으로 몬스터를 소환
            foreach (var spawnInfo in stageInfo.monsterSpawnInfos)
            {
                GameObject spawnedMonster = UnityEngine.Object.Instantiate(spawnInfo.prefab, stageRoot.transform);
                spawnedMonster.transform.position = spawnInfo.position;

            }

        }
    }
}
