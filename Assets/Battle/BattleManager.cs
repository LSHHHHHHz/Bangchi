using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Battle
{
    public class BattleManager : MonoBehaviour
    {
        public event Action restartStage;

        public static BattleManager instance;
        public string startStageName;

        private bool stageEndCheck = true;
        private string currentStageName;

        private void Awake()
        {
            instance = this;
            currentStageName = startStageName;
            SceneManager.LoadScene(startStageName, LoadSceneMode.Additive);
        }

        void Update()
        {
            if (stageEndCheck && IsStageEnded())
            {
                stageEndCheck = false;

                RestartStage();
            }
        }

        private void RestartStage()
        {
            // reset player position
            UnitManager.instance.player.transform.position = UnitManager.instance.playerInitialPosition;

            // 이미 씬이 로딩되어 있음. 그 상태에서 중복해서 로딩할 수 없기 때문에 씬을 언로드.
            SceneManager.UnloadScene(currentStageName);

            // 언로드했으면 다시 씬 로딩하기.
            SceneManager.LoadScene(currentStageName, LoadSceneMode.Additive);
            stageEndCheck = true;
            restartStage.Invoke();
        }

        // 현재 스테이지
        private bool IsStageEnded()
        {
            return UnitManager.instance.monsterList.Count == 0;
        }




        public void LoadStage(int page, int stageNumber)
        {
            // 이전 스테이지 언로드
            SceneManager.UnloadScene(currentStageName);

            // 새로운 스테이지 로드
            string newStageName = "Stage" + page + "-" + stageNumber;
            SceneManager.LoadScene(newStageName, LoadSceneMode.Additive);

            currentStageName = newStageName;
        }
    }
}