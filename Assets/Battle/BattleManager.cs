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
        float fiveTimeHasPassed;
        bool readToRestartStage = false;
        private void Awake()
        {
            instance = this;
            stageRoot = new GameObject("StageRoot");
        }
        void Update()
        {
            if (stageEndCheck && IsStageEnded())
            {
                OnStageDone?.Invoke(currentStageInfo);
                if (currentStageInfo.Type != StageType.Boss)
                {
                    stageEndCheck = false;
                    fiveTimeHasPassed = 0;
                }
                else
                {
                    fiveTimeHasPassed += Time.deltaTime;
                    if (fiveTimeHasPassed > 5)
                    {
                        stageEndCheck = false;
                        RestartStage();
                        fiveTimeHasPassed = 0;
                    }
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