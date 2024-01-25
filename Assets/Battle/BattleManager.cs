using Assets.HeroEditor.Common.Scripts.Common;
using Assets.HeroEditor.InventorySystem.Scripts.Elements;
using Assets.Making.Stage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditorInternal.VersionControl.ListControl;

namespace Assets.Battle
{
    public class BattleManager : MonoBehaviour
    {

        public List<ItemInfo> MyStages = new List<ItemInfo>();

        public event Action<StageInfo> OnStageDone;
        public event Action OnStageRestart;

        public static BattleManager instance;

        private bool stageEndCheck = false;
        public bool isrestartNomarStage = false;
        
        public StageInfo currentStageInfo;
        public int PageNum;
        public StageInfo LastStageInfo;
        public GameObject stageRoot;
        float stageRestartDelay = 0;
        public bool isRestartStage = false;
        public bool isBossStageStart = false;
        public bool bossStageDone = false;
        public SunBossInfo sunbossInfo;
        public Image bossTimeBarMask;
        public Image bossTimeBar;

        public event Action<SunBossInfo> SunBossInfoStart;

        //GameManger가 Stage시작을 모르니 BattleManager로 옮김
        public int GetLastPlayedNormalStage() => PlayerPrefs.GetInt("LastPlayedNormalStage", defaultValue: 1);
        public void SetLastPlayedNormalStage(int value) => PlayerPrefs.SetInt("LastPlayedNormalStage", value);


        private void Awake()
        {
            if (currentStageInfo != null)
            {
                PageNum = currentStageInfo.pageNumber;
            }
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            stageRoot = new GameObject("StageRoot");
        }
        void Update()
        {
            // 스테이지 또는 보스스테이지가 끝났는지 체크
            if ((stageEndCheck && IsStageEnded())||bossStageDone)
            {
                bossStageDone = false;
                FadeInOutStageProcessor.instance.RunFadeOutStage();

                // 끝났다면 OnStageDone 이벤트 발생
                OnStageDone?.Invoke(currentStageInfo);
                //이 안에서 currentStageInfo를 라스트 스테이지로 저장시켜서 아래 currentStageInfo에 노말이 저장되게됨


                // 더이상 스테이지가 끝났는지 체크 안하도록 stageEndCheck 변수 false로 바꿔주기
                stageEndCheck = false;

                // 스테이지 재시작에 걸리는 대기 시간을 설정해준다.

                stageRestartDelay = 2;
                
            }

            //스테이지가 끝나야 delay값이 있어 아래로 넘어갈 수 있음
            if (stageRestartDelay > 0)
            {
                stageRestartDelay -= Time.deltaTime;
                if (stageRestartDelay < 0)
                {
                    RestartStage();
                    
                }
            }
            FadeInOutStageProcessor.instance.bossstageDone = false;
        }

        public void StartStage(StageInfo stageInfo)
        {
            UnitManager.instance.player.transform.position = UnitManager.instance.playerInitialPosition;

            currentStageInfo = stageInfo;
            LastStageInfo = stageInfo;
            StageInfoUtility.PrepareStage(stageRoot, stageInfo);
            stageEndCheck = true;

            if (stageInfo.Type == StageType.Normal)
            {
                
                isRestartStage = true;
                SetLastPlayedNormalStage(stageInfo.StageNumber);
                OnStageRestart?.Invoke();

            }
            isRestartStage = false;
        }
        
        public void StartSunbossStage(SunBossInfo sunBossInfo, int level)
        {
            sunbossInfo = sunBossInfo;
            isBossStageStart = true;
            isrestartNomarStage = false;
            int hp = sunBossInfo.BossHPByLevel[level];
            StartStage(sunBossInfo);
            var boss = UnitManager.instance.monsterList[0];
            boss._Max_HP = hp;
            boss._Current_HP = hp;
            stageEndCheck = true;
            SunBossInfoStart?.Invoke(sunbossInfo);
        }

        public void RestartStage()
        {
            StartStage(currentStageInfo);
            isrestartNomarStage = true;
            isBossStageStart= false;
        }

        // 현재 스테이지
        private bool IsStageEnded()
        {
            return UnitManager.instance.monsterList.Count == 0;
        }
    }
}