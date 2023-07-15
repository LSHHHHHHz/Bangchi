
using Assets.Battle;
using Assets.Item1;
using Assets.Making.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    StagePopup stagePopup;
    public StageDB stageDB;
    private int currentPage;

    public static StageUI instance;

    private void Awake()
    {
        instance = this;
    }

    public void RunStage(int page)
    {
        this.currentPage = page;
        
            var prefab = Resources.Load<GameObject>("StagePopup");
            stagePopup = Instantiate(prefab).GetComponent<StagePopup>();

            // 사탕 봉지            =  마트가서      사탕을 사온다.
            StageResult stageResult = StageCalculator.Calculate(stageDB, page);

            stagePopup.Initialize(stageResult);
            int pageNumber = page;
            if (pageNumber < stageDB.stagePage.Count)
            {
                StageInfo selectedPage = stageDB.stagePage[pageNumber];
                StagePopup.instance.pageIcon.sprite = Resources.Load<Sprite>(selectedPage.pageIconPath);
            }
        
    }
    public void RightStageChange()
    {
        currentPage++;
        if (stagePopup != null)
        {
            Destroy(stagePopup.gameObject);
        }

        // RunStage 호출하여 페이지 업데이트
        RunStage(currentPage);
    }


    public void LeftStageChange()
    {
        currentPage--; 
        if (stagePopup != null)
        {
            Destroy(stagePopup.gameObject);
        }
        // RunStage 호출하여 페이지 업데이트
        RunStage(currentPage);
    }
}