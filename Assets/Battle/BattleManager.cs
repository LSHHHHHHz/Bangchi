using Assets.HeroEditor.InventorySystem.Scripts.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Battle
{
    public class BattleManager : MonoBehaviour
    {
        public event Action restartStage;
        public List<ItemInfo> myStages = new List<ItemInfo>();

        public static BattleManager instance;
        private string startStageName;

        private bool stageEndCheck = true;
        private string currentStageName;

        // StageInfo 객체를 할당하기 위한 변수 추가
        [SerializeField]
        private StageInfo stageInfo;
        private void Awake()
        {
            instance = this;
            if (startStageName != null)
            {
                currentStageName = startStageName;
                SceneManager.LoadScene(currentStageName, LoadSceneMode.Additive);
            }
        }

   
        void Update()
        {
            if (stageEndCheck && IsStageEnded())
            {
                stageEndCheck = false;

                //RestartStage();
            }
        }

       /* public void RestartStage()
        {
            // reset player position
            UnitManager.instance.player.transform.position = UnitManager.instance.playerInitialPosition;

            // 이미 씬이 로딩되어 있음. 그 상태에서 중복해서 로딩할 수 없기 때문에 씬을 언로드.
            SceneManager.UnloadScene(currentStageName);

            // 언로드했으면 다시 씬 로딩하기.
            SceneManager.LoadScene(currentStageName, LoadSceneMode.Additive);
            stageEndCheck = true;
            restartStage.Invoke();
        }*/

        // 현재 스테이지
        private bool IsStageEnded()
        {
            return UnitManager.instance.monsterList.Count == 0;
        }




        public void LoadStage(string spawnNumber)
        {
            UnitManager.instance.player.transform.position = UnitManager.instance.playerInitialPosition;
            // 이전 스테이지 언로드
            if (currentStageName != null)
            {
                SceneManager.UnloadScene(currentStageName);
            }
            // 새로운 스테이지 로드
            startStageName = spawnNumber;
            SceneManager.LoadScene(startStageName, LoadSceneMode.Additive);
            
            currentStageName = startStageName;
            stageEndCheck = true;
            restartStage.Invoke(); //왜 비어있다고 나오는거지
            
        }
    }
}