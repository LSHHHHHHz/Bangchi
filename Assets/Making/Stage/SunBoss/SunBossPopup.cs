using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunBossPopup : MonoBehaviour
{
    public static SunBossPopup instance;
    public GridLayoutGroup gird;
    public GameObject subBossSlotPrefab;

    private void Awake()
    {
        instance = this;
    }
}
