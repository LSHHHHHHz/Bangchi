using Assets.Battle;
using Assets.Making.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StagePopup : MonoBehaviour
{
    public static StagePopup instance;

    public Image pageIcon;
    public GridLayoutGroup grid;
    public GameObject stagePrefab; //StageSlot 프리펩임

    public StageUI stageUI;
    public Button stagePageLeftButton;
    public Button stagePageRightButton;

    private List<GameObject> children = new List<GameObject>();
    private void Awake()
    {    
        stageUI = GameObject.FindObjectOfType<StageUI>();

        stagePageLeftButton.onClick.AddListener(StageUI.instance.LeftStageChange);
        stagePageRightButton.onClick.AddListener(StageUI.instance.RightStageChange);


        instance = this;
    }
    //Page 변경 파라미터 필요
    //버튼 누를 때 이벤트 쓰고 이벤트가 호출될 때마다 이전에 보관되어있던 UI 파괴
    public void Initialize(PageInfo pageInfo)
    {
        foreach (GameObject child in children)
        {
            Destroy(child);
        }
        children.Clear();


        for (int i = 0; i < pageInfo.stages.Count; i++)
        {
            StageSlot stageSlot = Instantiate(stagePrefab, grid.transform).GetComponent<StageSlot>();
            stageSlot.SetData(pageInfo.stages[i]);

            stageSlot.gameObject.SetActive(true);

            children.Add(stageSlot.gameObject);
        }



        //버튼(page 이동 버튼 클릭 시) 이벤트 함수 호출
        //가차팝업에서 원몰타임이랑 비슷하면서도 Page를 변경시켜야함
    }



    public void Exit()
    {
        Destroy(gameObject);
    }


}
