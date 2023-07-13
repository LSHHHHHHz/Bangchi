
using Assets.Battle;
using Assets.Item1;
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

    public static StageUI instance;

    private void Awake()
    {
        instance = this;
    }

    public void RunStage(int page)
    {
        if (stagePopup == null)
        {
            var prefab = Resources.Load<GameObject>("StagePopup");
            stagePopup = Instantiate(prefab).GetComponent<StagePopup>();

            // 사탕 봉지            =  마트가서      사탕을 사온다.
            StageResult stageResult = StageCalculator.Calculate(stageDB, page);

            stagePopup.Initialize(stageResult);
        }
    }
    
}