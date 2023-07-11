﻿using Assets.Battle;
using Assets.Item1;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
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
        private void Awake()
        {
            stageButton = stageClickButton.GetComponent<Button>();
            stageButton.onClick.AddListener(StageSelect);
        }
        public void SetData(StageInfo StageInfo)
        {
            this.stageInfo = StageInfo;
            icon.sprite = Resources.Load<Sprite>(StageInfo.stageIconPath);
            StagePopup.instance.pageIcon.sprite = Resources.Load<Sprite>(StageInfo.pageIconPath);
        }
        //
        public void StageSelect()
        {
            //BattleManager battleManager = new BattleManager();
            BattleManager battleManager = BattleManager.instance;
            battleManager.LoadStage(stageInfo.stageSpawnCount);
        }
    }
}
