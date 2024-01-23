using Assets.Battle;
using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Making.Stage
{
    public class StageSlot : MonoBehaviour
    {
        public StageInfo stageInfo;
        public Image icon;
        public GameObject stageClickButton; //컴포넌트를 갖고오려고 씀
        private Button stageButton;
        public Text StageNum;
        public Text MonsterCount;

        private void Awake()
        {
            stageButton = stageClickButton.GetComponent<Button>();
            stageButton.onClick.AddListener(StageSelect);
        }
        private void Start()
        {
            StageNum.text = "Stage"+stageInfo.StageNumber.ToString();
            MonsterCount.text = "몬스터 수 :"+ stageInfo.monsterSpawnInfos.Count.ToString();
        }
        public void SetData(StageInfo StageInfo)
        {
            this.stageInfo = StageInfo;
        }
        
        public void StageSelect()
        {
            if (stageInfo.Type == StageType.Boss)
            {
                //BossStageProcessor.instance.RunBossStage(stageInfo);
            }
            else
            {
                BattleManager battleManager = BattleManager.instance;
                battleManager.StartStage(stageInfo);
            }
            StagePopup.instance.Exit();
        }
    }
}
