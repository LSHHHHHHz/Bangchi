using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunBossUI : MonoBehaviour
{
    SunBossPopup sunBossPopup;
    public static SunBossUI instance;
    public GridLayoutGroup gird;

    private void Awake()
    {
        instance = this;
    }
    public void LoadSunBossPage()
    {
        if (sunBossPopup != null)
        {
            Destroy(sunBossPopup.gameObject);
        }
        var prefab = Resources.Load<GameObject>("SunBossPopup");
        sunBossPopup = Instantiate(prefab).GetComponent<SunBossPopup>();

    }
}
