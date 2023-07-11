
using Assets.Battle;
using Assets.Item1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    StagePopup stagePopup;
    StageResult stageResult;
    public StageDB stageDB;

    public void RunStage(int page)
    {
        if (stagePopup == null)
        {
            var prefab = Resources.Load<GameObject>("StagePopup");
            stagePopup = Instantiate(prefab).GetComponent<StagePopup>();

            StageResult stageResult = StageCalculator.Calculate(stageDB, page);
            //어떻게 배열을 받을 수 있는거지??? Caculate는 배열아닌가

            stagePopup.Initialize(stageResult);
        }
    }
}