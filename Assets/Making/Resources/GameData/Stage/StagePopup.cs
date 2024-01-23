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

    public Text PageNumber;
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
    }
    public void Exit()
    {
        Destroy(gameObject);
    }
}
