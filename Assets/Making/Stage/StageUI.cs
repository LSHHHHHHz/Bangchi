
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
using DG.Tweening;
public class StageUI : MonoBehaviour
{
    StagePopup stagePopup;
    public PageDB pageDB;
    public int currentPage;

    public static StageUI instance;

    private Action<int> stageChangeDelegate;

    private void Awake()
    {
        instance = this;
        stageChangeDelegate = RunStage;
    }
    public void RunStage(int pageIndex)
    {
        if (pageIndex >= pageDB.stagePage.Count || pageIndex < 0)
        {
            return;
        }
        this.currentPage = pageIndex;

        var prefab = Resources.Load<GameObject>("StagePopup");
        stagePopup = Instantiate(prefab).GetComponent<StagePopup>();


        PageInfo pageInfo = pageDB.stagePage[pageIndex];

        stagePopup.Initialize(pageInfo);
        int pageNumber = pageIndex;
        if (pageNumber < pageDB.stagePage.Count)
        {
            PageInfo selectedPage = pageDB.stagePage[pageNumber];
            StagePopup.instance.pageIcon.sprite = Resources.Load<Sprite>(selectedPage.pageIconPath);
            BattleManager.instance.BGscroller.backgroundImage.sprite = Resources.Load<Sprite>(selectedPage.backGroundIconPath);
        }
    }
    public void RightStageChange()
    {
        currentPage++;
        if (currentPage >= pageDB.stagePage.Count)
        {
            currentPage--;
        }
        if (stagePopup != null)
        {
            Destroy(stagePopup.gameObject);
        }
        // RunStage 호출하여 페이지 업데이트
        stageChangeDelegate.Invoke(currentPage);
    }
    public void LeftStageChange()
    {
        currentPage--;
        if (currentPage < 0)
        {
            currentPage++;
        }
        if (stagePopup != null)
        {
            Destroy(stagePopup.gameObject);
        }
        stageChangeDelegate.Invoke(currentPage);
    }
}