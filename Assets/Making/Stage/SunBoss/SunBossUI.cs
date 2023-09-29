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
        // ���� �ε�� �˾��� �ִٸ� ����
        if (sunBossPopup != null)
        {
            Destroy(sunBossPopup.gameObject);
        }

        // �˾��� �ε��ϰ� �ʱ�ȭ
        var prefab = Resources.Load<GameObject>("SunBossPopup");
        sunBossPopup = Instantiate(prefab).GetComponent<SunBossPopup>();

        // ���� SunBossDB�� ���� ���������� �ִٸ�,
        // ������ ���������� �����ؼ� sunBossPopup.Initialize �޼��忡 ������ �� �ֽ��ϴ�.
        // ���⼭�� ù ��° ���������� �ε��Ѵٰ� �����մϴ�.
        if (sunBossDB.Stages.Count > 0)
        {
            SunBossPageInfo sunBossPageInfo = sunBossDB.Stages[0];
            sunBossPopup.Initialize(sunBossPageInfo);
        }
    }

    //grid�� ������ ������ ��ȯ
    public int GetGridCount()
    {
        return gird.transform.childCount;
    }
    public void RunSunBossStage(int BossLevel)
    {

    }
}
