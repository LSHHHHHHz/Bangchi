using Assets.HeroEditor.InventorySystem.Scripts.Elements;
using Assets.Making.Stage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Battle
{
    public class BattleManager : MonoBehaviour
    {
        public List<ItemInfo> myStages = new List<ItemInfo>();

        public event Action<StageInfo> OnStageDone;

        public static BattleManager instance;
        private bool stageEndCheck = false;
        public StageInfo currentStageInfo;
        public GameObject stageRoot;
        float stageRestartDelay = 0;
        bool readToRestartStage = false;
        private void Awake()
        {
            instance = this;
            stageRoot = new GameObject("StageRoot");
        }
        void Update()
        {
            // 스테이지가 끝났는지 체크
            if (stageEndCheck && IsStageEnded())
            {
                // 끝났다면 OnStageDone 이벤트 발생
                OnStageDone?.Invoke(currentStageInfo);
                // 더이상 스테이지가 끝났는지 체크 안하도록 stageEndCheck 변수 false로 바꿔주기
                stageEndCheck = false;

                // 스테이지 재시작에 걸리는 대기 시간을 설정해준다.
                if (currentStageInfo.Type != StageType.Boss)
                {
                    stageRestartDelay = 2;
                }
                else
                {
                    stageRestartDelay = 5;
                }
            }

            // 만약 스테이지 재시작 대기 시간이 있다면
            if (stageRestartDelay > 0)
            {
                // 현재 시간 지난 만큼 빼준다.
                stageRestartDelay -= Time.deltaTime;
                // 만약 대기 시간이 0보다 작다면 대기가 끝난 것.
                if (stageRestartDelay < 0)
                {
                    // 스테이지 재시작.
                    RestartStage();
                }
            }
        }

        public void StartStage(StageInfo stageInfo)
        {
            UnitManager.instance.player.transform.position = UnitManager.instance.playerInitialPosition;

            currentStageInfo = stageInfo;
            StageInfoUtility.PrepareStage(stageRoot, stageInfo);
            stageEndCheck = true;

        }

        public void StartSunbossStage(SunBossInfo sunBossInfo, int level)
        {
            int hp = sunBossInfo.BossHPByLevel[level];
            StartStage(sunBossInfo);
            var boss = UnitManager.instance.monsterList[0];
            boss._Max_HP = hp;
            boss._Current_HP = hp;
        }

        public void RestartStage()
        {
            StartStage(currentStageInfo);
        }

        // 현재 스테이지
        private bool IsStageEnded()
        {
            return UnitManager.instance.monsterList.Count == 0;
        }
    }
}