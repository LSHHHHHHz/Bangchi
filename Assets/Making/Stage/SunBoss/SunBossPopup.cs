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
    private List<GameObject> children = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public void Initialize(SunBossPageInfo pageInfo)
    {
        // ��� SunBossStage�� ��ȸ
        foreach (Transform sunBossStage in grid.transform)
        {
            int sunBossInfoIndex = 0;

            // �� SunBossStage�� �ڽĵ��� ��ȸ
            foreach (Transform sunBossSlotTransform in sunBossStage)
            {
                SunBossSlot slot = sunBossSlotTransform.GetComponent<SunBossSlot>();
                if (slot != null && sunBossInfoIndex < pageInfo.sunBossInfos.Count)
                {
                    slot.SetData(pageInfo.sunBossInfos[sunBossInfoIndex]);
                    sunBossInfoIndex++;
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