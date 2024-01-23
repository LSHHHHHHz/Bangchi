using Assets.Making.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SunBossPopup : MonoBehaviour
{
    public static SunBossPopup instance;
    public GridLayoutGroup grid;
    public GameObject sunBossSlotPrefab;
    public SunBossPageInfo pageInfo;
    private List<GameObject> children = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        Initialize();
    }

    public void destorypopup()
    {
        Destroy(gameObject);
    }
    public void Initialize()
    {
        // 모든 SunBossStage를 순회
        foreach (Transform sunBossStage in grid.transform)
        {
            // 각 SunBossStage의 자식들을 순회
            foreach (Transform sunBossSlotTransform in sunBossStage)
            {
                SunBossSlot slot = sunBossSlotTransform.GetComponent<SunBossSlot>();
                if (slot == null)
                    continue;

                SunBossInfo sunBossInfo = pageInfo.sunBossInfos.Find(bossInfo => bossInfo.bossType == slot.bossType);
                if (sunBossInfo != null)
                {
                    slot.SetData(sunBossInfo);
                }
            }
        }
    }

    public int GetCountGrid()
    {
        return grid.transform.childCount;
    }

    public void Exit()
    {
        Destroy(gameObject);
    }
}