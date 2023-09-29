using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunBossUI : MonoBehaviour
{
    SunBossPopup sunBossPopup;
    public SunBossDB sunBossDB;
    public static SunBossUI instance;
    public GridLayoutGroup gird;

    private void Awake()
    {
        instance = this;
        LoadSunBossPage();
    }
    public void LoadSunBossPage()
    {
        // 현재 로드된 팝업이 있다면 삭제
        if (sunBossPopup != null)
        {
            Destroy(sunBossPopup.gameObject);
        }

        // 팝업을 로드하고 초기화
        var prefab = Resources.Load<GameObject>("SunBossPopup");
        sunBossPopup = Instantiate(prefab).GetComponent<SunBossPopup>();

        // 만약 SunBossDB에 여러 스테이지가 있다면,
        // 적절한 스테이지를 선택해서 sunBossPopup.Initialize 메서드에 전달할 수 있습니다.
        // 여기서는 첫 번째 스테이지를 로드한다고 가정합니다.
        if (sunBossDB.Stages.Count > 0)
        {
            SunBossPageInfo sunBossPageInfo = sunBossDB.Stages[0];
            sunBossPopup.Initialize(sunBossPageInfo);
        }
    }

    //grid에 장착된 갯수를 반환
    public int GetGridCount()
    {
        return gird.transform.childCount;
    }
    public void RunSunBossStage(int BossLevel)
    {

    }
}
